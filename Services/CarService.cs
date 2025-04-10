using Carvana.Data;
using Microsoft.EntityFrameworkCore;

namespace Carvana.Services;

public class CarService : ICarService
{
    private readonly ApplicationDbContext _context;

    public CarService(ApplicationDbContext context)
    {
        _context = context;
    }

    // ------------------- GET CARS -------------------

    // Outputs all cars in DB as a list
    public async Task<IEnumerable<Car>> GetCarsAsync()
    {
        var output = await _context.Cars.Include(c => c.CarModel).ToListAsync();

        foreach (var car in output)
        {
            Console.WriteLine($"Car: {car.CarId}, Model: {car.CarModel.Name}, Type: {car.CarModel.VehicleType}");
        }

        return output;
    }

    // searches for car with matching primary key and returns as nullable car object
    public async Task<Car?> GetCarAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return null;
        }

        return await _context.Cars.FindAsync(id);
    }

    // ------------------- GET MODELS -------------------

    // returns list containing all models in DB
    public async Task<IEnumerable<Model>> GetAllModelsAsync()
    {
        return await _context.Models.ToListAsync();
    }

    // searches for model in DB with matching Guid
    public async Task<Model?> GetCarsByModelIDAsync(Guid modelId)
    {
        Model? output = await _context.Models.FindAsync(modelId);

        if (output == null)
        {
            Console.WriteLine("Error, model matching ModelID not found.");
            return null;
        }

        return output;
    }

    // ------------------- SEARCH / FILTER -------------------

    // returns list of cars matching modelName in DB
    public async Task<List<Car>?> GetCarsByModel(string modelName)
    {
        Console.WriteLine(modelName); // for debugging

        if (string.IsNullOrWhiteSpace(modelName))
            return null;

        string lowerModelName = modelName.ToLower();

        List<Car> allCars = await _context.Cars
            .Include(c => c.CarModel)
            .ToListAsync();

        List<Car> matchingCars = allCars
            .Where(c =>
                !string.IsNullOrEmpty(c.CarModel.Name) &&
                !string.IsNullOrEmpty(c.CarModel.Brand) &&
                (
                    lowerModelName.Contains(c.CarModel.Name.ToLower()) ||
                    lowerModelName.Contains(c.CarModel.Brand.ToLower()) ||
                    c.CarModel.Name.ToLower().Contains(lowerModelName) ||
                    c.CarModel.Brand.ToLower().Contains(lowerModelName)
                ))
            .ToList();

        return matchingCars.Any() ? matchingCars : null;
    }

    // ------------------- ADD / DELETE -------------------

    // adds car to DB and saves change
    public async Task<bool> AddCarAsync(Car car)
    {
        bool success = true;

        await _context.Cars.AddAsync(car);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            success = false;
        }

        return success;
    }

    // Attempts to remove car with primary key matching ID, returns boolean to indicate success
    public async Task<bool?> DeleteCarAsync(Guid id)
    {
        bool? success = true;
        Car? car = await _context.Cars.FindAsync(id);

        if (car != null)
        {
            try
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                success = false;
            }
        }
        else
        {
            success = null;
        }

        return success;
    }

    // ------------------- UTILITY -------------------

    // Returns total number of cars in DB
    public async Task<int> CountCarsAsync()
    {
        return await _context.Cars.CountAsync();
    }
}
