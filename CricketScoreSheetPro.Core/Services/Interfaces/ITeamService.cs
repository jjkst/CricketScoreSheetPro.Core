using CricketScoreSheetPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Interfaces
{
    public interface ITeamService
    {
        Task<Team> AddTeamAsync(Team newTeam);
        Task UpdateTeamAsync(string teamId, TeamDetail updateTeam);
        Task UpdateTeamPropertyAsync(string id, string fieldName, object val);
        Task<TeamDetail> GetTeamDetailAsync(string teamId);
        Task<IList<Team>> GetTeamsAsync();
        Task DeleteAllTeamsAsync();
        Task DeleteTeamAsync(string teamId);
    }
}
