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

    public class Car
    {
        private readonly Guid _carId; // private key
        private readonly Model _carModel; // foreign key
        private readonly VehicleStatus _vehicleStatus;
        private readonly string _licensePlate;
        private readonly int _mileage;
        private readonly string _colour;
        private int _pricePerDay; // not readonly as damage to the car could decrease rentable price


        public Car(Guid carId, Model model, VehicleStatus vehicleStatus, string licensePlate, int mileage,
            string colour, int pricePerDay)
        {
            _carId = carId;
            _carModel = model;
            _vehicleStatus = vehicleStatus;
            _licensePlate = licensePlate;
            _mileage = mileage;
            _colour = colour;
            _pricePerDay = pricePerDay;
        }

        // Getters
        public Guid GetCarId()
        {
            return _carId;
        }

        public Model GetModel()
        {
            return _carModel;
        }

        public VehicleStatus GetVehicleStatus()
        {
            return _vehicleStatus;
        }

        public string GetLicensePlate()
        {
            return _licensePlate;
        }

        public int GetMileage()
        {
            return _mileage;
        }

        public string GetColour()
        {
            return _colour;
        }

        public int GetPricePerDay()
        {
            return _pricePerDay;
        }

        public void SetPricePerDay(int price) // update price if damage occurs
        {
            _pricePerDay = price;
        }
    }
}