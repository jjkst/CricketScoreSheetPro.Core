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
        Task UpdateTeamAsync(string teamId, Team updateTeam);
        Task<Team> GetTeamAsync(string teamId);
        Task<IList<UserTeam>> GetUserTeamsAsync();
        Task<IList<UserTeam>> GetUserTeamsPerTournamentAsync(string tournamentId);
        Task DeleteAllTeamsAsync();
        Task DeleteTeamAsync(string teamId);
    }
}
