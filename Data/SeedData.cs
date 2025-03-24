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
            
            if (context.Cars.Any() || context.Customers.Any() || context.Licenses.Any() || context.Models.Any() || context.RentalContracts.Any())
            {
                return;   // DB isnt empty
            }
            
            var model = new Model
            {
                Name = "Tesla Model S"
            };
            context.Models.Add(model);
            context.SaveChanges();

            var license = new License
            {
                LicenseNumber = "ABC12345",
            };
            context.Licenses.Add(license);
            context.SaveChanges();

            var customer = new Customer
            {
                FullName = "John Doe", 
                LicenseNumber = "ABC12345"
            };
            context.Customers.Add(customer);
            context.SaveChanges();

            var car = new Car
            {
                LicensePlate = "XYZ9876",
                ModelID = model.ModelID
            };
            context.Cars.Add(car);
            context.SaveChanges();

            var rentalContract = new RentalContract
            {
                CarID = car.CarId,
                CustomerID = customer.CustomerID,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };
            context.RentalContracts.Add(rentalContract);
            context.SaveChanges();
        }
    }
}
