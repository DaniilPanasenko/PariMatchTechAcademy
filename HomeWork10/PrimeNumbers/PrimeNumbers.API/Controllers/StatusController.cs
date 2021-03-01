using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace PrimeNumbers.API.Controllers
{
    [ApiController]
    [Route("api/status")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class StatusController : ControllerBase
    {
        public StatusController()
        {
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<string> Get()
        {
            return Ok("Prime Numbers API by Daniil Panasenko");
        }
    }
}