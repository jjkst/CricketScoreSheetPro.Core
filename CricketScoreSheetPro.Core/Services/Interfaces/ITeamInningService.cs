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
        Task<TeamInning> AddTeamInningAsync(TeamInning newteaminning);
        Task UpdateTeamInningAsync(string TeaminningId, TeamInning updateTeamInning);
        Task<TeamInning> GetTeamInningAsync(string TeaminningId);
        Task<IList<TeamInning>> GetTeamInningsAsync(string teamId);
        Task<IList<TeamInning>> GetTeamInningsByTournamentIdAsync(string teamId, string tournamentId);
        Task DeleteAllTeamInningsAsync();
        Task DeleteTeamInningAsync(string TeaminningId);
    }
}
