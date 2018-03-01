using CricketScoreSheetPro.Core.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class UserTeamRepository : BaseRepository<UserTeam>
    {
        public UserTeamRepository(FirebaseClient client, string uuid)
        {
            _reference = client.Child("Users")
                               .Child(uuid)
                               .Child("UserTeams");
        }
    }
}
