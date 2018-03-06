using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class TeamInningService : ITeamInningService
    {
        private readonly IRepository<TeamDetail> _teamdetailRepository;
        private readonly IRepository<TeamInning> _teaminningsRepository;

        public TeamInningService(IRepository<TeamDetail> teamdetailRepository, IRepository<TeamInning> teaminningsRepository)
        {
            _teamdetailRepository = teamdetailRepository ?? throw new ArgumentNullException($"TeamDetailRepository is null");
            _teaminningsRepository = teaminningsRepository ?? throw new ArgumentNullException($"TeamInningRepository is null");
        }

        public async Task<TeamInning> AddTeamInningAsync(string teamId, string matchId, string tournamentId = "")
        {
            var teamdetail = await _teamdetailRepository.GetItemAsync(teamId);
            var newteaminning = new TeamInning
            {
                TeamId = teamId,
                TeamName = teamdetail.Name,
                MatchId = matchId,
                TournamentId = tournamentId
            };

            var teamInningAdd = await _teaminningsRepository.CreateAsync(newteaminning);
            return teamInningAdd;
        }

        public async Task UpdateTeamInningAsync(string teamInningsId, TeamInning teaminning)
        {
            if (teaminning == null) throw new ArgumentNullException($"TeamInning is null");
            if (string.IsNullOrEmpty(teamInningsId)) throw new ArgumentException($"TeamInning ID is null");
            await _teaminningsRepository.CreateWithIdAsync(teamInningsId, teaminning);
        }

        public async Task<TeamInning> GetTeamInningAsync(string teamInningsId)
        {
            if (string.IsNullOrEmpty(teamInningsId)) throw new ArgumentException($"TeamInning ID is null");
            var teamInning = await _teaminningsRepository.GetItemAsync(teamInningsId);
            return teamInning;
        }

        public async Task<IList<TeamInning>> GetTeamInningsAsync()
        {
            var teamInnings = await _teaminningsRepository.GetListAsync();
            return teamInnings;
        }

        public async Task DeleteAllTeamInningsAsync()
        {
            await _teaminningsRepository.DeleteAsync();
        }

        public async Task DeleteTeamInningAsync(string teamInningsId)
        {
            if (string.IsNullOrEmpty(teamInningsId)) throw new ArgumentException($"TeamInning ID is null");
            await _teaminningsRepository.DeleteByIdAsync(teamInningsId);
        }
    }
}
