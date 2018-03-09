using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class UmpireService : IUmpireService
    {
        private readonly IRepository<Umpire> _umpireRepository;

        public UmpireService(IRepository<Umpire> umpireRepository)
        {
            _umpireRepository = umpireRepository ?? throw new ArgumentNullException($"UmpireRepository is null");
        }

        public async Task<Umpire> AddUmpireAsync(Umpire newumpire)
        {
            if (newumpire == null) throw new ArgumentNullException($"Umpire is null");
            var umpireAdd = await _umpireRepository.CreateAsync(newumpire);
            return umpireAdd;
        }

        public async Task DeleteAllUmpiresAsync()
        {
            await _umpireRepository.DeleteAsync();
        }

        public async Task DeleteUmpireAsync(string umpireId)
        {
            if (string.IsNullOrEmpty(umpireId)) throw new ArgumentException($"Umpire ID is null");
            await _umpireRepository.DeleteByIdAsync(umpireId);
        }

        public async Task<Umpire> GetUmpireAsync(string umpireId)
        {
            if (string.IsNullOrEmpty(umpireId)) throw new ArgumentException($"Umpire ID is null");
            var umpire = await _umpireRepository.GetItemAsync(umpireId);
            return umpire;
        }

        public async Task<IList<Umpire>> GetUmpiresAsync()
        {
            var umpires = await _umpireRepository.GetListAsync();
            return umpires;
        }

        public async Task UpdateUmpireAsync(string umpireId, Umpire updateumpire)
        {
            if (updateumpire == null) throw new ArgumentNullException($"UserUmpire is null");
            if (string.IsNullOrEmpty(umpireId)) throw new ArgumentException($"Umpire ID is null");
            await _umpireRepository.CreateWithIdAsync(umpireId, updateumpire);
        }
    }
}
