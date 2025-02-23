using System;

namespace Carvana
{
    public enum VehicleStatus
    {
        Available,
        Rented,
        InMaintenance,
        OutOfService
    }

    public enum VehicleType
    {
        Sedan,
        SUV,
        Truck,
        Coupe,
        Convertible,
        Van
    }

    public class Car
    {
        private Model model; //Model details of the car
        private string licensePlate; //License plate of the car
        private Registration registration; //Registration details of the car
        private Insurance insurance; //Insurance details of the car
        private VehicleStatus status; //Status of the car
        private VehicleType type; //Type of the car
        private string location; //Location of the car
        private string condition; //Condition of the car
        private Guid rentalId; //Rental ID
        private int distanceTravelled; //Distance travelled
        private DateTime lastServiceDate; //Last service date
        private DateTime nextServiceDate; //Next service date

        //Constructor
        public Car(Model model, string licensePlate, Registration registration, Insurance insurance, VehicleStatus status, VehicleType type, string location, string condition, Guid rentalId, int distanceTravelled, DateTime lastServiceDate, DateTime nextServiceDate)
        {
            this.model = model;
            this.licensePlate = licensePlate;
            this.registration = registration;
            this.insurance = insurance;
            this.status = status;
            this.type = type;
            this.location = location;
            this.condition = condition;
            this.rentalId = rentalId;
            this.distanceTravelled = distanceTravelled;
            this.lastServiceDate = lastServiceDate;
            this.nextServiceDate = nextServiceDate;
        }

        //Getters

        //Get Model
        public Model GetModel()
        {
            return model;
        }

        //Get License Plate
        public string GetLicensePlate()
        {
            return licensePlate;
        }

        //Get Registration
        public Registration GetRegistration()
        {
            return registration;
        }

        //Get Insurance
        public Insurance GetInsurance()
        {
            return insurance;
        }

        //Get Status
        public VehicleStatus GetStatus()
        {
            return status;
        }

        //Get Type
        public VehicleType GetCarType()
        {
            return type;
        }

        //Get Location
        public string GetLocation()
        {
            return location;
        }

        //Get Condition
        public string GetCondition()
        {
            return condition;
        }

        //Get Rental ID
        public Guid GetRentalId()
        {
            return rentalId;
        }

        //Get Distance Travelled
        public int GetDistanceTravelled()
        {
            return distanceTravelled;
        }

        //Get Last Service Date
        public DateTime GetLastServiceDate()
        {
            return lastServiceDate;
        }

        //Get Next Service Date
        public DateTime GetNextServiceDate()
        {
            return nextServiceDate;
        }

        //Setters (Update Methods)

        //Update Registration
        public void UpdateRegistration(Registration newRegistration)
        {
            this.registration = newRegistration;
        }

        //Update Insurance
        public void UpdateInsurance(Insurance newInsurance)
        {
            this.insurance = newInsurance;
        }

        //Update Status
        public void UpdateStatus(VehicleStatus newStatus)
        {
            this.status = newStatus;
        }

        //Update Location
        public void UpdateLocation(string newLocation)
        {
            this.location = newLocation;
        }

        //Update Condition
        public void UpdateCondition(string newCondition)
        {
            this.condition = newCondition;
        }

        //Update Rental ID
        public void UpdateRentalId(Guid newRentalId)
        {
            this.rentalId = newRentalId;
        }

        //Update Distance Travelled
        public void UpdateDistanceTravelled(int newDistanceTravelled)
        {
            if (newDistanceTravelled < this.distanceTravelled)
            {
                throw new ArgumentException("Distance travelled cannot be decreased");
            }
            this.distanceTravelled = newDistanceTravelled;
        }

        //Update Last Service Date
        public void UpdateLastServiceDate(DateTime newLastServiceDate)
        {
            if (newLastServiceDate > DateTime.Now)
            {
                throw new ArgumentException("Last service date must be in the past");
            }
            this.lastServiceDate = newLastServiceDate;
        }

        //Update Next Service Date
        public void ScheduleMaintenance(DateTime newServiceDate)
        {
            if (newServiceDate < DateTime.Now)
            {
                throw new ArgumentException("Maintenance date must be in the future");
            }
            this.nextServiceDate = newServiceDate;
        }

        //Check if the car is available
        public bool IsAvailable()
        {
            return status == VehicleStatus.Available;
        }

        //Check if the maintenance is due
        public bool IsMaintenanceDue()
        {
            return DateTime.Now >= nextServiceDate;
        }


        //ToString Method
        public override string ToString() => $"Manufacturer: {model.getManufacturer()}, Name: {model.getName()}, Year: {model.getYear()}, " +
                   $"Colour: {model.getColour()}, Price: {model.getPrice()}, Number of Doors: {model.getNumDoors()}, " +
                   $"Number of Seats: {model.getNumSeats()}, Is Manual: {model.getIsManual()}, " +
                   $"Engine: {model.getEngine()}, License Plate: {licensePlate}, " +
                   $"Current Rental ID: {rentalId}, Status: {status}, Location: {location}, Condition: {condition}, " +
                   $"Distance Travelled: {distanceTravelled}, Last Service Date: {lastServiceDate}, " +
                   $"Next Service Date: {nextServiceDate}";
    }
}