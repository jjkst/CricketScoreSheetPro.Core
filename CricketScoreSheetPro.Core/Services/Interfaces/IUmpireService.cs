using CricketScoreSheetPro.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Interfaces
{
    public interface IUmpireService
    {
        Task<Umpire> AddUmpireAsync(Umpire newumpire);
        Task UpdateUmpireAsync(string umpireId, Umpire updateumpire);
        Task<Umpire> GetUmpireAsync(string umpireId);
        Task<IList<Umpire>> GetUmpiresAsync();
        Task DeleteAllUmpiresAsync();
        Task DeleteUmpireAsync(string umpireId);
    }
}
