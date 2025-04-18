using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Carvana.Services;

namespace Carvana
{
    public class Customer
    {
        [Key] public Guid CustomerID { get; set; } // private key
        public string LicenseNumber { get; set; } // foreign key
        public License? License { get; set; } // navigation to license
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public ICollection<RentalContract> RentalContracts { get; set; } = new List<RentalContract>(); // 1 customer -> many contracts

        public Customer() { } // parameterless constructor for EFCore
        private Customer(Guid customerID, License license, string email, string fullName, int age, string phoneNumber, string password) // private constructor for factory
        {
            CustomerID = customerID;
            License = license;

            if (license != null)
            {
                LicenseNumber = license.LicenseNumber;
            }
            else
            {
                LicenseNumber = String.Empty;
            }

            Email = email;
            FullName = fullName;
            Age = age;
            PhoneNumber = phoneNumber;
            Password = password;
        }

        // public method to call the private constructor as EFCore requires a public one with no variables
        public static Customer Create(Guid customerID, License license, string email, string fullName, int age,
            string phoneNumber, string password)
        {
            if (customerID == Guid.Empty)
            {
                customerID = Guid.NewGuid();
            }
            return new Customer(customerID, license, email, fullName, age, phoneNumber, password);
        }


        // for birthdays
        public void IncrementAge()
        {
            Age++;
        }

    }
}
