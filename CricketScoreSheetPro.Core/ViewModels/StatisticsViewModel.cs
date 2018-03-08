using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Implementations;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public abstract class StatisticsViewModel
    {
        private readonly ITeamService _teamService;
        private readonly IPlayerInningService _playerInningService;
        private readonly StatisticsService _statistics;

        public StatisticsViewModel(ITeamService teamService,
            IPlayerInningService playerInningService,
            StatisticsService statistics)
        {
            _teamService = teamService ??
                throw new ArgumentNullException($"TeamService is null");
            _playerInningService = playerInningService ??
                throw new ArgumentNullException($"PlayerInningService is null");
            _statistics = statistics ?? throw new ArgumentNullException($"StatisticsService is null");

            Statistics = new List<PlayerStatistics>();
            foreach (var team in _teamService.GetTeamsAsync().Result)
            {
                var players = _teamService.GetTeamDetailAsync(team.Id).Result.Players;
                foreach (var player in players)
                {
                    Statistics.Add(_statistics.GetPlayerStatistics(
                            _playerInningService.GetPlayerInningsAsync(player.Id).Result));
                }
            }
        }

        public List<PlayerStatistics> Statistics { get; private set; }
    }
}
