using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
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
        Task UpdateTournamentAsync(string tournamentId, TournamentDetail updateTournament);
        Task UpdateTournamentPropertyAsync(string id, string fieldName, object val);
        Task<TournamentDetail> GetTournamentDetailAsync(string tournamentId);
        Task<IList<Tournament>> GetTournamentsAsync();
        Task DeleteAllTournamentsAsync();
        Task DeleteTournamentAsync(string tournamentId);
    }
}
