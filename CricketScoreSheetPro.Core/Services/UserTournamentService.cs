using CricketScoreSheetPro.Core.Models;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services
{
    public class UserTournamentService : BaseService<UserTournament>, IService<UserTournament> 
    {
        public IList<UserTournament> ExistingUserTournaments => GetList().Result;

        public UserTournamentService(string uuid, bool importFlg)
        {
            _reference = Client.Child(importFlg ? "Imports" : "Users")
                            .Child(uuid)
                            .Child("UserTournaments");
        }
    }
}
