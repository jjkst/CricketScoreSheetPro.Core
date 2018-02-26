using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Validations
{
    public class TeamValidator 
    {
        //private IService<UserTeam> Service;
        //private Team Team { get; set; }

        //public TeamValidator(IService<UserTeam> service, Team obj)
        //{
        //    this.Service = service;
        //    this.Team = obj;
        //}

        //public IList<ErrorResponse> IsValid()
        //{
        //    var errortype = new List<ErrorResponse>();

        //    if (string.IsNullOrEmpty(Team.Name))
        //    {
        //        errortype.Add(new ErrorResponse
        //        {
        //            Message = "Team name cannot be blank.",
        //            Type = ErrorTypes.Error
        //        });
        //        return errortype;
        //    }

        //    var userteams = Service.GetList().Result;

        //    if (userteams.Any(tp => tp.TeamName.ToLower() == Team.Name.ToLower()))
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
