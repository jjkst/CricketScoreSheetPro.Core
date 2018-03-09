using CricketScoreSheetPro.Core.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class UmpireRepository : BaseRepository<Umpire>
    {
        public UmpireRepository(FirebaseClient client, string uuid)
        {
            _reference = client.Child("User")
                               .Child(uuid)
                               .Child("Umpire");
        }
    }
}
