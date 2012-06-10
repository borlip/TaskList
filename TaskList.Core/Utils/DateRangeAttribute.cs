using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TaskList.Core.Utils
{
    public class DateRangeAttribute : ValidationAttribute
    {
        private const string DateFormat = "yyyy/MM/dd";

        private const string DefaultErrorMessage = "'{0}' must be a date between {1:d} and {2:d}.";

        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }

        public DateRangeAttribute(DateTime minDate, DateTime maxDate)
            : base(DefaultErrorMessage)
        {
            MinDate = minDate;
            MaxDate = maxDate;
        }

        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime))
            {
                return true;
            }
            var dateValue = (DateTime) value;
            return MinDate <= dateValue && dateValue <= MaxDate;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
                                 ErrorMessageString,
                                 name, MinDate, MaxDate);
        }
    }
}