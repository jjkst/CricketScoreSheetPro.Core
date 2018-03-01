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
            Teams = _teamService.GetUserTeamsAsync().Result.ToList();
        }

        public List<UserTeam> Teams { get; private set; }
    }
}
