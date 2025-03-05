using Carvana.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carvana.Controllers
{
    [ApiController]
    [Route("checkout")]
    public class CheckoutController : ControllerBase
    {
        private readonly string? _paymentDB;

        [HttpPost()]
        public ActionResult checkout() // reserves car and processes transaction
        {
            return Ok();
        }
    }
}