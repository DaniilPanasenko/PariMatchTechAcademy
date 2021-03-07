using System;
using DepsWebApp.Filters;
using DepsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Authorization controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ExceptionFilter]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Registration.
        /// </summary>
        /// <param name="user">User model from request body.</param>
        /// <exception cref="NotImplementedException">Action is not implemented.</exception>
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
