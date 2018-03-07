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
        private readonly IRepository<TeamDetail> _teamdetailRepository;

        public TeamService(IRepository<Team> teamRepository, IRepository<TeamDetail> teamdetailRepository)
        {
            _teamRepository = teamRepository ?? throw new ArgumentNullException($"TeamRepository is null");
            _teamdetailRepository = teamdetailRepository ?? throw new ArgumentNullException($"TeamDetailRepository is null");
        }

        public async Task<Team> AddTeamAsync(Team newTeam)
        {
            if (newTeam == null) throw new ArgumentNullException($"Team is null");
            var teamAdd = await _teamRepository.CreateAsync(newTeam);
            var newuserteam = new TeamDetail
            {
                Name = teamAdd.Name
            };
            await _teamdetailRepository.CreateWithIdAsync(teamAdd.Id, newuserteam);
            return teamAdd;
        }

        public async Task UpdateTeamAsync(string teamId, TeamDetail updateTeamDetail)
        {
            if (updateTeamDetail == null) throw new ArgumentNullException($"Team is null");
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"Team ID is null");            
            var updateteam = new Team
            {
                Name = updateTeamDetail.Name
            };
            await _teamRepository.CreateWithIdAsync(teamId, updateteam);
            await _teamdetailRepository.CreateWithIdAsync(teamId, updateTeamDetail);
        }

        public async Task UpdateTeamPropertyAsync(string id, string fieldName, object val)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Team ID is null");
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentException($"Team property is null");
            if (val == null) throw new ArgumentException($"Team property value is null");
            if (fieldName.ToLower() == "name") await _teamRepository.UpdateAsync(id, "TeamName", val);
            await _teamdetailRepository.UpdateAsync(id, fieldName, val);
        }

        public async Task<TeamDetail> GetTeamDetailAsync(string teamId)
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"Team ID is null");
            var team = await _teamdetailRepository.GetItemAsync(teamId);
            return team;
        }

        public async Task<IList<Team>> GetTeamsAsync()
        {
            var userteams = await _teamRepository.GetListAsync();
            return userteams;
        }

        public async Task DeleteAllTeamsAsync()
        {
            await _teamRepository.DeleteAsync();
            await _teamdetailRepository.DeleteAsync();
        }

        public async Task DeleteTeamAsync(string teamId)
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"Team ID is null");
            await _teamRepository.DeleteByIdAsync(teamId);
            await _teamdetailRepository.DeleteByIdAsync(teamId);
        }
    }
}
