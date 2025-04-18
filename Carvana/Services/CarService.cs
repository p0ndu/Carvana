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

    // Outputs all cars in DB as a list, including their models
    public async Task<IEnumerable<Car>> GetCarsAsync()
    {
        var output = await _context.Cars.Include(c => c.CarModel).ToListAsync();

        foreach (var car in output)
        {
            Console.WriteLine($"Car: {car.CarId}, Model: {car.CarModel.Name}, Type: {car.CarModel.VehicleType}");
        }

        return output;
    }

    // Searches for car with matching primary key and returns it as a nullable car object
    public async Task<Car?> GetCarAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return null; // early return if ID is empty
        }

        return await _context.Cars.FindAsync(id);
    }

    // ------------------- GET MODELS -------------------

    // Returns a list containing all models in the DB
    public async Task<IEnumerable<Model>> GetAllModelsAsync()
    {
        return await _context.Models.ToListAsync();
    }

    // Searches for a model in DB with a matching Guid and returns it
    public async Task<Model?> GetCarsByModelIDAsync(Guid modelId)
    {
        Model? output = await _context.Models.FindAsync(modelId);

        if (output == null)
        {
            Console.WriteLine("Error, model matching ModelID not found.");
            return null; // no matching model found
        }

        return output;
    }

    // ------------------- SEARCH / FILTER -------------------

    // Returns a list of cars matching the modelName in DB
    public async Task<List<Car>?> GetCarsByModel(string modelName)
    {
        Console.WriteLine(modelName); // log for debugging

        if (string.IsNullOrWhiteSpace(modelName))
            return null; // return null if modelName is empty or just whitespace

        string lowerModelName = modelName.ToLower(); // case-insensitive search

        // Fetch all cars with their model data from DB
        List<Car> allCars = await _context.Cars
            .Include(c => c.CarModel)
            .ToListAsync();

        // Filter cars based on model name or brand containing the search term
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

        return matchingCars.Any() ? matchingCars : null; // return filtered cars or null if no match
    }

    // ------------------- ADD / DELETE -------------------

    // Adds car to DB and saves changes, returns success flag
    public async Task<bool> AddCarAsync(Car car)
    {
        bool success = true;

        await _context.Cars.AddAsync(car); // add car to context

        try
        {
            await _context.SaveChangesAsync(); // attempt to save changes
        }
        catch (DbUpdateConcurrencyException)
        {
            success = false; // handle concurrency issues
        }

        return success;
    }

    // Attempts to remove a car with primary key matching ID, returns boolean to indicate success
    public async Task<bool?> DeleteCarAsync(Guid id)
    {
        bool? success = true;
        Car? car = await _context.Cars.FindAsync(id);

        if (car != null)
        {
            try
            {
                _context.Cars.Remove(car); // remove car from context
                await _context.SaveChangesAsync(); // save changes to DB
            }
            catch (DbUpdateConcurrencyException)
            {
                success = false; // handle concurrency issues
            }
        }
        else
        {
            success = null; // return null if no matching car found
        }

        return success;
    }

    // ------------------- UTILITY -------------------

    // Returns the total number of cars in the DB
    public async Task<int> CountCarsAsync()
    {
        return await _context.Cars.CountAsync();
    }
}
