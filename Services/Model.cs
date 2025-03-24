using System.ComponentModel.DataAnnotations;

namespace Carvana
{
    public enum VehicleType
        {
            Sedan,
            SUV,
            Truck,
            Coupe,
            Convertible,
            Van
        }
    public class Model
    {
        [Key] public Guid ModelID { get; set; } // private key
        public VehicleType VehicleType { get; }// type of vehicle it is
        public string Name { get; set; } // name of the model, i.e. "Civic"
        public int Year { get; } // year of the model, i.e. 2019
        public int NumDoors { get; } // number of doors
        public int NumSeats { get; } // number of seats
        
        public ICollection<Car> Cars { get; set; } = new List<Car>(); // many cars -> one model

        public Model() // parameterless constructor for EFCore
        {
        }

        private Model(Guid modelID, VehicleType vehicleType, string name, int year, int numDoors, int numSeats) // private constructor for factory
        {
            ModelID = modelID;
            VehicleType = vehicleType;
            Name = name;
            Year = year;
            NumDoors = numDoors;
            NumSeats = numSeats;
        }

        public static Model Create(Guid modelID, VehicleType type, string name, int year, int numDoors, int numSeats) // factory constructor
        {
            return new Model(modelID, type, name, year, numDoors, numSeats ); // calls private constructor
        }
    }
}