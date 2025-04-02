using Carvana.Data;
using Carvana.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("models/id={modelID}")]
        public async Task<IActionResult> GetCarsByModelID(Guid modelID)
        {
            Model model = await _carService.GetCarsByModelIDAsync(modelID);

            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpGet("models")]
        public async Task<IActionResult> GetAllModelsAsync()
        {
            var output = _carService.GetAllModelsAsync();
            return Ok(output);
        }
        
        [HttpGet()]
        public async Task<IActionResult> Get() // returns all cars from DB
        {
            var cars = await _carService.GetCarsAsync(); 
            return Ok(cars);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCarById(Guid id) // get specific car matching id
        {
            Car? car = await _carService.GetCarAsync(id); // tries to get car by ID
        
            if (car == null)
            {
                return NotFound(); // if car is not found
            }
            return Ok(car); // if car is found
        }
        
        [HttpDelete("{id}")]
                public async Task<IActionResult> DeleteCarById(Guid id) // removes car from DB
                {
                    bool result = await _carService.DeleteCarAsync(id);
        
                    if (result)
                    {
                        return Ok();
                    }

                    return NotFound();
                }

        [HttpGet("count")]
        public async Task<ActionResult> Count() // returns number of cars
        {
            var count = await _carService.CountCarsAsync(); 
            return Ok(count);
        }
        
    }
}