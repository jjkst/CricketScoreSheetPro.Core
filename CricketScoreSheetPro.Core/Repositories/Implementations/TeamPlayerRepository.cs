using CricketScoreSheetPro.Core.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class TeamPlayerRepository : BaseRepository<TeamPlayer>
    {
        public TeamPlayerRepository(FirebaseClient client, string teamId)
        {
            _reference = client.Child("Teams").Child(teamId).Child("Players");
        }
    }
}
