using CricketScoreSheetPro.Core.Models;
using Firebase.Database;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class TournamentRepository : BaseRepository<Tournament>
    {
        public TournamentRepository(FirebaseClient client)
        {
            _reference = client.Child("Tournaments");
        }
    }
}
