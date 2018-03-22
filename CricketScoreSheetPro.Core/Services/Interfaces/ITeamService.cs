using CricketScoreSheetPro.Core.Models;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Services.Interfaces
{
    public interface ITeamService
    {
        Team AddTeam(Team newTeam);
        void UpdateTeam(string teamId, TeamDetail updateTeam);
        void UpdateTeamProperty(string id, string fieldName, object val);
        TeamDetail GetTeamDetail(string teamId);
        IList<Team> GetTeams();
        void DeleteAllTeams();
        void DeleteTeam(string teamId);
    }
}
