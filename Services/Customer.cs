using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carvana.Services;

namespace Carvana
{
    public class Customer
    {
        private readonly Guid _customerID; // private key
        private readonly License _license; // foreign key
        private string _password;
        private string _email;
        private string _fullName;
        private int _age;
        private string _phoneNumber;

        public Customer(Guid customerID, License license, String email, String fullName, Int32 age,
            String phoneNumber, String passowrd)
        {
            _customerID = customerID;
            _license = license;
            _email = email;
            _fullName = fullName;
            _age = age;
            _phoneNumber = phoneNumber;
            _password = passowrd;
        }
        
        // getters

        public Guid GetCustomerID()
        {
            return _customerID;
        }

        public License GetLicense()
        {
            return _license;
        }

        public String GetPassword()
        {
            return _password; 
        }

        public string GetEmail()
        {
            return _email;
        }

        public string getFullName()
        {
            return _fullName;
        }

        public string GetPhoneNumber()
        {
            return _phoneNumber;
        }

        public int GetAge()
        {
            return _age;
        }
        
        // setters

        public void SetPassword(String password)
        {
            _password = password;
        }

        public void SetEmail(String email)
        {
            _email = email;
        }

        public void SetFullName(String fullName)
        {
            _fullName = fullName;
        }

        public void SetPhoneNumber(String phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        public void SetAge(int age)
        {
            _age = age;
        }

        public void IncrementAge() // for birthdays
        {
            _age++;
        }
        
    }
}
