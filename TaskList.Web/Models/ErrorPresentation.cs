using System;

namespace TaskList.Web.Models
{
    public class ErrorPresentation
    {
        public string ErrorMessage { get; set; }

        public Exception TheException { get; set; }

        public bool ShowMessage { get; set; }

        public bool ShowLink { get; set; }
    }
}