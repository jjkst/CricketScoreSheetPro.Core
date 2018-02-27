using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
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
            _playerRepository = playerRepository ?? throw new ArgumentNullException($"PlayerRepository is null"); ;
            _teamplayerRepository = teamplayerRepository ?? throw new ArgumentNullException($"TeamPlayerRepository is null"); ;
        }

        public async Task<Player> AddPlayerAsync(Player newPlayer)
        {
            if (newPlayer == null) throw new ArgumentNullException($"Player is null");
            var playerAdd = await _playerRepository.CreateAsync(newPlayer);
            var newteamplayer = new TeamPlayer
            {
                PlayerId = playerAdd.Id,
                PlayerName = playerAdd.Name,
                Roles = playerAdd.Roles
            };
            await _teamplayerRepository.CreateWithIdAsync(playerAdd.Id, newteamplayer);
            return playerAdd;
        }

        public async Task UpdatePlayerAsync(string playerId, Player updatePlayer)
        {
            if (updatePlayer == null) throw new ArgumentNullException($"Player is null");
            if (string.IsNullOrEmpty(playerId)) throw new ArgumentException($"Player ID is null");
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
            if (string.IsNullOrEmpty(playerId)) throw new ArgumentException($"Player ID is null");
            var team = await _playerRepository.GetItemAsync(playerId);
            return team;
        }

        public async Task<IList<TeamPlayer>> GetPlayersPerTeamAsync()
        {
            var teamplayers = await _teamplayerRepository.GetListAsync();
            return teamplayers;
        }

        public async Task<IList<Player>> GetPlayersForAllUserTeamsAsync(IRepository<UserTeam> userteamRepository)
        {
            if (userteamRepository == null) throw new ArgumentNullException($"UserTeamRepo is null");
            var userteams = await userteamRepository.GetListAsync();
            var players = new List<Player>();
            foreach (var team in userteams)
            {
                var teamplayers = await _playerRepository.GetFilteredListAsync("TeamId", team.TeamId);
                players.AddRange(teamplayers);
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
            if (string.IsNullOrEmpty(playerId)) throw new ArgumentException($"Player ID is null");
            await _playerRepository.DeleteByIdAsync(playerId);
            await _teamplayerRepository.DeleteByIdAsync(playerId);
        }
    }
}
