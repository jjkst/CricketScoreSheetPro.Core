using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<Player> _playerRepository;
        private readonly IRepository<TeamPlayer> _teamplayerRepository;

        public PlayerService(IRepository<Player> playerRepository, IRepository<TeamPlayer> teamplayerRepository)
        {
            _playerRepository = playerRepository;
            _teamplayerRepository = teamplayerRepository;
        }

        public async Task<Player> AddPlayerAsync(Player newPlayer)
        {
            var playerAdd = await _playerRepository.CreateAsync(newPlayer);
            var newteamplayer = new TeamPlayer
            {
                PlayerId = playerAdd.Key,
                PlayerName = playerAdd.Object.Name,
                Roles = playerAdd.Object.Roles
            };
            await _teamplayerRepository.CreateWithIdAsync(playerAdd.Key, newteamplayer);
            return playerAdd.Object;
        }

        public async Task UpdatePlayerAsync(string playerId, Player updatePlayer)
        {
            await _playerRepository.CreateWithIdAsync(playerId, updatePlayer);
            var updateteamplayer = new TeamPlayer
            {
                PlayerId = playerId,
                PlayerName = updatePlayer.Name,
                Roles = updatePlayer.Roles
            };
            await _teamplayerRepository.CreateWithIdAsync(playerId, updateteamplayer);
        }

        public async Task<Player> GetPlayerAsync(string playerId)
        {
            var team = await _playerRepository.GetItemAsync(playerId);
            return team;
        }

        public async Task<IList<TeamPlayer>> GetPlayersPerTeamAsync()
        {
            var teamplayers = await _teamplayerRepository.GetListAsync();
            return Common.ConvertFirebaseObjectCollectionToList(teamplayers);
        }

        public async Task<IList<Player>> GetPlayersForAllUserTeamsAsync(IRepository<UserTeam> userteamRepository)
        {
            var userteams = await userteamRepository.GetListAsync();
            var players = new List<Player>();
            foreach (var team in userteams)
            {
                var teamplayers = await _playerRepository.GetFilteredListAsync("TeamId", team.Key);
                players.AddRange(Common.ConvertFirebaseObjectCollectionToList(teamplayers));
            }
            return players;
        }

        public async Task DeleteAllPlayersAsync()
        {
            await _playerRepository.DeleteAsync();
            await _teamplayerRepository.DeleteAsync();
        }

        public async Task DeletePlayerAsync(string playerId)
        {
            await _playerRepository.DeleteByIdAsync(playerId);
            await _teamplayerRepository.DeleteByIdAsync(playerId);
        }
    }
}
