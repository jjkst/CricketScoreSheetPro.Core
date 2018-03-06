using CricketScoreSheetPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Interfaces
{
    public interface ITeamInningService
    {
        Task<TeamInning> AddTeamInningAsync(string teamId, string matchId, string tournamentId = "");
        Task UpdateTeamInningAsync(string TeaminningId, TeamInning updateTeamInning);
        Task<TeamInning> GetTeamInningAsync(string TeaminningId);
        Task<IList<TeamInning>> GetTeamInningsAsync();
        Task DeleteAllTeamInningsAsync();
        Task DeleteTeamInningAsync(string TeaminningId);
    }
}
