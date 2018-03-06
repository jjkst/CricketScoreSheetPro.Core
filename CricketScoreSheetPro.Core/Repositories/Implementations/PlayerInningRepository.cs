using CricketScoreSheetPro.Core.Models;
using Firebase.Database;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class PlayerInningRepository : BaseRepository<PlayerInning>
    {
        public PlayerInningRepository(FirebaseClient client)
        {
            _reference = client.Child("PlayerInning");
        }
    }
}
