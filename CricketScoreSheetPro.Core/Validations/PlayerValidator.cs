using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Validations
{
    public class PlayerValidator : IValidator
    {
        private IService<TeamPlayer> Service;
        private Player Player { get; set; }

        public PlayerValidator(IService<TeamPlayer> service, Player obj)
        {
            this.Service = service;
            this.Player = obj;
        }

        public IList<ErrorType> IsValid()
        {
            var errortype = new List<ErrorType>();

            if (string.IsNullOrEmpty(Player.Name))
            {
                errortype.Add(new ErrorType
                {
                    Message = "Player name cannot be blank.",
                    Type = ErrorTypes.Error
                });
                return errortype;
            }

            var teamPlayers = Service.GetList().Result;

            if (teamPlayers.Any(tp=>tp.PlayerName.ToLower() == Player.Name.ToLower()))
            {
                errortype.Add(new ErrorType
                {
                    Message = "Player name already exists.",
                    Type = ErrorTypes.Error
                });
                return errortype;
            }

            if (teamPlayers.Any(tp=>tp.Roles.Contains("captain")) && Player.Roles.Contains("captain"))
            {
                errortype.Add(new ErrorType
                {
                    Message = "Captain already exist. If you want to add captain, please edit/delete previous captain.",
                    Type = ErrorTypes.Error
                });
                return errortype;
            }

            if (teamPlayers.Any(tp => tp.Roles.Contains("keeper") && Player.Roles.Contains("keeper")))
            {
                errortype.Add(new ErrorType
                {
                    Message = "Keeper already exist. If you want to add keeper then you have to select any one when adding match",
                    Type = ErrorTypes.Warning
                });
            }

            return errortype;
        }
    }
}
