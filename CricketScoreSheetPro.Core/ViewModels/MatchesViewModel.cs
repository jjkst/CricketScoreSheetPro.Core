using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.ViewModels
{
    public class MatchesViewModel
    {
        private readonly IMatchService _matchService;

        public MatchesViewModel(IMatchService matchService)
        {
            _matchService = matchService ?? throw new ArgumentNullException($"MatchService is null");
            Matches = _matchService.GetMatchesAsync().Result.ToList();
        }

        public List<Match> Matches { get; private set; }
    }
}
