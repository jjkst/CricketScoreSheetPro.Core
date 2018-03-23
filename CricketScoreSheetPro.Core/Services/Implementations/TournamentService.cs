using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class TournamentService : ITournamentService
    {
        private readonly IRepository<Tournament> _tournamentRepository;
        private readonly IRepository<TournamentDetail> _tournamentdetailRepository;

        public TournamentService(IRepository<Tournament> tournamentRepository, IRepository<TournamentDetail> tournamentdetailRepository)
        {
            _tournamentRepository = tournamentRepository ?? throw new ArgumentNullException($"TournamentRepository is null");
            _tournamentdetailRepository = tournamentdetailRepository ?? throw new ArgumentNullException($"TournamentDetailRepository is null");
        }

        public Tournament AddTournament(Tournament newTournament)
        {
            if (newTournament == null) throw new ArgumentNullException($"Tournament is null");
            var tournamentAdd =  _tournamentRepository.Create(newTournament);
            var newtournamentdetail = new TournamentDetail
            {
                Name = tournamentAdd.Name,
                Status = tournamentAdd.Status,
                StartDate = tournamentAdd.AddDate
            };
             _tournamentdetailRepository.CreateWithId(tournamentAdd.Id, newtournamentdetail);
            return tournamentAdd;
        }

        public void UpdateTournament(string tournamentId, TournamentDetail updateTournamentDetail)
        {
            if (updateTournamentDetail == null) throw new ArgumentNullException($"Tournament is null");
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"Tournament ID is null");
            var updatetournament = new Tournament
            {
                Name = updateTournamentDetail.Name,
                Status = updateTournamentDetail.Status
            };
             _tournamentRepository.CreateWithId(tournamentId, updatetournament);
             _tournamentdetailRepository.CreateWithId(tournamentId, updateTournamentDetail);
        }

        public void UpdateTournamentProperty(string id, string fieldName, object val)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException($"Tournament ID is null");
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentException($"Tournament property is null");
            if (val == null) throw new ArgumentException($"Tournament property value is null");
            if (fieldName.ToLower() == "name")  _tournamentRepository.Update(id, "TournamentName", val);
            if (fieldName.ToLower() == "status")  _tournamentRepository.Update(id, "Status", val);
             _tournamentdetailRepository.Update(id, fieldName, val);
        }

        public TournamentDetail GetTournamentDetail(string tournamentId)
        {
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"Tournament ID is null");
            var tournament =  _tournamentdetailRepository.GetItem(tournamentId);
            return tournament;
        }

        public IList<Tournament> GetTournaments()
        {
            var usertournaments =  _tournamentRepository.GetList();
            return usertournaments;
        }

        public void DeleteAllTournaments()
        {
             _tournamentRepository.Delete();
             _tournamentdetailRepository.Delete();
        }

        public void DeleteTournament(string tournamentId)
        {
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"Tournament ID is null");
             _tournamentRepository.DeleteById(tournamentId);
             _tournamentdetailRepository.DeleteById(tournamentId);
        }
    }
}
