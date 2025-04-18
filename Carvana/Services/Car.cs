using System.ComponentModel.DataAnnotations;

namespace Carvana
{
    public enum VehicleStatus
    {
        Available,
        Rented,
        InMaintenance,
        OutOfService
    }

    public class Car
    {
        [Key] public Guid CarId { get; set; } // private key
        public Guid ModelID { get; set; } // foreign key to link to Model
        public Model CarModel { get; set; } // to navigate to model
        public VehicleStatus VehicleStatus { get; set; }
        public string LicensePlate { get; set; }
        public int Mileage { get; set; }
        public string Colour { get; set; }
        public int PricePerDay { get; set; }
        public List<String> Features { get; set; }

        public Car() { } // EFCore needs parameterless constructor

        private Car(Guid carId, Model model, VehicleStatus vehicleStatus, string licensePlate, int mileage, string colour, int pricePerDay, List<String> features) // private constructor for factory
        {

            CarId = carId;
            CarModel = model;

            if (model != null)
            {
                ModelID = model.ModelID;
            }
            else
            {
                ModelID = Guid.Empty;
            }

            VehicleStatus = vehicleStatus;
            LicensePlate = licensePlate;
            Mileage = mileage;
            Colour = colour;
            PricePerDay = pricePerDay;
            Features = features ?? [];
        }
        public static Car Create(Guid carId, Model model, VehicleStatus vehicleStatus, string licensePlate, int mileage, string colour, int pricePerDay, List<String> features) // factory constructor
        {
            return new Car(carId, model, vehicleStatus, licensePlate, mileage, colour, pricePerDay, features); // calls private constructor
        }
        public void AddMiles(int miles)
        {
            Mileage += miles;
        }
    }
}