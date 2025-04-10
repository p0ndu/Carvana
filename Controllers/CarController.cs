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
            return Ok(models);
        }

        [HttpGet("models/id/{modelID:guid}")]
        public async Task<IActionResult> GetCarsByModelID([FromRoute] Guid modelID)
        {
            var model = await _carService.GetCarsByModelIDAsync(modelID);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpGet("models/search/{modelName}")]
        public async Task<IActionResult> GetCarsByModelName([FromRoute] string modelName)
        {
            var carList = await _carService.GetCarsByModel(modelName);

            if (carList == null || !carList.Any())
            {
                return NotFound("No cars with given model name found");
            }

            return Ok(carList);
        }

        // ---------------------------------------
        // Car Endpoints
        // ---------------------------------------

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carService.GetCarsAsync();
            return Ok(cars);
        }

        [HttpGet("search/{id:guid}")]
        public async Task<IActionResult> GetCarById([FromRoute] Guid id)
        {
            var car = await _carService.GetCarAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCarById([FromRoute] Guid id)
        {
            var result = await _carService.DeleteCarAsync(id);

            if (result == true)
            {
                return Ok();
            }

            if (result == null)
            {
                return NotFound("No car with given id found");
            }

            return BadRequest("Error deleting car");
        }

        // ---------------------------------------
        // Utility
        // ---------------------------------------

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _carService.CountCarsAsync();
            return Ok(count);
        }
    }
}
