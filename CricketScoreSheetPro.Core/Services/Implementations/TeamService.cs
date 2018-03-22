using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;

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

        public Team AddTeam(Team newTeam)
        {
            if (newTeam == null) throw new ArgumentNullException($"Team is null");
            var teamAdd =  _teamRepository.Create(newTeam);
            var newuserteam = new TeamDetail
            {
                Name = teamAdd.Name
            };
            _teamdetailRepository.CreateWithId(teamAdd.Id, newuserteam);
            return teamAdd;
        }

        public void UpdateTeam(string teamId, TeamDetail updateTeamDetail)
        {
            if (updateTeamDetail == null) throw new ArgumentNullException($"Team is null");
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"Team ID is null");            
            var updateteam = new Team
            {
                Name = updateTeamDetail.Name
            };
             _teamRepository.CreateWithId(teamId, updateteam);
             _teamdetailRepository.CreateWithId(teamId, updateTeamDetail);
        }

        public void UpdateTeamProperty(string id, string fieldName, object val)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Team ID is null");
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentException($"Team property is null");
            if (val == null) throw new ArgumentException($"Team property value is null");
            if (fieldName.ToLower() == "name")  _teamRepository.Update(id, "TeamName", val);
             _teamdetailRepository.Update(id, fieldName, val);
        }

        public TeamDetail GetTeamDetail(string teamId)
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"Team ID is null");
            var team =  _teamdetailRepository.GetItem(teamId);
            return team;
        }

        public IList<Team> GetTeams()
        {
            var userteams =  _teamRepository.GetList();
            return userteams;
        }

        public void DeleteAllTeams()
        {
             _teamRepository.Delete();
             _teamdetailRepository.Delete();
        }

        public void DeleteTeam(string teamId)
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"Team ID is null");
             _teamRepository.DeleteById(teamId);
             _teamdetailRepository.DeleteById(teamId);
        }
    }
}
