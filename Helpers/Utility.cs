using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Exceptions;

namespace WebApi.Helpers
{
    public class Utility
    {
        public static bool SetGlobalErrorSession(HttpContext context, string key, Flash flash)
        {
            var jsonString = JsonSerializer.Serialize(flash);
            context.Session.SetString(key, jsonString);
            return true;
        }
        
    }
}