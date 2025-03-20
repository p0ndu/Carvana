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
        return await _context.Cars.ToListAsync(); // returns list of cars
    }

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
        
        return car; // return the same car
    }

    public async Task<bool> DeleteCarAsync(Guid id)
    {
        var car = await _context.Cars.FindAsync(id); // try find the car

        if (car != null)
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
            
            return true; // deletion succeeded
        }

        return false;
    }

    public async Task<int> CountCarsAsync()
    {
        return await _context.Cars.CountAsync();
    }
}