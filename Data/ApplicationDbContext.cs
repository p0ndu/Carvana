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
            // car
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.CarId);

                // Car -> Model many-one 
                entity.HasOne(e => e.CarModel)
                      .WithMany(m => m.Cars)
                      .HasForeignKey(e => e.ModelID)
                      .IsRequired();

                entity.Property(e => e.LicensePlate).IsRequired();
            });

            // model
            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasKey(e => e.ModelID);
                
            });

            // rental contract 
            modelBuilder.Entity<RentalContract>(entity =>
            {
                entity.HasKey(e => e.ContractID);

                // RentalContract -> Car is many-many 
                entity.HasOne(rc => rc.Car)
                      .WithMany() // or with a collection if you prefer Car->Contracts
                      .HasForeignKey(rc => rc.CarID)
                      .IsRequired();

                // RentalContract -> Customer is many-many 
                entity.HasOne(rc => rc.Customer)
                      .WithMany(c => c.RentalContracts)
                      .HasForeignKey(rc => rc.CustomerID)
                      .IsRequired();
            });

            // customer
            modelBuilder.Entity<Customer>(entity =>
            {
                
                entity.HasKey(e => e.CustomerID);

                // to handle conversion between clean text and cyphertext in DB
                var encryptConverter = new EncryptedStringConverter();
                
                // convert phoneNumber
                entity.Property(e => e.PhoneNumber)
                    .HasConversion(encryptConverter);
                
                //convert password
                entity.Property(e => e.Password)
                    .HasConversion(encryptConverter);
                
                //convert licenseNumber
                entity.Property(e => e.Email)
                    .HasConversion(encryptConverter);
                
                // customer -> licese is 1-1
                entity.HasOne(c => c.License)
                      .WithOne(l => l.Customer) // If License has a Customer property
                      .HasForeignKey<Customer>(c => c.LicenseNumber)
                      .IsRequired(); 
            });

            // license
            modelBuilder.Entity<License>(entity =>
            {
                entity.HasKey(e => e.LicenseNumber);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
