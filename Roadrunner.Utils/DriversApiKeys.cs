using System;
using System.Collections.Concurrent;

namespace Roadrunner.Utils
{
    public class DriversApiKeys
    {
        private static ConcurrentDictionary<int, string> ApiKeys = new ConcurrentDictionary<int, string>();

        public static string SetupDriverApiKey(int driverId)
        {
            if (!ApiKeys.ContainsKey(driverId))
            {
                ApiKeys.TryAdd(driverId, GetRansomApiKey());
            }

            return ApiKeys[driverId];
        }

        private static string GetRansomApiKey()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
