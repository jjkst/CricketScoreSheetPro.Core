using CricketScoreSheetPro.Core.Models;
using Firebase.Database;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class TeamRepository : BaseRepository<Team>
    {
        public TeamRepository(FirebaseClient client)
        {
            _reference = client.Child("Teams");
        }
    }
}
