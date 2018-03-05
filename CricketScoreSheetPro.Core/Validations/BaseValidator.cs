using CricketScoreSheetPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Validations
{
    public abstract class BaseValidator
    {
        public IList<ErrorResponse> IsValidString(string value)
        {
            var errors = new List<ErrorResponse>();

            if (string.IsNullOrEmpty(value))
            {
                errors.Add(new ErrorResponse
                {
                    Message = "Value cannot be blank.",
                    ErrorType = ErrorTypes.Error
                });
                return errors;
            }

            return errors;
        }

        public IList<ErrorResponse> IsValidDate(DateTime date)
        {
            var errors = new List<ErrorResponse>();

            if (date == null || date < DateTime.Today)
            {
                errors.Add(new ErrorResponse
                {
                    Message = "Date cannot be blank or past date.",
                    ErrorType = ErrorTypes.Error
                });
                return errors;
            }

            return errors;
        }

        public IList<ErrorResponse> IsValidMoney(string value)
        {
            var errors = new List<ErrorResponse>();

            if (!decimal.TryParse(value, out decimal result))
            {
                errors.Add(new ErrorResponse
                {
                    Message = "Invalid money.",
                    ErrorType = ErrorTypes.Error
                });
                return errors;
            }

            return errors;
        }
    }
}
