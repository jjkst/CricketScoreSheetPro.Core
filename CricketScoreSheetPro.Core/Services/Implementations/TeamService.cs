using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
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
            _teamRepository = teamRepository;
            _userteamRepository = userteamRepository;
        }

        public async Task<Team> AddTeamAsync(Team newTeam)
        {
            var teamAdd = await _teamRepository.CreateAsync(newTeam);
            var newuserteam = new UserTeam
            {
                TeamId = teamAdd.Key,
                TeamName = teamAdd.Object.Name,
                TournamentId = teamAdd.Object.TournamentId
            };
            await _userteamRepository.CreateWithIdAsync(teamAdd.Key, newuserteam);
            return teamAdd.Object;
        }

        public async Task UpdateTeamAsync(string teamId, Team updateTeam)
        {
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
            var team = await _teamRepository.GetItemAsync(teamId);
            return team;
        }

        public async Task<IList<UserTeam>> GetTeamsAsync()
        {
            var userteams = await _userteamRepository.GetListAsync();
            return Common.ConvertFirebaseObjectCollectionToList(userteams);
        }

        public async Task DeleteAllTeamsAsync()
        {
            await _teamRepository.DeleteAsync();
            await _userteamRepository.DeleteAsync();
        }

        public async Task DeleteTeamAsync(string teamId)
        {
            await _teamRepository.DeleteByIdAsync(teamId);
            await _userteamRepository.DeleteByIdAsync(teamId);
        }
    }
}
