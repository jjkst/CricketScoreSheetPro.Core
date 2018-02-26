﻿using CricketScoreSheetPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<Tournament> AddTournamentAsync(Tournament newTournament);
        Task UpdateTournamentAsync(string tournamentId, Tournament updateTournament);
        Task<Tournament> GetTournamentAsync(string tournamentId);
        Task<IList<UserTournament>> GetTournamentsAsync();
        Task DeleteAllTournamentsAsync();
        Task DeleteTournamentAsync(string tournamentId);
    }
}
