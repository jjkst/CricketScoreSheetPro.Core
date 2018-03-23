using CricketScoreSheetPro.Core.Models;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Services.Interfaces
{
    public interface IUmpireService
    {
        string AddUmpire(Umpire newumpire);
        void UpdateUmpire(string umpireId, Umpire updateumpire);
        Umpire GetUmpire(string umpireId);
        IList<Umpire> GetUmpires();
        void DeleteAllUmpires();
        void DeleteUmpire(string umpireId);
    }
}
