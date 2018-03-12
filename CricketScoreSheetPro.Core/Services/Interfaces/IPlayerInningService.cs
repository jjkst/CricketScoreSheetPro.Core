using CricketScoreSheetPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Interfaces
{
    public interface IPlayerInningService
    {
        Task<PlayerInning> AddPlayerInningsAsync(PlayerInning newplayerinning);
        Task UpdatePlayerInningAsync(string playerinningId, PlayerInning updatePlayerInning);
        Task<PlayerInning> GetPlayerInningAsync(string playerinningId);
        Task<IList<PlayerInning>> GetPlayerInningsAsync(string playerId);
        Task<IList<PlayerInning>> GetPlayerInningsByTournamentIdAsync(string playerId, string tournamentId);
        Task<IList<PlayerInning>> GetAllPlayerInningsByTeamMatchIdAsync(string teamId, string matchId); //Used for Match
        Task DeleteAllPlayerInningsAsync();
        Task DeletePlayerInningAsync(string playerinningId);
    }
}
