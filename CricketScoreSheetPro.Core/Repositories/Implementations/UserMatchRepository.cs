using CricketScoreSheetPro.Core.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class UserMatchRepository : BaseRepository<UserMatch>
    {
        public UserMatchRepository(FirebaseClient client, string uuid, bool importFlg)
        {
            _reference = client.Child(importFlg ? "Imports" : "Users")
                                    .Child(uuid)
                                    .Child("UserMatches");
        }

    }
}
