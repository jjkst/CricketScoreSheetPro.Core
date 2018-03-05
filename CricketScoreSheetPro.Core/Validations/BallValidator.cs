using CricketScoreSheetPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Validations
{
    public class BallValidator 
    {
        private Ball Ball { get; set; }

        public BallValidator(Ball obj)
        {
            this.Ball = obj;
        }

        public IList<ErrorResponse> IsValid()
        {
            var errortype = new List<ErrorResponse>();

            return errortype;
        }
    }
}
