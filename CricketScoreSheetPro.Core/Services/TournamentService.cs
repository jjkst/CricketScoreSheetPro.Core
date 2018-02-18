using CricketScoreSheetPro.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services
{
    public class TournamentService : BaseService<Tournament> , IService<Tournament>
    {        
        public TournamentService()
        {
            _reference = Client.Child("Tournaments");
        }
    }
}
