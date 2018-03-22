using CricketScoreSheetPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Interfaces
{
    public interface IMatchService
    {
        Match AddMatch(Match newmatch);
        void UpdateMatch(string matchId, Match updateMatch);
        Match GetMatch(string matchId);
        IList<Match> GetMatches();
        void DeleteAllMatches();
        void DeleteMatch(string matchId);
    }
}
