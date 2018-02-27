using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class MatchService : IMatchService
    {
        private readonly IRepository<UserMatch> _usermatchRepository;

        public MatchService(IRepository<UserMatch> usermatchRepository)
        {
            _usermatchRepository = usermatchRepository ?? throw new ArgumentNullException($"UserMatchRepository is null");
        }

        public async Task<UserMatch> AddMatchAsync(UserMatch newmatch)
        {
            if (newmatch == null) throw new ArgumentNullException($"UserMatch is null");
            var matchAdd = await _usermatchRepository.CreateAsync(newmatch);
            return matchAdd;
        }

        public async Task UpdateMatchAsync(string matchId, UserMatch updateMatch)
        {
            if (updateMatch == null) throw new ArgumentNullException($"UserMatch is null");
            if (string.IsNullOrEmpty(matchId)) throw new ArgumentException($"Match ID is null");
            await _usermatchRepository.CreateWithIdAsync(matchId, updateMatch);
        }

        public async Task<UserMatch> GetMatchAsync(string matchId)
        {
            if (string.IsNullOrEmpty(matchId)) throw new ArgumentException($"Match ID is null");
            var team = await _usermatchRepository.GetItemAsync(matchId);
            return team;
        }

        public async Task<IList<UserMatch>> GetMatchesAsync()
        {
            var matches = await _usermatchRepository.GetListAsync();
            return matches;
        }

        public async Task DeleteAllMatchesAsync()
        {
            await _usermatchRepository.DeleteAsync();
        }

        public async Task DeleteMatchAsync(string matchId)
        {
            if (string.IsNullOrEmpty(matchId)) throw new ArgumentException($"Match ID is null");
            await _usermatchRepository.DeleteByIdAsync(matchId);
        }
    }
}
