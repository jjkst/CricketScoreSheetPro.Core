﻿using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public class TournamentsViewModel
    {
        private readonly ITournamentService _tournamentService;

        public TournamentsViewModel(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
            Tournaments = _tournamentService.GetUserTournamentsAsync().Result.ToList();
        }

        public List<UserTournament> Tournaments { get; private set; }
    }
}
