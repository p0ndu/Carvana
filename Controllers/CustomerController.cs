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
        public async Task<IActionResult> Login([FromQuery] string username, [FromQuery] string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Email and password are required.");
            }
            {
                string? result = await _customerService.Login(username, password);

                if (result == null)
                {
                    return Unauthorized("Invalid credentials");
                }

                return Ok(result);
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] CustomerData customer)
        {
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
                return NotFound("Customer not found.");
            }

            return Ok(customer);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] CustomerData newData)
        {
            bool success = await _customerService.UpdateCustomerAsync(newData);

            if (!success)
            {
                return BadRequest("Error updating profile.");
            }

            return Ok("Profile updated successfully.");
        }
    }
}
