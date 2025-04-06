using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Carvana.Services;
using CarRentalAPI.Helpers;

namespace Carvana
{
    public class Customer
    {
        [Key] public Guid CustomerID { get; set; } // Private key
        public string LicenseNumber { get; set; } // Foreign key
        public License License { get; set; } // Navigation to license
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<RentalContract> RentalContracts { get; set; } = new List<RentalContract>(); // 1 customer -> many contracts

        public Customer() { } // Parameterless constructor for EFCore

        private Customer(Guid customerID, License license, string email, string fullName, int age, string phoneNumber, string password) // Private constructor for factory
        {
            CustomerID = customerID;
            License = license;
            LicenseNumber = license != null ? license.LicenseNumber : String.Empty;

            // Encrypt sensitive fields before storing
            Email = EncryptionHelper.Encrypt(email);
            FullName = EncryptionHelper.Encrypt(fullName);
            PhoneNumber = EncryptionHelper.Encrypt(phoneNumber);
            Password = EncryptionHelper.Encrypt(password);
            Age = age;
        }

        public static Customer Create(Guid customerID, License license, string email, string fullName, int age, string phoneNumber, string password)
        {
            if (customerID == Guid.Empty)
            {
                customerID = Guid.NewGuid();
            }
            return new Customer(customerID, license, email, fullName, age, phoneNumber, password);
        }

        public void IncrementAge() // for birthdays
        {
            Age++;
        }

        // Decrypt the customer data when needed
        public void DecryptFields()
        {
            Email = EncryptionHelper.Decrypt(Email);
            FullName = EncryptionHelper.Decrypt(FullName);
            PhoneNumber = EncryptionHelper.Decrypt(PhoneNumber);
            Password = EncryptionHelper.Decrypt(Password);
        }
    }
}
