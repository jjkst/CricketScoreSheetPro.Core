using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public class TournamentAddViewModel
    {
        private readonly ITournamentService _tournamentService;

        public TournamentAddViewModel(ITournamentService tournamentService, Tournament newtournament)
        {
            _tournamentService = tournamentService;
            Tournament = _tournamentService.AddTournamentAsync(newtournament).Result;
        }

        public Tournament Tournament { get; private set; }
    }
}
