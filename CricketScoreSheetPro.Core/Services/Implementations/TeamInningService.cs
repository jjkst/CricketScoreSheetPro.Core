using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class TeamInningService : ITeamInningService
    {
        private readonly IRepository<TeamInning> _teaminningsRepository;

        public TeamInningService(IRepository<TeamInning> teaminningsRepository)
        {
            _teaminningsRepository = teaminningsRepository ?? throw new ArgumentNullException($"TeamInningRepository is null");
        }

        public string AddTeamInning(TeamInning newteaminning)
        {
            if (newteaminning == null) throw new ArgumentNullException($"New TeamInning is null");
            var teamInningAddkey = _teaminningsRepository.Create(newteaminning);
            return teamInningAddkey;
        }

        public void UpdateTeamInning(string teamInningsId, TeamInning teaminning)
        {
            if (teaminning == null) throw new ArgumentNullException($"TeamInning is null");
            if (string.IsNullOrEmpty(teamInningsId)) throw new ArgumentException($"TeamInning ID is null");
            _teaminningsRepository.CreateWithId(teamInningsId, teaminning);
        }

        public TeamInning GetTeamInning(string teamInningsId)
        {
            if (string.IsNullOrEmpty(teamInningsId)) throw new ArgumentException($"TeamInning ID is null");
            var teamInning =  _teaminningsRepository.GetItem(teamInningsId);
            return teamInning;
        }

        public IList<TeamInning> GetTeamInnings(string teamId)
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"TeamID is null");
            var teamInnings =  _teaminningsRepository.
                GetFilteredList(nameof(TeamInning.TeamId), teamId);
            return teamInnings;
        }

        public IList<TeamInning> GetTeamInningsByTournamentId(string teamId, string tournamentId)
        {
            if (string.IsNullOrEmpty(teamId)) throw new ArgumentException($"TeamID is null");
            if (string.IsNullOrEmpty(tournamentId)) throw new ArgumentException($"TournamentId is null");
            var teamInnings =  _teaminningsRepository.
                GetFilteredList(nameof(TeamInning.Team_TournamentId), $"{teamId}_{tournamentId}");
            return teamInnings;
        }

        public void DeleteAllTeamInnings()
        {
             _teaminningsRepository.Delete();
        }

        public void DeleteTeamInning(string teamInningsId)
        {
            if (string.IsNullOrEmpty(teamInningsId)) throw new ArgumentException($"TeamInning ID is null");
             _teaminningsRepository.DeleteById(teamInningsId);
        }
    }
}
