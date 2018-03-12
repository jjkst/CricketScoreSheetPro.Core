using CricketScoreSheetPro.Core.Models;
using Firebase.Database;

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
