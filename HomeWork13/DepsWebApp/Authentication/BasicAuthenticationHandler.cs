using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DepsWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DepsWebApp.Authentication
{
    /// <summary>
    /// Basic authentication handler.
    /// </summary>
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthService _userService;

        /// <summary>
        /// Constructor.
        /// </summary>
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthService userService)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        /// <summary>
        /// Handling the authentication.
        /// </summary>
        /// <returns>
        /// <see cref="AuthenticateResult.Success(AuthenticationTicket)"/> if authorization header is OK; 
        /// <see cref="AuthenticateResult.Fail(string)"/> if an error occured during the authentication.
        /// </returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                Logger.LogInformation("Missing Authorization Header");
                return AuthenticateResult.Fail("Missing Authorization Header");
            }
            var isAuthenticated = false;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                isAuthenticated = await _userService.AuthenticateAsync(username, password);
            }
            catch
            {
                Logger.LogInformation("Invalid Authorization Header");
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (!isAuthenticated)
            {
                Logger.LogInformation("Invalid Username or Password");
                return AuthenticateResult.Fail("Invalid Username or Password");
            }

            Logger.LogInformation("Successfully authenticated");
            return AuthenticateResult.Success(
            new AuthenticationTicket(
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new List<Claim>() { new Claim(ClaimTypes.Role,"user") },
                        Scheme.Name)),
                Scheme.Name));
        }
    }
}