using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Implementations;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public class ScoreViewModel
    {
        private readonly IMatchService _matchService;
        private readonly IPlayerInningService _playerInningService;
        
        public ScoreViewModel(IMatchService matchService,
            IPlayerInningService playerInningService,
            string matchId, string teamId, bool IsBatsman)
        {
            _matchService = matchService ?? throw new ArgumentNullException($"MatchService is null");
            _playerInningService = playerInningService ?? throw new ArgumentNullException($"PlayerInningService is null");

            CurrentMatch = _matchService.GetMatchAsync(matchId).Result;
            IList<PlayerInning> batsman;
            IList<PlayerInning> bowler;
            if ((IsBatsman && CurrentMatch.HomeTeam.Id == teamId) || (!IsBatsman && CurrentMatch.AwayTeam.Id == teamId))
            {
                BattingTeamName = CurrentMatch.HomeTeam.TeamName;
                batsman = _playerInningService.GetAllPlayerInningsByTeamMatchId(CurrentMatch.HomeTeam.Id, matchId).Result;
                bowler = _playerInningService.GetAllPlayerInningsByTeamMatchId(CurrentMatch.AwayTeam.Id, matchId).Result;
            }
            else if ((IsBatsman && CurrentMatch.AwayTeam.Id == teamId) || (!IsBatsman && CurrentMatch.HomeTeam.Id == teamId))
            {
                BattingTeamName = CurrentMatch.AwayTeam.TeamName;
                batsman = _playerInningService.GetAllPlayerInningsByTeamMatchId(CurrentMatch.AwayTeam.Id, matchId).Result;
                bowler = _playerInningService.GetAllPlayerInningsByTeamMatchId(CurrentMatch.HomeTeam.Id, matchId).Result;
            }
            else
            {
                throw new ArgumentException("Team Id is invalid");
            }

            ActiveBatsman = batsman.Where(p => p.HowOut == "not out").ToList();   
            ActiveBowler = bowler.ToList();
            Fielder_Keeper = bowler.ToList();
        }

        public string BattingTeamName { get; set; }

        public Match CurrentMatch { get; set; }

        public List<PlayerInning> ActiveBatsman { get; set; }

        public List<PlayerInning> ActiveBowler { get; set; }

        public List<PlayerInning> Fielder_Keeper { get; set; }

        public List<int> ExtraRuns
        {
            get
            {
                return new List<int> { 0, 1, 2, 3, 4 };
            }
        }

        public void Update(Ball thisBall, List<Ball> allballs)
        {
            var ballService = new BallService(thisBall);
            ballService.UpdateMatch(CurrentMatch, BattingTeamName);
            ballService.UpdateBatsman(_playerInningService.GetPlayerInningAsync(thisBall.ActiveBatsmanId).Result);
            var selectedBowler = _playerInningService.GetPlayerInningAsync(thisBall.ActiveBowlerId).Result;
            ballService.UpdateBowler(selectedBowler);
            if(ballService.GetMaidenValue(allballs) == 1)
            {
                //_playerInningService.UpdatePlayerInningPropertyAsync(nameof(PlayerInning.Maiden), selectedBowler.Maiden + 1);
            }            
            if (string.IsNullOrEmpty(thisBall.FielderId)) return;
            ballService.UpdateFielder(_playerInningService.GetPlayerInningAsync(thisBall.FielderId).Result);
        }

        public void Undo(Ball thisBall, List<Ball> allballs)
        {
            var ballService = new BallService(thisBall, true);
            ballService.UpdateMatch(CurrentMatch, BattingTeamName);
            ballService.UpdateBatsman(_playerInningService.GetPlayerInningAsync(thisBall.ActiveBatsmanId).Result);
            var selectedBowler = _playerInningService.GetPlayerInningAsync(thisBall.ActiveBowlerId).Result;
            ballService.UpdateBowler(selectedBowler);
            if (ballService.GetMaidenValue(allballs) == -1)
            {
                //_playerInningService.UpdatePlayerInningPropertyAsync(nameof(PlayerInning.Maiden), selectedBowler.Maiden - 1);
            }
            if (string.IsNullOrEmpty(thisBall.FielderId)) return;
            ballService.UpdateFielder(_playerInningService.GetPlayerInningAsync(thisBall.FielderId).Result);
        }

    }
}
