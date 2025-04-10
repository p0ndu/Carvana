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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            string? result = await _customerService.Login(request.Email, request.Password);

            if (result == null)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(result);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] Customer customer)
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
            bool success = await _customerService.UpdateCustomer(newData);

            if (!success)
            {
                return BadRequest("Error updating profile.");
            }

            return Ok("Profile updated successfully.");
        }
    }

    // DTO class for login
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
