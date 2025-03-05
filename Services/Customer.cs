using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carvana
{
    public class Customer
    {
        private string licenseID; // Customer's license 
        private PersonalInfo personalInfo; // Personal details
        private History history; // Rental & damage history

        public Customer(string licenseID, PersonalInfo personalInfo)
        {
            this.licenseID = licenseID;
            this.personalInfo = personalInfo;
            this.history = new History(); // New customer starts with new history
        }

        public string getLicenseID() { return licenseID; }
        public PersonalInfo getPersonalInfo() { return personalInfo; }
        public History getHistory() { return history; }

        public override string ToString()
        {
            return $"License ID: {licenseID}\n{personalInfo}\n{history}";
        }
    }
}
