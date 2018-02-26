using CricketScoreSheetPro.Core.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class UserTeamRepository : BaseRepository<UserTeam>
    {
        public UserTeamRepository(FirebaseClient client, string uuid, bool importFlg)
        {
            _reference = client.Child(importFlg ? "Imports" : "Users")
                    .Child(uuid)
                    .Child("UserTeams");
        }
    }
}
