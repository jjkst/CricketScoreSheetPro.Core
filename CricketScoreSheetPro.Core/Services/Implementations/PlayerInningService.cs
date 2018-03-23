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

        public string AddPlayerInnings(PlayerInning newplayerinning)
        {
            if (newplayerinning == null) throw new ArgumentNullException($"PlayerInning is null");
            var playerinningaddedKey = _playerinningsRepository.Create(newplayerinning);      
            return playerinningaddedKey;
        }

        public void UpdatePlayerInning(string playerInningsId, PlayerInning playerinning)
        {
            if (playerinning == null) throw new ArgumentNullException($"PlayerInning is null");
            if (string.IsNullOrEmpty(playerInningsId)) throw new ArgumentException($"PlayerInning ID is null");
            _playerinningsRepository.CreateWithId(playerInningsId, playerinning);
        }

        public PlayerInning GetPlayerInning(string playerInningsId)
        {
            if (string.IsNullOrEmpty(playerInningsId)) throw new ArgumentException($"PlayerInning ID is null");
            var playerInning = _playerinningsRepository.GetItem(playerInningsId);
            return playerInning;
        }

        public IList<PlayerInning> GetPlayerInnings(string playerId)
        {
            if (string.IsNullOrEmpty(playerId)) throw new ArgumentException($"PlayerID is null");
            var playerInnings = _playerinningsRepository.
                GetFilteredList(nameof(PlayerInning.PlayerId), playerId);
            return playerInnings;
        }

        public IList<PlayerInning> GetPlayerInningsByTournamentId(string playerId, string tournamentId)
        {
            if (string.IsNullOrEmpty(playerId)) throw new ArgumentException($"PlayerID is null");
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"TournamentId is null");
            var playerInnings = _playerinningsRepository.
                GetFilteredList(nameof(PlayerInning.Player_TournamentId), $"{playerId}_{tournamentId}");
            return playerInnings;
        }

        public IList<PlayerInning> GetAllPlayerInningsByTeamMatchId(string teamId, string matchId) //Used for match
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"TeamID is null");
            if (string.IsNullOrEmpty(matchId)) throw new ArgumentException($"MatchId is null");
            var playerInnings = _playerinningsRepository.
                GetFilteredList(nameof(PlayerInning.Team_MatchId),
                $"{teamId}_{matchId}");
            return playerInnings;
        }

        public void DeleteAllPlayerInnings()
        {
            _playerinningsRepository.Delete();
        }

        public void DeletePlayerInning(string playerInningsId)
        {
            if (string.IsNullOrEmpty(playerInningsId)) throw new ArgumentException($"PlayerInning ID is null");
            _playerinningsRepository.DeleteById(playerInningsId);
        }
    }
}
