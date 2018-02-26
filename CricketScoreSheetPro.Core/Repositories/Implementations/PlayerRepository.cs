using CricketScoreSheetPro.Core.Models;
using Firebase.Database;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class PlayerRepository : BaseRepository<Player>
    {
        public PlayerRepository(FirebaseClient client)
        {
            _reference = client.Child("Players");
        }
    }
}
