using Carvana.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carvana.Controllers
{
    [ApiController]
    [Route("rent")] //rental route
    public class CarController : ControllerBase // will control all interactions to do with renting and car availability
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            this._carService = carService;
        }

        // searches DB for model matching modelID given
        [HttpGet("models/id")] // TODO, CHANGED ENDPOINT AND MODELID COMES FROM BODY NOW
        public async Task<IActionResult> GetCarsByModelID([FromBody] Guid modelID)
        {
            Model? model = await _carService.GetCarsByModelIDAsync(modelID);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // returns all models as list
        [HttpGet("models")]
        public async Task<IActionResult> GetAllModelsAsync()
        {
            var output = await _carService.GetAllModelsAsync();

            return Ok(output);
        }

        // searches DB for model matching model name given
        [HttpGet("models/search")] // TODO CHANGED TO FROMBODY
        public async Task<IActionResult> GetCarsByModelName([FromBody] string model)
        {
            // try find cars with matching model names
            List<Car>? carList = await _carService.GetCarsByModel(model);

            if (carList == null)
            {
                return NotFound("No cars with given model name found");
            }

            return Ok(carList);
        }

        // returns all cars from the DB
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var cars = await _carService.GetCarsAsync();
            return Ok(cars);
        }

        // searches for car in DB and returns it if found
        [HttpGet("/search")] // TODO CAHNGED TO FROMBODY
        public async Task<ActionResult> GetCarById([FromBody] Guid id) // get specific car matching id
        {
            Car? car = await _carService.GetCarAsync(id); // tries to get car by ID

            if (car == null)
            {
                return NotFound(); // if car is not found
            }

            return Ok(car); // if car is found
        }

        // remove car with matching ID from database if found
        [HttpDelete()] // TODO CAHNGED TO FROMBODY
        public async Task<IActionResult> DeleteCarById([FromBody] Guid id) // removes car from DB
        {
            bool? result = await _carService.DeleteCarAsync(id);

            if (result == true)
            {
                return Ok();
            }
            else if (result == null)
            {
                return NotFound("No car with given id found");
            }

            return BadRequest("Error deleting car");
        }

        // count the number of cars in the DB
        [HttpGet("count")]
        public async Task<ActionResult> Count() // returns number of cars
        {
            var count = await _carService.CountCarsAsync();
            return Ok(count);
        }

    }
}