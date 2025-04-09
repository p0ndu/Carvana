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

    // Outputs all cars in DB as a list
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

    public async Task<Model?> GetCarsByModelIDAsync(Guid modelId)
    {
        Model? output = await _context.Models.FindAsync(modelId);

        if (output == null) // if no model is found
        {
            Console.WriteLine("Error, model matching ModelID not found.");
            return null;
        }

        return output;
    }

    public async Task<IEnumerable<Model>> GetAllModelsAsync()
    {
        var output = await _context.Models.ToListAsync();
        return output;
    }

    // searches for car with matching primary key and returns as nullable car object
    public async Task<Car?> GetCarAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return null;
        }
        
        return await _context.Cars.FindAsync(id); // search DB for car matching ID
    }

    public async Task<Car?> AddCarAsync(Car car)
    {

        await _context.Cars.AddAsync(car); // adds Car to DB locally
        try
        {
            await _context.SaveChangesAsync(); // save changes     
        }
        catch (DbUpdateConcurrencyException)
        {
            return null;
        }
        
        _context.Cars.AddAsync(car); // adds Car to DB locally
        await _context.SaveChangesAsync(); // save changes


        return car; // return the same car
    }
    // Attempts to remove car with primary key matching ID, returns boolean to indicate success
    public async Task<bool> DeleteCarAsync(Guid id)
    {
        // try find the car in table
        Car? car = await _context.Cars.FindAsync(id);

        if (car != null) // if car was found
        {

            try
            {
                _context.Cars.Remove(car); // remove the car     
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }       

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

    public async Task<List<Car>?> GetCarsByModel(string modelName)
    {
        // i really don't enjoy DB work...
        Console.WriteLine(modelName); // print the model name to console for debugging

        if (string.IsNullOrWhiteSpace(modelName))
            return null;

        // normalize user input
        string lowerModelName = modelName.ToLower();

        // fetch all cars and their models from the database
        List<Car> allCars = await _context.Cars
            .Include(c => c.CarModel)
            .ToListAsync();

        // filter cars where the model name or brand matches any part of the search input
        List<Car> matchingCars = allCars
            .Where(c =>
                !string.IsNullOrEmpty(c.CarModel?.Name) &&
                !string.IsNullOrEmpty(c.CarModel?.Brand) &&
                (
                    lowerModelName.Contains(c.CarModel.Name.ToLower()) ||
                    lowerModelName.Contains(c.CarModel.Brand.ToLower()) ||
                    c.CarModel.Name.ToLower().Contains(lowerModelName) ||
                    c.CarModel.Brand.ToLower().Contains(lowerModelName)
                ))
            .ToList();

        // return matching cars if any
        return matchingCars.Any() ? matchingCars : null;
    }
}