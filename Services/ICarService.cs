namespace Carvana.Services;

public interface ICarService
{
    Task<IEnumerable<Car>> GetCarsAsync(); // returns all cars
    Task<Car?> GetCarAsync(Guid id); // gets a specific car
    Task<Car> AddCarAsync(Car car); // adds a car to DB
    Task<bool> DeleteCarAsync(Guid id); // deletes car from DB
    Task<int> CountCarsAsync(); // counts the number of cars from DB    
}