using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalyst.Utilities
{
    public static class AuthorizationUtils
    {
        public static bool Authorized(HttpRequest request)
        {
            bool result = false;

            if (request != null)
            {
                var keyHeaders = request.Headers.Where(h => h.Key == "apiKey");
                result = keyHeaders.Count() == 1 && keyHeaders.First().Value == "catalystKey123";
            }

            return result;
        }
    }
}
