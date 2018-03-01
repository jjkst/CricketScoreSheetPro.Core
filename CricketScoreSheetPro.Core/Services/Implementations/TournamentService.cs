using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class TournamentService : ITournamentService
    {
        private readonly IRepository<Tournament> _tournamentRepository;
        private readonly IRepository<UserTournament> _usertournamentRepository;

        public TournamentService(IRepository<Tournament> tournamentRepository, IRepository<UserTournament> usertournamentRepository)
        {
            _tournamentRepository = tournamentRepository ?? throw new ArgumentNullException($"TournamentRepository is null");
            _usertournamentRepository = usertournamentRepository ?? throw new ArgumentNullException($"UserTournamentRepository is null");
        }

        public async Task<Tournament> AddTournamentAsync(Tournament newTournament)
        {
            if (newTournament == null) throw new ArgumentNullException($"Tournament is null");
            var tournamentAdd = await _tournamentRepository.CreateAsync(newTournament);
            var newusertournament = new UserTournament
            {
                TournamentId = tournamentAdd.Id,
                TournamentName = tournamentAdd.Name,
                Status = tournamentAdd.Status
            };
            await _usertournamentRepository.CreateWithIdAsync(tournamentAdd.Id, newusertournament);
            return tournamentAdd;
        }

        public async Task UpdateTournamentAsync(string tournamentId, Tournament updateTournament)
        {
            if (updateTournament == null) throw new ArgumentNullException($"Tournament is null");
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"Tournament ID is null");
            await _tournamentRepository.CreateWithIdAsync(tournamentId, updateTournament);
            var updateusertournament = new UserTournament
            {
                TournamentId = tournamentId,
                TournamentName = updateTournament.Name,
                Status = updateTournament.Status
            };
            await _usertournamentRepository.CreateWithIdAsync(tournamentId, updateusertournament);
        }

        public async Task UpdateTournamentPropertyAsync(string id, string fieldName, object val)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Tournament ID is null");
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentException($"Tournament property is null");
            if (val == null) throw new ArgumentException($"Tournament property value is null");
            await _tournamentRepository.UpdateAsync(id, fieldName, val);
            if (fieldName.ToLower() == "name") await _usertournamentRepository.UpdateAsync(id, "TournamentName", val);
            if (fieldName.ToLower() == "status") await _usertournamentRepository.UpdateAsync(id, "Status", val);
        }

        public async Task<Tournament> GetTournamentAsync(string tournamentId)
        {
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"Tournament ID is null");
            var tournament = await _tournamentRepository.GetItemAsync(tournamentId);
            return tournament;
        }

        public async Task<IList<UserTournament>> GetUserTournamentsAsync()
        {
            var usertournaments = await _usertournamentRepository.GetListAsync();
            return usertournaments;
        }

        public async Task DeleteAllTournamentsAsync()
        {
            await _tournamentRepository.DeleteAsync();
            await _usertournamentRepository.DeleteAsync();
        }

        public async Task DeleteTournamentAsync(string tournamentId)
        {
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"Tournament ID is null");
            await _tournamentRepository.DeleteByIdAsync(tournamentId);
            await _usertournamentRepository.DeleteByIdAsync(tournamentId);
        }
    }
}
