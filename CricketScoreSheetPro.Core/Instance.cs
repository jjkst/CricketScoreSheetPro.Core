using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services
{
    public class Instance
    {
        public static List<Ball> HomeTeamBalls { get; set; }

        public static List<Ball> AwayTeamBalls { get; set; }

        public string UUID { get; set; }

        public string TeamId { get; set; }

        //private PlayerService playerService;
        //public PlayerService PlayerService
        //{
        //    get
        //    {
        //        if (playerService == null)
        //            playerService = new PlayerService();
        //        return playerService;
        //    }
        //}

        //private TeamService teamService;
        //public TeamService TeamService
        //{
        //    get
        //    {
        //        if (teamService == null)
        //            teamService = new TeamService();
        //        return teamService;
        //    }
        //}

        //private TournamentService tournamentService;
        //public TournamentService TournamentService
        //{
        //    get
        //    {
        //        if (tournamentService == null)
        //            tournamentService = new TournamentService();
        //        return tournamentService;
        //    }
        //}

        //private MatchService matchService;
        //public MatchService MatchService
        //{
        //    get
        //    {
        //        if (matchService == null && !string.IsNullOrEmpty(UUID))
        //            matchService = new MatchService(UUID, ImportFlag);
        //        return matchService;
        //    }
        //}
    }
}
