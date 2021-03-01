using Microsoft.AspNetCore.Mvc;
using PrimeNumbers.API.Interfaces;
using PrimeNumbers.API.Models;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PrimeNumbers.API.Controllers
{
    [ApiController]
    [Route("api/primes")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class PrimesController : ControllerBase
    {
        private readonly IPrimeNumbersSevice _primeNumbersSevice;

        public PrimesController(IPrimeNumbersSevice primeNumbersSevice)
        {
            _primeNumbersSevice = primeNumbersSevice;
        }

        [HttpGet]
        [Route("{number}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetIsPrimeAsync(int number)
        {
            var isPrime = await _primeNumbersSevice.IsPrimeAsync(number);
            return isPrime ? Ok() : NotFound();
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<string>> GetPrimesAsync(int? from, int? to)
        {
            if(from ==null || to == null)
            {
                return BadRequest("Invalid parametres");
            }
            var settings = new PrimesSettings((int)from, (int)to);
            var primes = await _primeNumbersSevice.GetPrimesAsync(settings);
            var result = string.Join(",", primes);
            return Ok(result);
        }
    }
}
