using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public class TeamsViewModel
    {
        private readonly ITeamService _teamService;

        public TeamsViewModel(ITeamService teamService)
        {
            _teamService = teamService;
            Teams = _teamService.GetTeamsAsync().Result.ToList();
        }

        public List<Team> Teams { get; private set; }

        public Team AddTeam(string teamName)
        {
            var tournamentAdd = _teamService.AddTeamAsync(new Team { Name = teamName }).Result;
            return tournamentAdd;
        }
    }
}
