using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carvana
{
    public class PersonalInfo
    {
        private string name; // Customer name
        private int age; // Customer age
        private string email; // Email
        private string phoneNumber; // Phone number
        private string paymentInfo; // Payment details

        public PersonalInfo(string name, int age, string email, string phoneNumber, string paymentInfo)
        {
            this.name = name;
            this.age = age;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.paymentInfo = paymentInfo;
        }

        public string getName() { return name; }
        public int getAge() { return age; }
        public string getEmail() { return email; }
        public string getPhoneNumber() { return phoneNumber; }
        public string getPaymentInfo() { return paymentInfo; }

        public override string ToString()
        {
            return $"Name: {name}, Age: {age}, Email: {email}, Phone: {phoneNumber}, Payment: {paymentInfo}";
        }
    }
}
