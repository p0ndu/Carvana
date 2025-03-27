using Carvana;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Carvana.Services;

namespace Carvana.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            // ensure DB exists 
            context.Database.Migrate();

            var model = Model.Create(Guid.Empty, VehicleType.Convertible, "convertible Test", 2019, 4, 4);
            context.Models.Add(model);
            context.SaveChanges();

            var license = License.Create("123456789");
            context.Licenses.Add(license);
            context.SaveChanges();

            var customer = Customer.Create(Guid.Empty, license, "john.doe@gmail.com", "John Doe", 23, "07501091238", "Password");
            context.Customers.Add(customer);
            context.SaveChanges();

            var car = Car.Create(Guid.NewGuid(), model, VehicleStatus.Available, "123iuh2", 1000, "yellow", 50, ["AC", "4WD"]);
            context.Cars.Add(car);
            context.SaveChanges();

            var rentalContract = RentalContract.Create(Guid.NewGuid(), car, customer, DateTime.UtcNow, DateTime.UtcNow.AddDays(7), 1000, true);
            context.RentalContracts.Add(rentalContract);
            context.SaveChanges();
        }
    }
}
