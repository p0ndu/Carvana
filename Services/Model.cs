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
        private readonly Guid _modelID; // private key
        private readonly VehicleType _vehicleType; // type of vehicle it is
        private readonly string _name; // name of the model, i.e. "Civic"
        private readonly int _year; // year of the model, i.e. 2019
        private readonly int _numDoors; // number of doors
        private readonly int _numSeats; // number of seats

        public Model(Guid modelID, VehicleType vehicleType, string name, int year, int numDoors, int numSeats)
        {
            _modelID = modelID;
            _vehicleType = vehicleType;
            _name = name;
            _year = year;
            _numDoors = numDoors;
            _numSeats = numSeats;
        }

        public Guid GetModelID()
        {
            return _modelID;
        }

        public VehicleType GetVehicleType()
        {
            return _vehicleType;
        }
        public string getName() // get name
        {
            return _name;
        }
        public int getYear() // get year
        {
            return _year;
        }
        public int getNumDoors(){ // get number of doors
            return _numDoors;
        }
        public int getNumSeats(){ // get number of seats
            return _numSeats;
        }
    }
}