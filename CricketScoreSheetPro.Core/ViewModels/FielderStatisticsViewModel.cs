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
    public class FielderStatisticsViewModel : StatisticsViewModel
    {
        public FielderStatisticsViewModel(ITeamService teamService,
            IPlayerInningService playerInningService,
            StatisticsService statistics) : base(teamService, playerInningService, statistics)
        {
            FielderStatistics = Statistics.OrderByDescending(b => b.Catches).ToList();
        }
        public List<PlayerStatistics> FielderStatistics { get; private set; }
    }
}
