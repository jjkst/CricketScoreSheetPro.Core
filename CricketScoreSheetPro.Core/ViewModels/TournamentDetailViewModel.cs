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
            Tournament = _tournamentService.GetTournamentDetailAsync(tournamentId)?.Result ?? throw new ArgumentNullException($"Tournament Id is not exist");
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
        public void UpdateTotalOvers(int totalOvers) =>
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.TotalOvers), totalOvers);

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

        public void IncludeTeam(Team team)
        {
            var teams = Tournament.Teams;
            teams.Add(team);
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Teams), teams);
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

        public void ExcludeTeam(string oldTeamName)
        {
            var teams = Tournament.Teams;
            for (int i = 0; i <= Tournament.Teams.Count; i++)
            {
                if (teams[i].Name == oldTeamName) teams.RemoveAt(i);
            }
            _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Teams), teams);
        }

        public void RefreshTournament()
        {
            Tournament = _tournamentService.GetTournamentDetailAsync(Tournament.Id).Result;
        }

        public List<Team> UserTeamNames(ITeamService teamService)
        {
            var teams = new List<Team>();
            foreach (var team in teamService.GetTeamsAsync().Result)
            {
                if(Tournament.Teams.Count(t=>t.Name.ToLower().Contains(team.Name.ToLower())) > 0) continue;
                teams.Add(team);
            }
            return teams;
        }
    }
}
