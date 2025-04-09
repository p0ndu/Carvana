using Carvana.Data;
using Microsoft.EntityFrameworkCore;

namespace Carvana.Services;

public class StartupService
{
    public static void Run(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope(); // create scope
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); // store context

        //TreeService _treeService = scope.ServiceProvider.GetRequiredService<TreeService>(); // create tree service
        

        Console.WriteLine("About to migrate DB");
        context.Database.Migrate(); // check that DB is created and migrated
        Console.WriteLine("Migrated DB");

        if (context.Cars.Any() || context.Customers.Any() || context.RentalContracts.Any())
        {
            Console.WriteLine("\n\n\n\n\nData found in DB\n\n\n\n");
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
        return Car.Create(id, CreateTestModel(VehicleType.SUV), VehicleStatus.Available, "wh8neb", 4500, "green", 45, ["AC", "4WD"]); // returns test car
    }

    private static Model CreateTestModel(VehicleType type)
    {
        Guid id = Guid.NewGuid();
        return Model.Create(id, type, "Tesla", "test", 2000, 4, 4); // returns test model
    }
}