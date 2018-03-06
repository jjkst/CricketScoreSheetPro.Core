using CricketScoreSheetPro.Core.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class TeamInningRepository : BaseRepository<Player>
    {
        public TeamInningRepository(FirebaseClient client, string teamId)
        {
            _reference = client.Child("TeamInning");
        }
    }
}
