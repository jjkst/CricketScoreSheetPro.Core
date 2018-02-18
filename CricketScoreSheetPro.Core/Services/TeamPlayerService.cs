using CricketScoreSheetPro.Core.Models;
using Firebase.Database.Query;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Services
{
    public class TeamPlayerService : BaseService<TeamPlayer>, IService<TeamPlayer>
    {
        public IList<TeamPlayer> ExistingTeamPlayers => GetList().Result;

        public TeamPlayerService(string teamId)
        {
            _reference = Client.Child("Teams").Child(teamId).Child("Players");
        }
    }
}
