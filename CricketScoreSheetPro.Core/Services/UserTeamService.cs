using CricketScoreSheetPro.Core.Models;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services
{
    public class UserTeamService : BaseService<UserTeam>, IService<UserTeam>
    {
        public IList<UserTeam> ExistingUserTeams => GetList().Result;

        public UserTeamService(string uuid, bool importFlg = false)
        {
            _reference = Client.Child(importFlg ? "Imports" : "Users")
                    .Child(uuid)
                    .Child("UserTeams");
        }
    }
}
