namespace Carvana
{
    public class Insurance
    {
        private string PolicyNumber;
        private string Provider;
        private DateTime StartDate;
        private DateTime ExpiryDate;
        private string CoverageType;
        private string Status;

        public Insurance(string policyNumber, string provider, DateTime startDate, DateTime expiryDate, string coverageType, string status)
        {
            this.PolicyNumber = policyNumber;
            this.Provider = provider;
            this.StartDate = startDate;
            this.ExpiryDate = expiryDate;
            this.CoverageType = coverageType;
            this.Status = status;
        }

        //Getters

        //Get the policy number
        public string GetPolicyNumber()
        {
            return PolicyNumber;
        }

        //Get the provider
        public string GetProvider()
        {
            return Provider;
        }

        //Get the start date
        public DateTime GetStartDate()
        {
            return StartDate;
        }

        //Get the expiry date
        public DateTime GetExpiryDate()
        {
            return ExpiryDate;
        }

        //Get the coverage type
        public string GetCoverageType()
        {
            return CoverageType;
        }

        //Get the status
        public string GetStatus()
        {
            return Status;
        }

        //Setters

        //Set the policy number
        public void SetPolicyNumber(string newPolicyNumber)
        {
            this.PolicyNumber = newPolicyNumber;
        }

        //Set the provider
        public void SetProvider(string newProvider)
        {
            this.Provider = newProvider;
        }

        //Set the start date
        public void SetStartDate(DateTime newStartDate)
        {
            if (newStartDate > ExpiryDate)
            {
                throw new System.ArgumentException("Start date cannot be after the expiry date");
            }
            this.StartDate = newStartDate;
        }

        //Set the expiry date
        public void SetExpiryDate(DateTime newExpiryDate)
        {
            if (newExpiryDate < StartDate)
            {
                throw new System.ArgumentException("Expiry date cannot be before the start date");
            }
            else if (newExpiryDate < DateTime.Now)
            {
                throw new System.ArgumentException("Expiry date cannot be in the past");
            }
            this.ExpiryDate = newExpiryDate;
        }

        //Set the coverage type
        public void SetCoverageType(string newCoverageType)
        {
            this.CoverageType = newCoverageType;
        }

        //Set the status
        public void SetStatus(string newStatus)
        {
            this.Status = newStatus;
        }

        //Check if the insurance is valid
        public bool IsInsuranceValid()
        {
            return DateTime.Now <= ExpiryDate;
        }

        //Override the ToString method
        public override string ToString()
        {
            return $"Insurance: {PolicyNumber}, Provider: {Provider}, Coverage: {CoverageType}, " +
                   $"Valid Until: {ExpiryDate.ToShortDateString()}";
        }
    }

}