using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SantexLeague.Common.Exceptions
{
    public class Error
    {
        public static string Message(string message) 
        {
            return JsonConvert.SerializeObject(new ErrorDetail(message));
        }
        private class ErrorDetail
        {
            public ErrorDetail(string message)
            {
                this.message = message;
            }
            public string message { get; set; }
        }
    }
}
