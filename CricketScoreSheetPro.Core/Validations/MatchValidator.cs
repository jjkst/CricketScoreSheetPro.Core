using CricketScoreSheetPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Validations
{
    public class MatchValidator 
    {
        private Match Match { get; set; }

        public MatchValidator(Match obj)
        {
            this.Match = obj;
        }

        public IList<ErrorResponse> IsValid()
        {
            var errortype = new List<ErrorResponse>();

            return errortype;
        }
    }
}
