using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public class AddDialogViewModel
    {
        public string Title { get; set; }
        private readonly ITeamService _teamService;
        private readonly ITournamentService _tournamentService;

        public AddDialogViewModel(ITeamService teamService)
        {
            Title = "Add Team";
            _teamService = teamService;
        }

        public AddDialogViewModel(ITournamentService tournamentService)
        {
            Title = "Add Tournament";
            _tournamentService = tournamentService;
        }

        public Tournament AddTournament(string tournamentName)
        {
            var tournamentAdd = _tournamentService.AddTournamentAsync(new Tournament { Name = tournamentName }).Result;
            return tournamentAdd;
        }

        public Team AddTeam(string teamName)
        {
            var teamAdd = _teamService.AddTeamAsync(new Team { Name = teamName }).Result;
            return teamAdd;
        }
    }
}
