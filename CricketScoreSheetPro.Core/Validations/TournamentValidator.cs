using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services;
using CricketScoreSheetPro.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.Validations
{
    public class TournamentValidator : BaseValidator
    {
        private ITournamentService _service;

        public TournamentValidator(ITournamentService service)
        {
            this._service = service ?? throw new ArgumentNullException($"TournamentService is null");
        }

        public IList<ErrorResponse> IsTournamentNameExist(string name)
        {
            var errors = new List<ErrorResponse>();
            var usertournaments = _service.GetTournaments();
            if (usertournaments.Any(ut => ut.Name.ToLower() == name.ToLower()))
            {
                errors.Add(new ErrorResponse
                {
                    Message = "Tournament name already exists.",
                    ErrorType = ErrorTypes.Error
                });
                return errors;
            }

            return errors;
        }

        public IList<ErrorResponse> IsValidStatus(string status)
        {
            var errors = new List<ErrorResponse>();
            if (status.ToLower() != "open" && status.ToLower() != "inprogress" && status.ToLower() != "complete")
            {
                errors.Add(new ErrorResponse
                {
                    Message = "Status can only be open, inprogress or complete.",
                    ErrorType = ErrorTypes.Error
                });
                return errors;
            }
            return errors;
        }       
    }
}
