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
        string AddPlayerInnings(PlayerInning newplayerinning);
        void UpdatePlayerInning(string playerinningId, PlayerInning updatePlayerInning);
        PlayerInning GetPlayerInning(string playerinningId);
        IList<PlayerInning> GetPlayerInnings(string playerId);
        IList<PlayerInning> GetPlayerInningsByTournamentId(string playerId, string tournamentId);
        IList<PlayerInning> GetAllPlayerInningsByTeamMatchId(string teamId, string matchId); //Used for Match
        void DeleteAllPlayerInnings();
        void DeletePlayerInning(string playerinningId);
    }
}
