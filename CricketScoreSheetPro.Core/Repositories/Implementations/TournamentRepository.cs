using CricketScoreSheetPro.Core.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class TournamentRepository : BaseRepository<Tournament>
    {
        public TournamentRepository(FirebaseClient client, string uuid)
        {
            _reference = client.Child("User")
                               .Child(uuid)
                               .Child("Tournament");
        }
    }
}
