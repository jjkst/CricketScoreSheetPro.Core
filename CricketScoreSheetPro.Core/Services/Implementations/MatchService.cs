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
        private readonly IRepository<Match> _matchRepository;

        public MatchService(IRepository<Match> matchRepository)
        {
            _matchRepository = matchRepository ?? throw new ArgumentNullException($"MatchRepository is null");
        }

        public async Task<Match> AddMatchAsync(Match newmatch)
        {
            if (newmatch == null) throw new ArgumentNullException($"Match is null");
            var matchAdd = await _matchRepository.CreateAsync(newmatch);
            return matchAdd;
        }

        public async Task UpdateMatchAsync(string matchId, Match updateMatch)
        {
            if (updateMatch == null) throw new ArgumentNullException($"UserMatch is null");
            if (string.IsNullOrEmpty(matchId)) throw new ArgumentException($"Match ID is null");
            await _matchRepository.CreateWithIdAsync(matchId, updateMatch);
        }

        public async Task<Match> GetMatchAsync(string matchId)
        {
            if (string.IsNullOrEmpty(matchId)) throw new ArgumentException($"Match ID is null");
            var match = await _matchRepository.GetItemAsync(matchId);
            return match;
        }

        public async Task<IList<Match>> GetMatchesAsync()
        {
            var matches = await _matchRepository.GetListAsync();
            return matches;
        }

        public async Task DeleteAllMatchesAsync()
        {
            await _matchRepository.DeleteAsync();
        }

        public async Task DeleteMatchAsync(string matchId)
        {
            if (string.IsNullOrEmpty(matchId)) throw new ArgumentException($"Match ID is null");
            await _matchRepository.DeleteByIdAsync(matchId);
        }
    }
}
