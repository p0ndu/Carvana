using Microsoft.EntityFrameworkCore;
using Carvana.Services;

namespace Carvana.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<RentalContract> RentalContracts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // car table
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.CarId);

                // many cars are the same model
                entity.HasOne(e => e.CarModel)
                      .WithMany(m => m.Cars)
                      .HasForeignKey(e => e.ModelID)
                      .IsRequired();

                entity.Property(e => e.LicensePlate).IsRequired();

                entity.Property(e => e.Features)
                      .HasColumnType("text[]");
                // etc.

            });

            // model table
            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasKey(e => e.ModelID);


                entity.Property(e => e.Brand).IsRequired();
                entity.Property(e => e.NumSeats).IsRequired();

                // optional: store VehicleType as string
                entity.Property(e => e.VehicleType)
                      .HasConversion<string>()
                      .IsRequired();

            });

            // rental contract table
            modelBuilder.Entity<RentalContract>(entity =>
            {
                entity.HasKey(e => e.ContractID);

                // one car will be in many contracts 
                entity.HasOne(rc => rc.Car)
                      .WithMany() 
                      .HasForeignKey(rc => rc.CarID)
                      .IsRequired();

                // one customer may have many contracts 
                entity.HasOne(rc => rc.Customer)
                      .WithMany(c => c.RentalContracts)
                      .HasForeignKey(rc => rc.CustomerID)
                      .IsRequired();
            });

            // customer table
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerID);
                
                // each customer only has one license 
                entity.HasOne(c => c.License)
                      .WithOne(l => l.Customer)
                      .HasForeignKey<Customer>(c => c.LicenseNumber)
                      .IsRequired();
            });

            // license table 
            modelBuilder.Entity<License>(entity =>
            {
                entity.HasKey(e => e.LicenseNumber);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
