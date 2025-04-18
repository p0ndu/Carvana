using Carvana.Data;
using Microsoft.EntityFrameworkCore;

namespace Carvana.Services;

public class StartupService // deprecated, only migrates DB now and ensures connection is working
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
}