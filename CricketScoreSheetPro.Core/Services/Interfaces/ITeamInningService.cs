using CricketScoreSheetPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Interfaces
{
    public interface ITeamInningService
    {
        TeamInning AddTeamInning(TeamInning newteaminning);
        void UpdateTeamInning(string TeaminningId, TeamInning updateTeamInning);
        TeamInning GetTeamInning(string TeaminningId);
        IList<TeamInning> GetTeamInnings(string teamId);
        IList<TeamInning> GetTeamInningsByTournamentId(string teamId, string tournamentId);
        void DeleteAllTeamInnings();
        void DeleteTeamInning(string TeaminningId);
    }
}
