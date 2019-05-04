using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTreinritten.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            //Dit is een verkorte if statement, bij false krijg je null anders krijg je een Json object
            // T is gewoon algemeen, anders moeten we 10 tallen methodes aanmaken, T staat voor elk type
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
