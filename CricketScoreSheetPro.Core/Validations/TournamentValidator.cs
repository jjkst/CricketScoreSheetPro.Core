using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.Validations
{
    public class TournamentValidator
    {
        //private IService<UserTournament> Service;
        //private Tournament Tournament { get; set; }

        //public TournamentValidator(IService<UserTournament> service, Tournament obj) 
        //{
        //    this.Service = service;
        //    this.Tournament = obj;            
        //}

        //public IList<ErrorResponse> IsValid()
        //{
        //    var errortype = new List<ErrorResponse>();

        //    if (string.IsNullOrEmpty(Tournament.Name))
        //    {
        //        errortype.Add(new ErrorResponse
        //        {
        //            Message = "Tournament name cannot be blank.",
        //            Type = ErrorTypes.Error
        //        });
        //        return errortype;
        //    }

        //    var usertournaments = Service.GetList().Result;

        //    if (usertournaments.Any(ut=>ut.TournamentName.ToLower() == Tournament.Name.ToLower()))
        //    {
        //        errortype.Add(new ErrorResponse
        //        {
        //            Message = "Team name already exists.",
        //            Type = ErrorTypes.Error
        //        });
        //        return errortype;
        //    }

        //    return errortype;
        //}
    }
}
