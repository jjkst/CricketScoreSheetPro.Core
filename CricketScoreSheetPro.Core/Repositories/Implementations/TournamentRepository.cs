using CricketScoreSheetPro.Core.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
