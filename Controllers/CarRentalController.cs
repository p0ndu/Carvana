using Carvana.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carvana.Controllers
{
    [ApiController]
    [Route("rent")] //rental route
    public class CarRentalController : ControllerBase // will control all interactions to do with renting and car availability
    {
        private readonly string? _carDB; // placeholder for car database



        [HttpGet()]
        public ActionResult Get() // returns all car details - PLACEHOLDER
        {
            return Ok();
        }

        [HttpGet("carType")]
        public ActionResult GetCarType() // fetches information on different car types and their details - PLACEHOLDER
        {
            return Ok();
        }
        
    }
}