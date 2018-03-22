using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public class AddMatchViewModel
    {
        private readonly IMatchService _matchService;
        private readonly ITeamService _teamService;
        private readonly IPlayerInningService _playerInningService;
        private readonly IUmpireService _umpireService;

        public AddMatchViewModel(IMatchService matchService,
            ITeamService teamService,
            IPlayerInningService playerInningService,
            IUmpireService umpireService)
        {
            _matchService = matchService ?? throw new ArgumentNullException($"MatchService is null");
            _teamService = teamService ?? throw new ArgumentNullException($"TeamService is null");
            _playerInningService = playerInningService ?? throw new ArgumentNullException($"PlayerInningService is null");
            _umpireService = umpireService ?? throw new ArgumentNullException($"PlayerInningService is null");

            Teams = _teamService.GetTeams().ToList();
            Matches = _matchService.GetMatches().ToList();
            var UmpiresListWithDupes = _umpireService.GetUmpires().ToList();
            Umpires = UmpiresListWithDupes.Distinct().ToList();
        }

        public List<Team> Teams { get; set; }

        public List<Tournament> Tournaments { get; set; }

        public bool IsTournament { get; set; }

        public Dictionary<int, string> SelectTotalOvers
        {
            get
            {
                var lst = new Dictionary<int, string>
                {
                    { 10, "Ten 10"},
                    { 20, "Twenty 20"},
                    { 30, "Thirty 30"},
                    { 35, "ThirtyFive 35"},
                    { 40, "Forty 40"},
                    { 50, "Fifty 50"},
                    { 0, "Custom"}
                };
                return lst;
            }
        }

        private List<Match> Matches { get; set; }

        public List<string> Locations
        {
            get
            {
                var locations = new List<string>();
                var uniqueLocations = Matches.GroupBy(l => l.Location);
                foreach (var ul in uniqueLocations)
                {
                    locations.Add(ul.Key);
                }
                return locations;
            }
        }

        public List<Umpire> Umpires { get; set; }
    }
}
