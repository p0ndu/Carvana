namespace Carvana
{
    public class Registration
    {
        private string RegistrationNumber;
        private string VIN;
        private DateTime RegistrationDate;
        private DateTime ExpiryDate;
        private string StateOfRegistration;

        public Registration(string registrationNumber, string vin, DateTime registrationDate,
                            DateTime expiryDate, string stateOfRegistration)
        {
            this.RegistrationNumber = registrationNumber;
            this.VIN = vin;
            this.RegistrationDate = registrationDate;
            this.ExpiryDate = expiryDate;
            this.StateOfRegistration = stateOfRegistration;
        }

        //Getters

        //Get the registration number
        public string GetRegistrationNumber()
        {
            return RegistrationNumber;
        }

        //Get the VIN
        public string GetVIN()
        {
            return VIN;
        }

        //Get the registration date
        public DateTime GetRegistrationDate()
        {
            return RegistrationDate;
        }

        //Get the expiry date
        public DateTime GetExpiryDate()
        {
            return ExpiryDate;
        }

        //Get the state of registration
        public string GetStateOfRegistration()
        {
            return StateOfRegistration;
        }

        //Setters

        //Set the registration number
        public void SetRegistrationNumber(string newRegistrationNumber)
        {
            this.RegistrationNumber = newRegistrationNumber;
        }

        //Set the registration date
        public void SetRegistrationDate(DateTime newRegistrationDate)
        {
            if (newRegistrationDate > DateTime.Now)
            {
                throw new System.ArgumentException("Registration date cannot be in the future");
            }
            this.RegistrationDate = newRegistrationDate;
        }

        //Set the expiry date
        public void SetExpiryDate(DateTime newExpiryDate)
        {
            if (newExpiryDate < RegistrationDate)
            {
                throw new System.ArgumentException("Expiry date cannot be before registration date");
            }
            else if (newExpiryDate < DateTime.Now)
            {
                throw new System.ArgumentException("Expiry date cannot be in the past");
            }
            this.ExpiryDate = newExpiryDate;
        }

        //Set the state of registration
        public void SetStateOfRegistration(string newStateOfRegistration)
        {
            this.StateOfRegistration = newStateOfRegistration;
        }

        //Check if the registration is valid
        public bool IsRegistrationValid()
        {
            return DateTime.Now <= ExpiryDate;
        }

        public override string ToString()
        {
            return $"Registration: {RegistrationNumber}, VIN: {VIN}, State: {StateOfRegistration}, " +
                   $"Valid Until: {ExpiryDate.ToShortDateString()}";
        }
    }

}