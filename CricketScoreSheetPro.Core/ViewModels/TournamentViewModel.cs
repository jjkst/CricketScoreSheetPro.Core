using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public class TournamentViewModel 
    {
        private readonly ITournamentService _tournamentService;
        
        public TournamentViewModel(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService ?? throw new ArgumentNullException($"TournamentService is null, cannot get tournaments."); 
        }

        public List<Tournament> Tournaments => _tournamentService.GetTournaments().ToList();

    }
}
