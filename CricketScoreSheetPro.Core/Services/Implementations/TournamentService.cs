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
        private readonly IRepository<TournamentDetail> _tournamentdetailRepository;

        public TournamentService(IRepository<Tournament> tournamentRepository, IRepository<TournamentDetail> tournamentdetailRepository)
        {
            _tournamentRepository = tournamentRepository ?? throw new ArgumentNullException($"TournamentRepository is null");
            _tournamentdetailRepository = tournamentdetailRepository ?? throw new ArgumentNullException($"TournamentDetailRepository is null");
        }

        public async Task<Tournament> AddTournamentAsync(Tournament newTournament)
        {
            if (newTournament == null) throw new ArgumentNullException($"Tournament is null");
            var tournamentAdd = await _tournamentRepository.CreateAsync(newTournament);
            var newtournamentdetail = new TournamentDetail
            {
                Name = tournamentAdd.Name,
                Status = tournamentAdd.Status
            };
            await _tournamentdetailRepository.CreateWithIdAsync(tournamentAdd.Id, newtournamentdetail);
            return tournamentAdd;
        }

        public async Task UpdateTournamentAsync(string tournamentId, TournamentDetail updateTournamentDetail)
        {
            if (updateTournamentDetail == null) throw new ArgumentNullException($"Tournament is null");
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"Tournament ID is null");
            var updatetournament = new Tournament
            {
                Name = updateTournamentDetail.Name,
                Status = updateTournamentDetail.Status
            };
            await _tournamentRepository.CreateWithIdAsync(tournamentId, updatetournament);
            await _tournamentdetailRepository.CreateWithIdAsync(tournamentId, updateTournamentDetail);
        }

        public async Task UpdateTournamentPropertyAsync(string id, string fieldName, object val)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Tournament ID is null");
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentException($"Tournament property is null");
            if (val == null) throw new ArgumentException($"Tournament property value is null");
            if (fieldName.ToLower() == "name") await _tournamentRepository.UpdateAsync(id, "TournamentName", val);
            if (fieldName.ToLower() == "status") await _tournamentRepository.UpdateAsync(id, "Status", val);
            await _tournamentdetailRepository.UpdateAsync(id, fieldName, val);
        }

        public async Task<TournamentDetail> GetTournamentDetailAsync(string tournamentId)
        {
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"Tournament ID is null");
            var tournament = await _tournamentdetailRepository.GetItemAsync(tournamentId);
            return tournament;
        }

        public async Task<IList<Tournament>> GetTournamentsAsync()
        {
            var usertournaments = await _tournamentRepository.GetListAsync();
            return usertournaments;
        }

        public async Task DeleteAllTournamentsAsync()
        {
            await _tournamentRepository.DeleteAsync();
            await _tournamentdetailRepository.DeleteAsync();
        }

        public async Task DeleteTournamentAsync(string tournamentId)
        {
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"Tournament ID is null");
            await _tournamentRepository.DeleteByIdAsync(tournamentId);
            await _tournamentdetailRepository.DeleteByIdAsync(tournamentId);
        }
    }
}
