using CricketScoreSheetPro.Core.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class TeamRepository : BaseRepository<Team>
    {
        public TeamRepository(FirebaseClient client, string uuid)
        {
            _reference = client.Child("User")
                               .Child(uuid)
                               .Child("Team");
        }
    }
}
