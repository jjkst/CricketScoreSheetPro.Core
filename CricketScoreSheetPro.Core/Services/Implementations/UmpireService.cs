using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class UmpireService : IUmpireService
    {
        private readonly IRepository<Umpire> _umpireRepository;

        public UmpireService(IRepository<Umpire> umpireRepository)
        {
            _umpireRepository = umpireRepository ?? throw new ArgumentNullException($"UmpireRepository is null");
        }

        public string AddUmpire(Umpire newumpire)
        {
            if (newumpire == null) throw new ArgumentNullException($"Umpire is null");
            var umpireAddKey =  _umpireRepository.Create(newumpire);
            return umpireAddKey;
        }

        public void DeleteAllUmpires()
        {
             _umpireRepository.Delete();
        }

        public void DeleteUmpire(string umpireId)
        {
            if (string.IsNullOrEmpty(umpireId)) throw new ArgumentException($"Umpire ID is null");
             _umpireRepository.DeleteById(umpireId);
        }

        public Umpire GetUmpire(string umpireId)
        {
            if (string.IsNullOrEmpty(umpireId)) throw new ArgumentException($"Umpire ID is null");
            var umpire =  _umpireRepository.GetItem(umpireId);
            return umpire;
        }

        public IList<Umpire> GetUmpires()
        {
            var umpires =  _umpireRepository.GetList();
            return umpires;
        }

        public void UpdateUmpire(string umpireId, Umpire updateumpire)
        {
            if (updateumpire == null) throw new ArgumentNullException($"UserUmpire is null");
            if (string.IsNullOrEmpty(umpireId)) throw new ArgumentException($"Umpire ID is null");
             _umpireRepository.CreateWithId(umpireId, updateumpire);
        }
    }
}
