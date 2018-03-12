using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class PlayerInningService : IPlayerInningService
    {
        private readonly IRepository<PlayerInning> _playerinningsRepository;

        public PlayerInningService(IRepository<PlayerInning> playerinningsRepository)
        {
            _playerinningsRepository = playerinningsRepository ?? throw new ArgumentNullException($"PlayerInningRepository is null");
        }

        public async Task<PlayerInning> AddPlayerInningsAsync(PlayerInning newplayerinning)
        {
            if (newplayerinning == null) throw new ArgumentNullException($"PlayerInning is null");
            var playerinningadded = await _playerinningsRepository.CreateAsync(newplayerinning);      
            return playerinningadded;
        }

        public async Task UpdatePlayerInningAsync(string playerInningsId, PlayerInning playerinning)
        {
            if (playerinning == null) throw new ArgumentNullException($"PlayerInning is null");
            if (string.IsNullOrEmpty(playerInningsId)) throw new ArgumentException($"PlayerInning ID is null");
            await _playerinningsRepository.CreateWithIdAsync(playerInningsId, playerinning);
        }

        public async Task<PlayerInning> GetPlayerInningAsync(string playerInningsId)
        {
            if (string.IsNullOrEmpty(playerInningsId)) throw new ArgumentException($"PlayerInning ID is null");
            var playerInning = await _playerinningsRepository.GetItemAsync(playerInningsId);
            return playerInning;
        }

        public async Task<IList<PlayerInning>> GetPlayerInningsAsync(string playerId)
        {
            if (string.IsNullOrEmpty(playerId)) throw new ArgumentException($"PlayerID is null");
            var playerInnings = await _playerinningsRepository.
                GetFilteredListAsync(nameof(PlayerInning.PlayerId), playerId);
            return playerInnings;
        }

        public async Task<IList<PlayerInning>> GetPlayerInningsByTournamentIdAsync(string playerId, string tournamentId)
        {
            if (string.IsNullOrEmpty(playerId)) throw new ArgumentException($"PlayerID is null");
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"TournamentId is null");
            var playerInnings = await _playerinningsRepository.
                GetFilteredListAsync(nameof(PlayerInning.Player_TournamentId), $"{playerId}_{tournamentId}");
            return playerInnings;
        }

        public async Task<IList<PlayerInning>> GetAllPlayerInningsByTeamMatchIdAsync(string teamId, string matchId) //Used for match
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"TeamID is null");
            if (string.IsNullOrEmpty(matchId)) throw new ArgumentException($"MatchId is null");
            var playerInnings = await _playerinningsRepository.
                GetFilteredListAsync(nameof(PlayerInning.Team_MatchId),
                $"{teamId}_{matchId}");
            return playerInnings;
        }

        public async Task DeleteAllPlayerInningsAsync()
        {
            await _playerinningsRepository.DeleteAsync();
        }

        public async Task DeletePlayerInningAsync(string playerInningsId)
        {
            if (string.IsNullOrEmpty(playerInningsId)) throw new ArgumentException($"PlayerInning ID is null");
            await _playerinningsRepository.DeleteByIdAsync(playerInningsId);
        }
    }
}
