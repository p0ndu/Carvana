using CarRentalAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CarRentalAPI.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly string _filePath = "customers.json"; // Path to JSON file

        // Load customers from JSON file
        private List<Customer> LoadCustomersFromJson()
        {
            if (!System.IO.File.Exists(_filePath))
                return new List<Customer>();

            var jsonData = System.IO.File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Customer>>(jsonData) ?? new List<Customer>();
        }

        // Returns all customers
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = LoadCustomersFromJson();
            return Ok(customers);
        }

        //Search customer by License ID
        [HttpGet("search/{licenseID}")]
        public IActionResult SearchCustomer(string licenseID)
        {
            var customers = LoadCustomersFromJson(); // Load data

            var foundCustomer = SearchHelper<Customer>.HashSearch(customers, c => c.LicenseID, licenseID);

            if (foundCustomer == null)
                return NotFound("Customer not found");

            return Ok(foundCustomer);
        }
    }
}
