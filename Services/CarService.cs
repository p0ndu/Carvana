using Carvana.Data;
using Microsoft.EntityFrameworkCore;

namespace Carvana.Services;

public class CarService : ICarService
{
    private readonly ApplicationDbContext _context; // DB context

    public CarService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Car>> GetCarsAsync() // returns full list of cars
    {
        //get cars from DB and include the model
        var output = await _context.Cars.Include(c => c.CarModel).ToListAsync();

        foreach (var car in output)
        {
            Console.WriteLine($"Car: {car.CarId}, Model: {car.CarModel.Name}, Type: {car.CarModel.VehicleType}");
        }

        return output;
    }

    public async Task<Model> GetCarsByModelIDAsync(Guid modelId)
    {
        var output = await _context.Models.FindAsync(modelId);

        if (output == null) // if no model is found
        {
            Console.WriteLine("Error, model matching ModelID not found.");
        }

        return output;
    }

    public async Task<IEnumerable<Model>> GetAllModelsAsync()
    {
        var output = await _context.Models.ToListAsync();
        return output;
    }

    public async Task<Car?> GetCarAsync(Guid id)
    {
        return await _context.Cars.FindAsync(id); // search DB for car matching ID
    }

    public async Task<Car> AddCarAsync(Car car)
    {
        _context.Cars.AddAsync(car); // adds Car to DB locally
        await _context.SaveChangesAsync(); // save changes

        return car; // return the same car
    }

    public async Task<bool> DeleteCarAsync(Guid id)
    {
        var car = await _context.Cars.FindAsync(id); // try find the car

        if (car != null)
        {
            _context.Cars.Remove(car); // remove the car
            await _context.SaveChangesAsync(); // save the change

            return true; // deletion succeeded
        }

        return false;
    }

    public async Task<int> CountCarsAsync()
    {
        return await _context.Cars.CountAsync();
    }
}