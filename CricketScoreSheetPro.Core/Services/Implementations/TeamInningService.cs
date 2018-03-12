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
        private readonly IRepository<TeamInning> _teaminningsRepository;

        public TeamInningService(IRepository<TeamInning> teaminningsRepository)
        {
            _teaminningsRepository = teaminningsRepository ?? throw new ArgumentNullException($"TeamInningRepository is null");
        }

        public async Task<TeamInning> AddTeamInningAsync(TeamInning newteaminning)
        {
            if (newteaminning == null) throw new ArgumentNullException($"New TeamInning is null");
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

        public async Task<IList<TeamInning>> GetTeamInningsAsync(string teamId)
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"TeamID is null");
            var teamInnings = await _teaminningsRepository.
                GetFilteredListAsync(nameof(TeamInning.TeamId), teamId);
            return teamInnings;
        }

        public async Task<IList<TeamInning>> GetTeamInningsByTournamentIdAsync(string teamId, string tournamentId)
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"TeamID is null");
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"TournamentId is null");
            var teamInnings = await _teaminningsRepository.
                GetFilteredListAsync(nameof(TeamInning.Team_TournamentId), $"{teamId}_{tournamentId}");
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
