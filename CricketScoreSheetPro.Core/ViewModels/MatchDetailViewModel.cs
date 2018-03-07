using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public class MatchDetailViewModel
    {
        private readonly IMatchService _matchService;
        private readonly IPlayerInningService _playerInningService;

        public MatchDetailViewModel(IMatchService matchService, IPlayerInningService playerInningService, 
            string matchId, string battingInningId)
        {
            _matchService = matchService ?? throw new ArgumentNullException($"MatchService is null");
            _playerInningService = playerInningService ?? throw new ArgumentNullException($"PlayerInningService is null");
            Match = _matchService.GetMatchAsync(matchId).Result;

            SelectedInnings(battingInningId);
        }

        public Match Match { get; private set; }

        public bool MatchStatus { get; private set; }

        public TeamInning BattingTeam { get; private set; }

        public TeamInning BowlingTeam { get; private set; }

        public List<PlayerInning> Batsman { get; private set; }

        public List<PlayerInning> Bowlers { get; private set; }

        public void SelectedInnings(string battingInningId)
        {
            if (Match.HomeTeam.Id == battingInningId)
            {
                BattingTeam = Match.HomeTeam;
                BowlingTeam = Match.AwayTeam;
                Batsman = _playerInningService.GetPlayerInningsAsync().Result.ToList(); //filter by teamId, matchId, tournamentId
                Bowlers = _playerInningService.GetPlayerInningsAsync().Result.ToList(); //filter by teamId, matchId, tournamentId

            }
            else if (Match.AwayTeam.Id == battingInningId)
            {
                BattingTeam = Match.AwayTeam;
                BowlingTeam = Match.HomeTeam;
                Batsman = _playerInningService.GetPlayerInningsAsync().Result.ToList(); //filter by teamId, matchId, tournamentId
                Bowlers = _playerInningService.GetPlayerInningsAsync().Result.ToList(); //filter by teamId, matchId, tournamentId
            }
            else
            {
                throw new ArgumentNullException($"Batting team id is not valid");
            }
        }
    }
}
