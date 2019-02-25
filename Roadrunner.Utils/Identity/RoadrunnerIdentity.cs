using System;
using Microsoft.AspNetCore.Http;

namespace Roadrunner.Utils.Identity
{
    public class RoadrunnerIdentity : IRoadrunnerIdentity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoadrunnerIdentity(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User != null &&
                   _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public string GetUserId()
        {
            return !IsAuthenticated() ? string.Empty : _httpContextAccessor.HttpContext.User.Identity.Name;
        }

        public void ThrowUnautorizedIfNotAuthenticated()
        {
            if (IsAuthenticated())
            {
                return;
            }

            throw new UnauthorizedAccessException();
        }
    }
}
