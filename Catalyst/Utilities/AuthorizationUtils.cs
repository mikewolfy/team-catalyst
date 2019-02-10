using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalyst.Utilities
{
    public static class AuthorizationUtils
    {
        public static bool Authorized(HttpRequest request, IConfiguration config)
        {
            bool result = false;

            if (request != null)
            {
                var keyHeaders = request.Headers.Where(h => h.Key == "apiKey");
                result = keyHeaders.Count() == 1 && keyHeaders.First().Value == config["ApiKey"]; ;
            }

            return result;
        }
    }
}
