using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using DepsWebApp.Authentication;
using DepsWebApp.Contracts;
using DepsWebApp.Filters;
using DepsWebApp.Models;
using DepsWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Authentication controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ExceptionFilter]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _auth;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="auth">IAuthService with DI</param>
        /// <param name="logger">ILogger with DI</param>
        public AuthController(
            IAuthService auth,
            ILogger<AuthController> logger)
        {
            _auth = auth;
            _logger = logger;
        }
        /// <summary>
        /// Registration.
        /// </summary>
        /// <param name="authRequest">Authentication request model is login and password</param>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Register(AuthRequest authRequest)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Invalid user data");
                return BadRequest(ModelState);
            }

            if (!await _auth.RegisterAsync(authRequest.Login, authRequest.Password))
            {
                _logger.LogInformation("This login already exist");
                return Conflict("This login already exist");
            }
            _logger.LogInformation("Successfully registration");
            return Ok();
        }
    }
}
