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
        private readonly IUmpireService _umpireService;

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

        public AddDialogViewModel(IUmpireService umpireService)
        {
            Title = "Add Umpire";
            _umpireService = umpireService;
        }

        public string AddTournament(string tournamentName)
        {
            var tournamentAddKey = _tournamentService.AddTournament(
                new Tournament {
                    Name = tournamentName,
                    Status = "Open",
                });
            return tournamentAddKey;
        }

        public string AddTeam(string teamName)
        {
            var teamAddKey = _teamService.AddTeam(new Team { Name = teamName });
            return teamAddKey;
        }

        public string AddUmpire(string umpireName)
        {
            var umpireAddKey = _umpireService.AddUmpire(new Umpire { Name = umpireName });
            return umpireAddKey;
        }
    }
}
