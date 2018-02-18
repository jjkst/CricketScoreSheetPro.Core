using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Models
{
    public enum ErrorTypes
    {
        None, Error, Warning, Info
    }

    public class ErrorType
    {
        public string Message { get; set; }

        public ErrorTypes Type { get; set; }
    }
}
