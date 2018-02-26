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
        Task<UserMatch> AddMatchAsync(UserMatch newmatch);
        Task UpdateMatchAsync(string matchId, UserMatch updateMatch);
        Task<UserMatch> GetMatchAsync(string matchId);
        Task<IList<UserMatch>> GetMatchesAsync();
        Task DeleteAllMatchesAsync();
        Task DeleteMatchAsync(string matchId);
    }
}
