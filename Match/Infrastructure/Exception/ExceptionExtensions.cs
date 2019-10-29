using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Infrastructure
{
    public static class ExceptionExtensions
    {
        public static void AddApplicationError(this Microsoft.AspNetCore.Http.HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}
