using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public class TeamViewModel
    {
        private readonly ITeamService _teamService;

        public TeamViewModel(ITeamService teamService)
        {
            _teamService = teamService;
            Teams = _teamService.GetTeams().ToList();
        }

        public List<Team> Teams { get; private set; }

    }
}
