using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Roadrunner.Utils;

namespace Roadrunner.Web.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableRewind();

            if (IsInvalidAuthHeader(context) || IsInvalidSignature(context))
            {
                context.Response.StatusCode = 401; //Unauthorized
                return;
            }

            var authParts = AuthorizationHeader(context).Split(' ', ':');
            var userId = authParts[1];
            context.User = new GenericPrincipal(new GenericIdentity(userId), new string[] { });
            await _next.Invoke(context);
        }

        private static bool IsOldRequest(HttpContext context)
        {
            var dateHeader = ExtractHeader(context, "Date");
            if (DateTime.TryParse(dateHeader, out var requestDate))
            {
                return requestDate < DateTime.Now.AddMinutes(-15);
            }
            return true;
        }

        private static string ExtractHeader(HttpContext context, string headerName)
        {
            if (context?.Request?.Headers == null || !context.Request.Headers.ContainsKey(headerName))
            {
                return string.Empty;
            }

            return context.Request.Headers.First(h => h.Key == headerName).Value.First();
        }

        private static bool IsInvalidAuthHeader(HttpContext context)
        {
            var authHeader = AuthorizationHeader(context);
            return authHeader.Split(' ', ':').Length != 3;
        }

        private static string AuthorizationHeader(HttpContext context)
        {
            return ExtractHeader(context, "Authorization");
        }

        private static bool IsInvalidSignature(HttpContext context)
        {
            var authParts = AuthorizationHeader(context).Split(' ', ':');
            var userId = authParts[1];
            var requestSignature = authParts[2];
            return requestSignature != AuthSignature.GenerateSignature(userId, Encoding.UTF8.GetBytes(DriversApiKeys.GetUserKey(userId)));
        }
    }
}
