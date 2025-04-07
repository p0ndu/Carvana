using Carvana.Services;
using CarRentalAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace Carvana.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly string _filePath = "customers.json"; // Path to JSON file
        private readonly string? _userDB; // IMPLEMENT DATABASE 

        // Load customers from JSON and decrypt their data
        private List<Customer> LoadCustomersFromJson()
        {
            if (!System.IO.File.Exists(_filePath))
                return new List<Customer>();

            var jsonData = System.IO.File.ReadAllText(_filePath);
            var customers = JsonSerializer.Deserialize<List<Customer>>(jsonData) ?? new List<Customer>();

            // Decrypt customer data
            foreach (var customer in customers)
            {
                customer.FullName = EncryptionHelper.Decrypt(customer.FullName);
                customer.Email = EncryptionHelper.Decrypt(customer.Email);
                customer.LicenseNumber = EncryptionHelper.Decrypt(customer.LicenseNumber);
                customer.Password = EncryptionHelper.Decrypt(customer.Password);
            }

            return customers;
        }

        // Search API Endpoint
        [HttpGet("search/{licenseID}")]
        public IActionResult SearchCustomer(string licenseID)
        {
            var customers = LoadCustomersFromJson(); // Load and decrypt data

            // Search by encrypted License ID
            var foundCustomer = SearchHelper<Customer>.HashSearch(customers, c => c.LicenseNumber, EncryptionHelper.Encrypt(licenseID));

            if (foundCustomer == null)
                return NotFound("Customer not found");

            return Ok(foundCustomer);
        }

        
        [HttpGet("/login")]
        public IActionResult Login([FromQuery] string username, string password)
        {
            return Ok();
        }

        [HttpPost("/signup")]
        public IActionResult SignUp([FromQuery] string username, [FromQuery] string password)
        {
            return Ok();
        }
    }
}
