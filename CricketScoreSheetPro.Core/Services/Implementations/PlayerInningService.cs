﻿using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class PlayerInningService : IPlayerService
    {
        private readonly IRepository<TeamDetail> _teamdetailRepository;
        private readonly IRepository<PlayerInning> _playerinningsRepository;

        public PlayerInningService(IRepository<TeamDetail> teamdetailRepository, IRepository<PlayerInning> playerinningsRepository)
        {
            _teamdetailRepository = teamdetailRepository ?? throw new ArgumentNullException($"TeamDetailRepository is null");
            _playerinningsRepository = playerinningsRepository ?? throw new ArgumentNullException($"PlayerInningRepository is null");
        }

        public async Task<IList<PlayerInning>> AddPlayerInningAsync(string teamId, string matchId, string tournamentId = "")
        {
            var teamdetail = await _teamdetailRepository.GetItemAsync(teamId);
            var playersadded = new List<PlayerInning>(); 
            foreach(var player in teamdetail.Players)
            {
                var newteaminning = new PlayerInning
                {
                    PlayerId = player.Id,
                    PlayerName = player.Name,
                    TeamId = teamId,
                    MatchId = matchId,
                    TournamentId = tournamentId
                };

                playersadded.Add(await _playerinningsRepository.CreateAsync(newteaminning));
            }            
            return playersadded;
        }

        public async Task UpdatePlayerInningAsync(string playerInningsId, PlayerInning playerinning)
        {
            if (playerinning == null) throw new ArgumentNullException($"PlayerInning is null");
            if (string.IsNullOrEmpty(playerInningsId)) throw new ArgumentException($"PlayerInning ID is null");
            await _playerinningsRepository.CreateWithIdAsync(playerInningsId, playerinning);
        }

        public async Task<PlayerInning> GetTeamInningAsync(string playerInningsId)
        {
            if (string.IsNullOrEmpty(playerInningsId)) throw new ArgumentException($"PlayerInning ID is null");
            var playerInning = await _playerinningsRepository.GetItemAsync(playerInningsId);
            return playerInning;
        }

        public async Task<IList<PlayerInning>> GetTeamInningsAsync()
        {
            var playerInnings = await _playerinningsRepository.GetListAsync();
            return playerInnings;
        }

        public async Task DeleteAllPlayerInningsInningsAsync()
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
