using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
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
            _tournamentRepository = tournamentRepository;
            _usertournamentRepository = usertournamentRepository;
        }

        public async Task<Tournament> AddTournamentAsync(Tournament newTournament)
        {
            var tournamentAdd = await _tournamentRepository.CreateAsync(newTournament);
            var newusertournament = new UserTournament
            {
                TournamentId = tournamentAdd.Key,
                TournamentName = tournamentAdd.Object.Name,
                Status = tournamentAdd.Object.Status
            };
            await _usertournamentRepository.CreateWithIdAsync(tournamentAdd.Key, newusertournament);
            return tournamentAdd.Object;
        }

        public async Task UpdateTournamentAsync(string tournamentId, Tournament updateTournament)
        {
            await _tournamentRepository.CreateWithIdAsync(tournamentId, updateTournament);
            var updateusertournament = new UserTournament
            {
                TournamentId = tournamentId,
                TournamentName = updateTournament.Name,
                Status = updateTournament.Status
            };
            await _usertournamentRepository.CreateWithIdAsync(tournamentId, updateusertournament);
        }

        public async Task<Tournament> GetTournamentAsync(string tournamentId)
        {
            var tournament = await _tournamentRepository.GetItemAsync(tournamentId);
            return tournament;
        }

        public async Task<IList<UserTournament>> GetTournamentsAsync()
        {
            var usertournaments = await _usertournamentRepository.GetListAsync();
            return Common.ConvertFirebaseObjectCollectionToList(usertournaments);
        }

        public async Task DeleteAllTournamentsAsync()
        {
            await _tournamentRepository.DeleteAsync();
            await _usertournamentRepository.DeleteAsync();
        }

        public async Task DeleteTournamentAsync(string tournamentId)
        {
            await _tournamentRepository.DeleteByIdAsync(tournamentId);
            await _usertournamentRepository.DeleteByIdAsync(tournamentId);
        }
    }
}
