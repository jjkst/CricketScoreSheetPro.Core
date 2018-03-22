using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public class TeamDetailViewModel
    {
        private readonly ITeamService _teamService;

        public TeamDetailViewModel(ITeamService teamService, string teamId)
        {
            _teamService = teamService;
            Team = _teamService.GetTeamDetail(teamId);
        }

        public TeamDetail Team { get; private set; }

        public void AddPlayer(Player player)
        {
            var players = Team.Players;
            players.Add(player);
            _teamService.UpdateTeamProperty(Team.Id, nameof(Team.Players), players);
        }

        public void DeletePlayer(string oldPlayerName)
        {
            var players = Team.Players;
            for (int i = 0; i <= Team.Players.Count; i++)
            {
                if (players[i].Name == oldPlayerName) players.RemoveAt(i);
            }
            _teamService.UpdateTeamProperty(Team.Id, nameof(Team.Players), players);
        }
    }
}
