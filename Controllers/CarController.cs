using Carvana.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carvana.Controllers
{
    [ApiController]
    [Route("rent")] // rental route
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        // ---------------------------------------
        // Model Endpoints
        // ---------------------------------------

        [HttpGet("models")]
        public async Task<IActionResult> GetAllModelsAsync()
        {
            var models = await _carService.GetAllModelsAsync();
            return Ok(models); // returns list of all available car models
        }

        [HttpGet("models/id/{modelID:guid}")]
        public async Task<IActionResult> GetCarsByModelID([FromRoute] Guid modelID)
        {
            var model = await _carService.GetCarsByModelIDAsync(modelID);

            if (model == null)
            {
                return NotFound(); // if no matching model is found, return 404
            }

            return Ok(model); // returns all cars with the matching model ID
        }

        [HttpGet("models/search/{modelName}")]
        public async Task<IActionResult> GetCarsByModelName([FromRoute] string modelName)
        {
            var carList = await _carService.GetCarsByModel(modelName);

            if (carList == null || !carList.Any())
            {
                return NotFound("No cars with given model name found"); // handles null and empty list just in case
            }

            return Ok(carList); // returns all cars that match the given model name (partial or exact depending on service logic)
        }

        // ---------------------------------------
        // Car Endpoints
        // ---------------------------------------

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carService.GetCarsAsync();
            return Ok(cars); // returns full list of cars, regardless of status
        }

        [HttpGet("search/{id:guid}")]
        public async Task<IActionResult> GetCarById([FromRoute] Guid id)
        {
            var car = await _carService.GetCarAsync(id);

            if (car == null)
            {
                return NotFound(); // no car found with this ID
            }

            return Ok(car); // returns car if match found
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCarById([FromRoute] Guid id)
        {
            var result = await _carService.DeleteCarAsync(id);

            if (result == true)
            {
                return Ok(); // car deleted successfully
            }

            if (result == null)
            {
                return NotFound("No car with given id found"); // nothing to delete
            }

            return BadRequest("Error deleting car"); // deletion failed for unknown reason
        }

        // ---------------------------------------
        // Utility
        // ---------------------------------------

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _carService.CountCarsAsync();
            return Ok(count); // returns number of cars in system
        }
    }
}
