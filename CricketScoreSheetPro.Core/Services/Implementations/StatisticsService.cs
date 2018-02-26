using CricketScoreSheetPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class StatisticsService
    {
        public IDictionary<string, Player> GetBatsmanStatistics(IDictionary<string, Player> playerspermatch)
        {
            var playersStatistics = playerspermatch;
            return playersStatistics;
        }

        public IDictionary<string, Player> GetBowlerStatistics(IDictionary<string, Player> playerspermatch)
        {
            var playersStatistics = playerspermatch;
            return playersStatistics;
        }

        public IDictionary<string, Player> GetFielderStatistics(IDictionary<string, Player> playerspermatch)
        {
            var playersStatistics = playerspermatch;
            return playersStatistics;
        }

        public IDictionary<string, Team> GetTeamStatistics(IDictionary<string, Team> teamsperTournament)
        {
            var teamsStatistics = teamsperTournament;
            return teamsStatistics;
        }
    }
}
