using Carvana.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Carvana.Controllers
{
    [ApiController]
    [Route("auth")] // authentication route
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("/login")]
        public async Task<IActionResult> Login([FromQuery] string username, [FromQuery] string password) // logs user in via credentails passed
        {
            // result = bool
            var result = await _customerService.Login(username, password);
            // if details match
            if (result)
            {
                return Ok();
            }
            // details do not match
            return BadRequest();
        }


        [HttpPost("/signup")]
        public async Task<IActionResult> CheckCustomerDetails([FromBody] Customer customer) // signs the user up, push to DB
        {
            bool result = await _customerService.CheckForDuplicates(customer.Email, customer.PhoneNumber);

            if (!result) // if account was found
            {
                return BadRequest("Account with matching details aready exists.");
            }

            if (_customerService.CreateCustomerAsync(customer) != null)
            {
                return Ok();
            }
            else
            {
                return Problem("Error when creating account");
            }


        }

    }
}