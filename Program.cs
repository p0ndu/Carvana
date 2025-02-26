using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carvana
{

    class Program
    {
        static void Main()
        {
            // Get Personal Info from User
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            string phone = Console.ReadLine();

            Console.Write("Enter Payment Info: ");
            string paymentInfo = Console.ReadLine();

            // Create Personal Info
            PersonalInfo person = new PersonalInfo(name, age, email, phone, paymentInfo);

            // Get License ID
            Console.Write("Enter License ID: ");
            string licenseId = Console.ReadLine();

            // Create Customer with License ID and Personal Info
            Customer customer2 = new Customer(licenseId, person);

            // Add History
            Console.Write("Enter a damage history record: ");
            string damage = Console.ReadLine();
            customer2.getHistory().addDamage(damage);

            Console.Write("Enter a rental history record: ");
            string rental = Console.ReadLine();
            customer2.getHistory().addRental(rental);

            // Display Customer Info
            Console.WriteLine("\nCustomer Info:");
            Console.WriteLine(customer2);

            ///////////////////////SEARCH ALGOS/////////////////////////////

            // Binary & Ternary Search 
            var numbers = new List<int> { 90, 155, 168, 180, 197, 201 };

            Console.Write("\nEnter a number to search (Binary Search): ");
            int searchBinary = int.Parse(Console.ReadLine());
            int binaryIndex = SearchAlgorithms<int>.BinarySearch(numbers, searchBinary);
            Console.WriteLine($"Binary Search: Index of {searchBinary} is {binaryIndex}");

            Console.Write("\nEnter a number to search (Ternary Search): ");
            int searchTernary = int.Parse(Console.ReadLine());
            int ternaryIndex = SearchAlgorithms<int>.TernarySearch(numbers, searchTernary);
            Console.WriteLine($"Ternary Search: Index of {searchTernary} is {ternaryIndex}");

            // Hash Search 
            var carDict = new Dictionary<string, string>
        {
            { "AA20AA", "BMW" },
            { "MD20XYZ", "Toyota" },
            { "MD10XYZ", "Audi" },
            { "MD11XYZ", "Seat" }
        };

            Console.Write("\nEnter Car License Plate to Search: ");
            string searchPlate = Console.ReadLine();

            if (SearchAlgorithms<string>.HashSearch(carDict, searchPlate, out string car))
                Console.WriteLine($"Found car: {car}");
            else
                Console.WriteLine("Car not found");
        }
    }


}









