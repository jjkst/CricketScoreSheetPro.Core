using System.Collections.Generic;
using System.Threading.Tasks;
using CricketScoreSheetPro.Core.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace CricketScoreSheetPro.Core.Repositories.Implementations
{
    public class UserMatchRepository : BaseRepository<UserMatch>
    { 
        public UserMatchRepository(FirebaseClient client, string uuid)
        {
            _reference = client.Child("Users")
                               .Child(uuid)
                               .Child("UserMatches");
        }

    }
}
