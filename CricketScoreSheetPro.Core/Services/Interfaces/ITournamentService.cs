using CricketScoreSheetPro.Core.Models;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Services.Interfaces
{
    public interface ITournamentService
    {
        string AddTournament(Tournament newTournament);
        void UpdateTournament(string tournamentId, TournamentDetail updateTournament);
        void UpdateTournamentProperty(string id, string fieldName, object val);
        TournamentDetail GetTournamentDetail(string tournamentId);
        IList<Tournament> GetTournaments();
        void DeleteAllTournaments();
        void DeleteTournament(string tournamentId);
    }
}
