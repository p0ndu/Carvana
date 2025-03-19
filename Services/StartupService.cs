using Carvana.Controllers;
using Carvana.Data;
using Microsoft.EntityFrameworkCore;

namespace Carvana.Services;

public class StartupService
{
    public static void Run(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope(); // create scope
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); // store context

        context.Database.Migrate(); // check that DB is created and migrated

        if (!context.Cars.Any()) // if cars DB is empty
        {
            Console.WriteLine("No cars found in database");
        }
    }

    private static void TestConnection(ApplicationDbContext context) // TODO finish
    {
        context.Cars.Add(CreateTestCar()); // add a car to Car DB
        context.SaveChanges();
        
      //  CarRentalController.
    }

    private static Car CreateTestCar()
    {
        Guid id = Guid.NewGuid();
        return Car.Create(id, CreateTestModel(VehicleType.SUV), VehicleStatus.Available, "wh8neb", 4500, "green", 45);
    }

    private static Model CreateTestModel(VehicleType type)
    {
       Guid id = Guid.NewGuid();
       return Model.Create(id, type, "test", 2000, 4, 4); // returns test model
    }
}