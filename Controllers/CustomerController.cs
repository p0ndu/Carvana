using Carvana.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carvana.Controllers
{
    [ApiController]
    [Route("auth")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // ---------------------------
        // Authentication
        // ---------------------------

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string email, [FromQuery] string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Email and password are required."); // early return if missing input
            }

            string? result = await _customerService.Login(email, password);

            if (result == null)
            {
                return Unauthorized("Invalid credentials"); // no match in DB
            }
            else if (result.Length == 0)
            {
                return Unauthorized("No matching account found");
            }

            return Ok(result); // return email
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] CustomerData customer)
        {
            // check if account already exists (email or phone match)
            bool exists = await _customerService.CheckForDuplicates(customer.Email, customer.PhoneNumber);

            if (exists)
            {
                return BadRequest("Account with matching email or phone already exists.");
            }

            bool created = await _customerService.CreateCustomerAsync(customer);

            if (!created)
            {
                return BadRequest("Error creating account.");
            }

            return Ok("Account created successfully.");
        }

        // ---------------------------
        // Profile
        // ---------------------------

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile([FromQuery] string email)
        {
            var customer = await _customerService.GetCustomerByEmailAsync(email);

            if (customer == null)
            {
                return NotFound("Customer not found."); // no matching customer
            }

            return Ok(customer); // returns full customer profile
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] CustomerData newData, string customerGuidString = null)
        {

            if (newData.CustomerID == null && customerGuidString == null)
            {
                return BadRequest("Customer ID or Phone number are required.");
            }
            else if (newData.CustomerID == null)
            {
                newData.CustomerID = new Guid(customerGuidString);
            }
            // data MUST HAVE CUSTOMER GUID to identify the record being updated
            bool success = await _customerService.UpdateCustomerAsync(newData);

            if (!success)
            {
                return BadRequest("Error updating profile.");
            }

            return Ok("Profile updated successfully.");
        }
    }
}
