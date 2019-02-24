using System;
using System.Collections.Concurrent;

namespace Roadrunner.Utils
{
    public class DriversApiKeys
    {
        private static ConcurrentDictionary<string, string> ApiKeys = new ConcurrentDictionary<string, string>();

        public static string SetupUserApiKey(string userId)
        {
            if (!ApiKeys.ContainsKey(userId))
            {
                ApiKeys.TryAdd(userId, GetRandomApiKey());
            }

            return ApiKeys[userId];
        }

        private static string GetRandomApiKey()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        public static string GetUserKey(string userId)
        {
            return ApiKeys[userId];
        }
    }
}
