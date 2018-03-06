using CricketScoreSheetPro.Core.Models;
using Firebase.Database;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class TournamentDetailRepository : BaseRepository<TournamentDetail>
    {
        public TournamentDetailRepository(FirebaseClient client)
        {
            _reference = client.Child("TournamentDetail");
        }
    }
}
