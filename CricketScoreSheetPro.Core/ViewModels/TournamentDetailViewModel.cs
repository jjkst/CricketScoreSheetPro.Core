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
            Tournament = _tournamentService.GetTournamentDetail(tournamentId) ?? throw new ArgumentNullException($"Tournament Id is not exist");
        }

        public TournamentDetail Tournament { get; private set; }

        public  void SetName(string name)
        {
            Tournament.Name = name;
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.Name), name);
        }
            
        public  void SetSponsor(string sponsor)
        {
            Tournament.Sponsor = sponsor;
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.Sponsor), sponsor);
        }

        public  void SetStatus(string status)
        {
            Tournament.Status = status;
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.Status), status);
        }
            
        public  void SetStartDate(DateTime startdate)
        {
            Tournament.StartDate = startdate;
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.StartDate), startdate);
        }
            
        public  void SetEntryFee(decimal entryfee)
        {
            Tournament.EntryFee = entryfee;
            _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.EntryFee), entryfee);
        }
            
        public  void UpdateTotalOvers(int totalOvers)
        {
            Tournament.TotalOvers = totalOvers;
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.TotalOvers), totalOvers);
        }
            

        public  void AddPrizes(string newPrize)
        {
            Tournament.Prizes.Add(newPrize);
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.Prizes), Tournament.Prizes);
        }

        public  void AddFacility(string newFacility)
        {
            Tournament.Facilities.Add(newFacility);
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.Facilities), Tournament.Facilities);
        }

        public  void AddVenue(string newVenue)
        {
            Tournament.Venues.Add(newVenue);
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.Venues), Tournament.Venues);
        }

        public  void IncludeTeam(Team team)
        {
            Tournament.Teams.Add(team);
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.Teams), Tournament.Teams);
        }

        public  void UpdatePrizes(string oldprize, string newprize)
        {
            for (int i = 0; i <= Tournament.Prizes.Count; i++)
            {
                if (Tournament.Prizes[i] == oldprize)
                {
                    Tournament.Prizes[i] = newprize;
                    break;
                }
            }
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.Prizes), Tournament.Prizes);
        }

        public  void UpdateFacility(string oldfacility, string newfacility)
        {
            for (int i = 0; i <= Tournament.Facilities.Count; i++)
            {
                if (Tournament.Facilities[i] == oldfacility)
                {
                    Tournament.Facilities[i] = newfacility;
                    break;
                }
            }
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.Facilities), Tournament.Facilities);
        }

        public  void UpdateVenue(string oldVenue, string newVenue)
        {
            for (int i = 0; i <= Tournament.Venues.Count; i++)
            {
                if (Tournament.Venues[i] == oldVenue)
                {
                    Tournament.Venues[i] = newVenue;
                    break;
                }
            }
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.Venues), Tournament.Venues);
        }

        public  void DeletePrizes(string oldprize)
        {
            for(int i = 0; i <= Tournament.Prizes.Count; i++)
            {
                if (Tournament.Prizes[i] == oldprize)
                {
                    Tournament.Prizes.RemoveAt(i);
                    break;
                }
            }
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.Prizes), Tournament.Prizes);
        }

        public  void DeleteFacility(string oldfacility)        
        {
            for (int i = 0; i <= Tournament.Facilities.Count; i++)
            {
                if (Tournament.Facilities[i] == oldfacility)
                {
                    Tournament.Facilities.RemoveAt(i);
                    break;
                }
            }
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.Facilities), Tournament.Facilities);
        }

        public  void DeleteVenue(string oldVenue)
        {
            for (int i = 0; i <= Tournament.Venues.Count; i++)
            {
                if (Tournament.Venues[i] == oldVenue)
                {
                    Tournament.Venues.RemoveAt(i);
                    break;
                }
            }
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.Venues), Tournament.Venues);
        }

        public  void ExcludeTeam(string oldTeamName)
        {
            for (int i = 0; i <= Tournament.Teams.Count; i++)
            {
                if (Tournament.Teams[i].Name == oldTeamName)
                {
                    Tournament.Teams.RemoveAt(i);
                    break;
                }
            }
             _tournamentService.UpdateTournamentProperty(Tournament.Id, nameof(Tournament.Teams), Tournament.Teams);
        }

        public void RefreshTournament()
        {
            Tournament = _tournamentService.GetTournamentDetail(Tournament.Id);
        }

        public List<Team> UserTeamNames(ITeamService teamService)
        {
            var teams = new List<Team>();
            foreach (var team in teamService.GetTeams())
            {
                if(Tournament.Teams.Count(t=>t.Name.ToLower().Contains(team.Name.ToLower())) > 0) continue;
                teams.Add(team);
            }
            return teams;
        }
    }
}
