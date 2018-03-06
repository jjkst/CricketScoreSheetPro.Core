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
            _tournamentService = tournamentService ?? throw new ArgumentNullException($"TournamentService is null");
            Tournament = _tournamentService.GetTournamentAsync(tournamentId).Result;
        }

        public TournamentDetail Tournament { get; private set; }

        public void UpdateName(string name) => 
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Name), name);
        public void UpdateSponsor(string sponsor) => 
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Sponsor), sponsor);
        public void UpdateStatus(string status) => 
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Status), status);
        public void UpdateStartDate(DateTime startdate) => 
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.StartDate), startdate);
        public void UpdateEntryFee(decimal entryfee) => 
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.EntryFee), entryfee);

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

        public void AddVenue(string newVenue)
        {
            var venues = Tournament.Venues;
            venues.Add(newVenue);
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Venues), venues);
        }

        public void AddPlayingTeamName(string playingTeamName)
        {
            var playingteamnames = Tournament.PlayingTeamNames;
            playingteamnames.Add(playingTeamName);
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.PlayingTeamNames), playingteamnames);
        }

        public void UpdatePrizes(string oldprize, string newprize)
        {
            var prizes = Tournament.Prizes;
            for (int i = 0; i <= Tournament.Prizes.Count; i++)
            {
                if (prizes[i] == oldprize) prizes[i] = newprize;
            }
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Prizes), prizes);
        }

        public void UpdateFacility(string oldfacility, string newfacility)
        {
            var facilities = Tournament.Facilities;
            for (int i = 0; i <= Tournament.Facilities.Count; i++)
            {
                if (facilities[i] == oldfacility) facilities[i] = newfacility;
            }
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Facilities), facilities);
        }

        public void UpdateVenue(string oldVenue, string newVenue)
        {
            var venues = Tournament.Venues;
            for (int i = 0; i <= Tournament.Venues.Count; i++)
            {
                if (venues[i] == oldVenue) venues[i] = newVenue;
            }
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Venues), venues);
        }

        public void DeletePrizes(string oldprize)
        {
            var prizes = Tournament.Prizes;
            for(int i = 0; i <= Tournament.Prizes.Count; i++)
            {
                if (prizes[i] == oldprize) prizes.RemoveAt(i);
            }
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Prizes), prizes);
        }

        public void DeleteFacility(string oldfacility)        
        {
            var facilities = Tournament.Facilities;
            for (int i = 0; i <= Tournament.Facilities.Count; i++)
            {
                if (facilities[i] == oldfacility) facilities.RemoveAt(i);
            }
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Facilities), facilities);
        }

        public void DeleteVenue(string oldVenue)
        {
            var venues = Tournament.Venues;
            for (int i = 0; i <= Tournament.Venues.Count; i++)
            {
                if (venues[i] == oldVenue) venues.RemoveAt(i);
            }
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Venues), venues);
        }

        public void DeletePlayingTeamName(string oldplayingTeamName)
        {
            var playingteamnames = Tournament.PlayingTeamNames;
            for (int i = 0; i <= Tournament.PlayingTeamNames.Count; i++)
            {
                if (playingteamnames[i] == oldplayingTeamName) playingteamnames.RemoveAt(i);
            }
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.PlayingTeamNames), playingteamnames);
        }

        public void RefreshTournament()
        {
            Tournament = _tournamentService.GetTournamentAsync(Tournament.Id).Result;
        }

        public List<Team> UserTeamNames(ITeamService teamService)
        {
            var userteams = new List<Team>();
            foreach (var team in teamService.GetUserTeamsAsync().Result)
            {
                if(Tournament.PlayingTeamNames.Contains(team.TeamName)) continue;
                userteams.Add(team);
            }
            return userteams;
        }
    }
}
