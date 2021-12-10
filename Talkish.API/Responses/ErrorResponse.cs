using System;
using System.Collections.Generic;

namespace Talkish.API.Responses
{
    public class ErrorResponse
    {
        public int Status { get; set; }

        public string ErrorMessage { get; set; }

        public List<string> Errors { get; set; }
    }
}
