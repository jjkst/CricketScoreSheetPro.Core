﻿using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Implementations;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public class BatsmanStatisticsViewModel : StatisticsViewModel
    {
       
        public BatsmanStatisticsViewModel(ITeamService teamService,
            IPlayerInningService playerInningService, 
            StatisticsService statistics) : base(teamService, playerInningService, statistics)
        {
            BatsmanStatistics = Statistics.OrderByDescending(b => b.Runs).ToList();
        }

        public List<PlayerStatistics> BatsmanStatistics { get; private set; }

    }
}
