using CricketScoreSheetPro.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services
{
    public class TeamService : BaseService<Team> , IService<Team>
    {  
        public TeamService()
        {
            _reference = Client.Child("Teams");
        }       
    }
}
