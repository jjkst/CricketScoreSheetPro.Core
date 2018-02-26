﻿namespace CricketScoreSheetPro.Core.Models
{
    public enum ErrorTypes
    {
        None, Error, Warning, Info
    }

    public class ErrorResponse
    {
        public string Message { get; set; }

        public ErrorTypes Type { get; set; }
    }
}
