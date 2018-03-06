using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public class TeamDetailViewModel
    {
        private readonly ITeamService _teamService;

        public TeamDetailViewModel(ITeamService teamService, string teamId)
        {
            _teamService = teamService;
            Team = _teamService.GetTeamAsync(teamId).Result;
        }

        public TeamDetail Team { get; private set; }
    }
}
