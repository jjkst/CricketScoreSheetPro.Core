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

        public string AddMatch(Match newmatch)
        {
            if (newmatch == null) throw new ArgumentNullException($"Match is null");
            var matchAddKey = _matchRepository.Create(newmatch);
            return matchAddKey;
        }

        public void UpdateMatch(string matchId, Match updateMatch)
        {
            if (updateMatch == null) throw new ArgumentNullException($"UserMatch is null");
            if (string.IsNullOrEmpty(matchId)) throw new ArgumentException($"Match ID is null");
            _matchRepository.CreateWithId(matchId, updateMatch);
        }

        public Match GetMatch(string matchId)
        {
            if (string.IsNullOrEmpty(matchId)) throw new ArgumentException($"Match ID is null");
            var match = _matchRepository.GetItem(matchId);
            return match;
        }

        public IList<Match> GetMatches()
        {
            var matches = _matchRepository.GetList();
            return matches;
        }

        public void DeleteAllMatches()
        {
            _matchRepository.Delete();
        }

        public void DeleteMatch(string matchId)
        {
            if (string.IsNullOrEmpty(matchId)) throw new ArgumentException($"Match ID is null");
            _matchRepository.DeleteById(matchId);
        }
    }
}
