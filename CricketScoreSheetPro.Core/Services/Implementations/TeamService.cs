using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<UserTeam> _userteamRepository;

        public TeamService(IRepository<Team> teamRepository, IRepository<UserTeam> userteamRepository)
        {
            _teamRepository = teamRepository ?? throw new ArgumentNullException($"TeamRepository is null"); 
            _userteamRepository = userteamRepository ?? throw new ArgumentNullException($"UserTeamRepository is null");
        }

        public async Task<Team> AddTeamAsync(Team newTeam)
        {
            if (newTeam == null) throw new ArgumentNullException($"Team is null");
            var teamAdd = await _teamRepository.CreateAsync(newTeam);
            var newuserteam = new UserTeam
            {
                TeamId = teamAdd.Id,
                TeamName = teamAdd.Name,
                TournamentId = teamAdd.TournamentId
            };
            await _userteamRepository.CreateWithIdAsync(teamAdd.Id, newuserteam);
            return teamAdd;
        }

        public async Task UpdateTeamAsync(string teamId, Team updateTeam)
        {
            if (updateTeam == null) throw new ArgumentNullException($"Team is null");
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"Team ID is null");
            await _teamRepository.CreateWithIdAsync(teamId, updateTeam);
            var updateuserteam = new UserTeam
            {
                TeamId = teamId,
                TeamName = updateTeam.Name,
                TournamentId = updateTeam.TournamentId
            };
            await _userteamRepository.CreateWithIdAsync(teamId, updateuserteam);
        }

        public async Task<Team> GetTeamAsync(string teamId)
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"Team ID is null");
            var team = await _teamRepository.GetItemAsync(teamId);
            return team;
        }

        public async Task<IList<UserTeam>> GetUserTeamsAsync()
        {
            var userteams = await _userteamRepository.GetListAsync();
            return userteams;
        }

        public async Task DeleteAllTeamsAsync()
        {
            await _teamRepository.DeleteAsync();
            await _userteamRepository.DeleteAsync();
        }

        public async Task DeleteTeamAsync(string teamId)
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"Team ID is null");
            await _teamRepository.DeleteByIdAsync(teamId);
            await _userteamRepository.DeleteByIdAsync(teamId);
        }
    }
}
