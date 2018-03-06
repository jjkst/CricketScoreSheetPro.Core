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
        Task<Match> AddMatchAsync(Match newmatch);
        Task UpdateMatchAsync(string matchId, Match updateMatch);
        Task<Match> GetMatchAsync(string matchId);
        Task<IList<Match>> GetMatchesAsync();
        Task DeleteAllMatchesAsync();
        Task DeleteMatchAsync(string matchId);
    }
}
