using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public async void SetName(string name)
        {
            Tournament.Name = name;
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Name), name);
        }
            
        public async void SetSponsor(string sponsor)
        {
            Tournament.Sponsor = sponsor;
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Sponsor), sponsor);
        }

        public async void SetStatus(string status)
        {
            Tournament.Status = status;
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Status), status);
        }
            
        public async void SetStartDate(DateTime startdate)
        {
            Tournament.StartDate = startdate;
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.StartDate), startdate);
        }
            
        public async void SetEntryFee(decimal entryfee)
        {
            Tournament.EntryFee = entryfee;
           await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.EntryFee), entryfee);
        }
            
        public async void UpdateTotalOvers(int totalOvers)
        {
            Tournament.TotalOvers = totalOvers;
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.TotalOvers), totalOvers);
        }
            

        public async void AddPrizes(string newPrize)
        {
            Tournament.Prizes.Add(newPrize);
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Prizes), Tournament.Prizes);
        }

        public async void AddFacility(string newFacility)
        {
            Tournament.Facilities.Add(newFacility);
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Facilities), Tournament.Facilities);
        }

        public async void AddVenue(string newVenue)
        {
            Tournament.Venues.Add(newVenue);
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Venues), Tournament.Venues);
        }

        public async void IncludeTeam(Team team)
        {
            Tournament.Teams.Add(team);
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Teams), Tournament.Teams);
        }

        public async void UpdatePrizes(string oldprize, string newprize)
        {
            for (int i = 0; i <= Tournament.Prizes.Count; i++)
            {
                if (Tournament.Prizes[i] == oldprize)
                {
                    Tournament.Prizes[i] = newprize;
                    break;
                }
            }
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Prizes), Tournament.Prizes);
        }

        public async void UpdateFacility(string oldfacility, string newfacility)
        {
            for (int i = 0; i <= Tournament.Facilities.Count; i++)
            {
                if (Tournament.Facilities[i] == oldfacility)
                {
                    Tournament.Facilities[i] = newfacility;
                    break;
                }
            }
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Facilities), Tournament.Facilities);
        }

        public async void UpdateVenue(string oldVenue, string newVenue)
        {
            for (int i = 0; i <= Tournament.Venues.Count; i++)
            {
                if (Tournament.Venues[i] == oldVenue)
                {
                    Tournament.Venues[i] = newVenue;
                    break;
                }
            }
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Venues), Tournament.Venues);
        }

        public async void DeletePrizes(string oldprize)
        {
            for(int i = 0; i <= Tournament.Prizes.Count; i++)
            {
                if (Tournament.Prizes[i] == oldprize)
                {
                    Tournament.Prizes.RemoveAt(i);
                    break;
                }
            }
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Prizes), Tournament.Prizes);
        }

        public async void DeleteFacility(string oldfacility)        
        {
            for (int i = 0; i <= Tournament.Facilities.Count; i++)
            {
                if (Tournament.Facilities[i] == oldfacility)
                {
                    Tournament.Facilities.RemoveAt(i);
                    break;
                }
            }
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Facilities), Tournament.Facilities);
        }

        public async void DeleteVenue(string oldVenue)
        {
            for (int i = 0; i <= Tournament.Venues.Count; i++)
            {
                if (Tournament.Venues[i] == oldVenue)
                {
                    Tournament.Venues.RemoveAt(i);
                    break;
                }
            }
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Venues), Tournament.Venues);
        }

        public async void ExcludeTeam(string oldTeamName)
        {
            for (int i = 0; i <= Tournament.Teams.Count; i++)
            {
                if (Tournament.Teams[i].Name == oldTeamName)
                {
                    Tournament.Teams.RemoveAt(i);
                    break;
                }
            }
            await _tournamentService.UpdateTournamentPropertyAsync(Tournament.Id, nameof(Tournament.Teams), Tournament.Teams);
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
