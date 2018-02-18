using CricketScoreSheetPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services
{
    public class AccessService
    {
        public static List<Ball> HomeTeamBalls { get; set; }

        public static List<Ball> AwayTeamBalls { get; set; }

        public string UUID { get; set; }

        public bool ImportFlag { get; set; }

        public string TeamId { get; set; }

        private PlayerService playerService;
        public PlayerService PlayerService
        {
            get
            {
                if (playerService == null)
                    playerService = new PlayerService();
                return playerService;
            }
        }

        private TeamPlayerService teamPlayerService;
        public TeamPlayerService TeamPlayerService
        {
            get
            {
                if (teamPlayerService == null && !string.IsNullOrEmpty(TeamId))
                    teamPlayerService = new TeamPlayerService(TeamId);
                return teamPlayerService;
            }
        }

        private TeamService teamService;
        public TeamService TeamService
        {
            get
            {
                if (teamService == null)
                    teamService = new TeamService();
                return teamService;
            }
        }

        private TournamentService tournamentService;
        public TournamentService TournamentService
        {
            get
            {
                if (tournamentService == null)
                    tournamentService = new TournamentService();
                return tournamentService;
            }
        }

        private UserMatchService matchService;
        public UserMatchService MatchService
        {
            get
            {
                if (matchService == null && !string.IsNullOrEmpty(UUID))
                    matchService = new UserMatchService(UUID, ImportFlag);
                return matchService;
            }
        }

        private UserTeamService userTeamService;
        public UserTeamService UserTeamService
        {
            get
            {
                if (userTeamService == null && !string.IsNullOrEmpty(UUID))
                    userTeamService = new UserTeamService(UUID, ImportFlag);
                return userTeamService;
            }
        }

        private UserTournamentService userTournamentService;
        public UserTournamentService UserTournamentService
        {
            get
            {
                if (userTournamentService == null && !string.IsNullOrEmpty(UUID))
                    userTournamentService = new UserTournamentService(UUID, ImportFlag);
                return userTournamentService;
            }
        }
    }
}
