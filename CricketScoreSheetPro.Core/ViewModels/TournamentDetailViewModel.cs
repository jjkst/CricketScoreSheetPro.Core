using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public class TournamentDetailViewModel
    {
        private readonly ITournamentService _tournamentService;

        public TournamentDetailViewModel(ITournamentService tournamentService, string tournamentId)
        {
            _tournamentService = tournamentService;
            Tournament = _tournamentService.GetTournamentAsync(tournamentId).Result;
        }

        public Tournament Tournament { get; private set; }

        public void UpdateName(string name) => _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Name), name);
        public void UpdateSponsor(string sponsor) => _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Sponsor), sponsor);
        public void UpdateStatus(string status) => _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Status), status);
        public void UpdateStartDate(DateTime startdate) => _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.StartDate), startdate);
        public void UpdateEntryFee(decimal entryfee) => _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.EntryFee), entryfee);

        public void AddPrizes(string newPrize)
        {
            var prizes = Tournament.Prizes;
            prizes.Add(newPrize);
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Prizes), prizes);
        }

        public void AddFacility(string newFacility)
        {
            var facilities = Tournament.Facilities;
            facilities.Add(newFacility);
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Facilities), facilities);
        }
        public void AddVenues(string newVenues)
        {
            var venues = Tournament.Venues;
            venues.Add(newVenues);
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Venues), venues);
        }

        public void UpdateTournament(Tournament updateTournament)
        {
            _tournamentService.UpdateTournamentAsync(Tournament.Id, updateTournament);
        }

    }
}
