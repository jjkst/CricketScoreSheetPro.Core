using CricketScoreSheetPro.Core.Models;
using Firebase.Database;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class TeamDetailRepository : BaseRepository<TeamDetail>
    {
        public TeamDetailRepository(FirebaseClient client)
        {
            _reference = client.Child("TeamDetail");
        }
    }
}
