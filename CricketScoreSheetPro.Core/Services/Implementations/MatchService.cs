using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class MatchService : IMatchService
    {
        private readonly IRepository<UserMatch> _usermatchRepository;

        public MatchService(IRepository<UserMatch> usermatchRepository)
        {
            _usermatchRepository = usermatchRepository;
        }

        public async Task<UserMatch> AddMatchAsync(UserMatch newmatch)
        {
            var matchAdd = await _usermatchRepository.CreateAsync(newmatch);
            return matchAdd.Object;
        }

        public async Task UpdateMatchAsync(string matchId, UserMatch updateMatch)
        {
            await _usermatchRepository.CreateWithIdAsync(matchId, updateMatch);
        }

        public async Task<UserMatch> GetMatchAsync(string matchId)
        {
            var team = await _usermatchRepository.GetItemAsync(matchId);
            return team;
        }

        public async Task<IList<UserMatch>> GetMatchesAsync()
        {
            var matches = await _usermatchRepository.GetListAsync();
            return Common.ConvertFirebaseObjectCollectionToList(matches);
        }

        public async Task DeleteAllMatchesAsync()
        {
            await _usermatchRepository.DeleteAsync();
        }

        public async Task DeleteMatchAsync(string matchId)
        {
            await _usermatchRepository.DeleteByIdAsync(matchId);
        }
    }
}
