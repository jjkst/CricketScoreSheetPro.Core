using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.Validations
{
    public class TournamentValidator :  IValidator
    {
        private IService<UserTournament> Service;
        private Tournament Tournament { get; set; }

        public TournamentValidator(IService<UserTournament> service, Tournament obj) 
        {
            this.Service = service;
            this.Tournament = obj;            
        }

        public IList<ErrorType> IsValid()
        {
            var errortype = new List<ErrorType>();

            if (string.IsNullOrEmpty(Tournament.Name))
            {
                errortype.Add(new ErrorType
                {
                    Message = "Tournament name cannot be blank.",
                    Type = ErrorTypes.Error
                });
                return errortype;
            }

            var usertournaments = Service.GetList().Result;

            if (usertournaments.Any(ut=>ut.TournamentName.ToLower() == Tournament.Name.ToLower()))
            {
                errortype.Add(new ErrorType
                {
                    Message = "Team name already exists.",
                    Type = ErrorTypes.Error
                });
                return errortype;
            }

            return errortype;
        }
    }
}
