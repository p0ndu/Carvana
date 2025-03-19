using Microsoft.EntityFrameworkCore;
using Carvana;
using Carvana.Services;

namespace Carvana.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // DbSets
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<RentalContract> RentalContracts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CAR
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.CarId);

                // Car -> Model (many Car to one Model)
                entity.HasOne(e => e.CarModel)
                      .WithMany(m => m.Cars)
                      .HasForeignKey(e => e.ModelID)
                      .IsRequired();

                entity.Property(e => e.LicensePlate).IsRequired();
                // etc.
            });

            // MODEL
            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasKey(e => e.ModelID);

                // optional: store VehicleType as string
                // entity.Property(e => e.VehicleType)
                //       .HasConversion<string>();
            });

            // RENTAL CONTRACT
            modelBuilder.Entity<RentalContract>(entity =>
            {
                entity.HasKey(e => e.ContractID);

                // RentalContract -> Car (many-to-one)
                entity.HasOne(rc => rc.Car)
                      .WithMany() // or with a collection if you prefer Car->Contracts
                      .HasForeignKey(rc => rc.CarID)
                      .IsRequired();

                // RentalContract -> Customer (many-to-one)
                entity.HasOne(rc => rc.Customer)
                      .WithMany(c => c.RentalContracts)
                      .HasForeignKey(rc => rc.CustomerID)
                      .IsRequired();
            });

            // CUSTOMER
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerID);

                // Customer -> License (1-to-1)
                entity.HasOne(c => c.License)
                      .WithOne(l => l.Customer) // If License has a Customer property
                      .HasForeignKey<Customer>(c => c.LicenseNumber)
                      .IsRequired(); 
            });

            // LICENSE
            modelBuilder.Entity<License>(entity =>
            {
                entity.HasKey(e => e.LicenseNumber);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
