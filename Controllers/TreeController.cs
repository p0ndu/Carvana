using Microsoft.AspNetCore.Mvc;
using Carvana.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Carvana.Controllers
{
    [ApiController]
    [Route("api/tree")] // not sure what route to use yet so im defaulting to this
    public class TreeController : ControllerBase // extends controllerbase to allow for BadRequest responses etc
    {
        private readonly TreeService _treeService;

        public TreeController(TreeService treeService)
        {
            this._treeService = treeService;
        }

        [HttpGet("autocomplete")]
        public IActionResult GetAutocomplete([FromQuery] string prefix) // IActionResult is abstract datatype allowing a variety of response types, helps make it more malleable
        {
            if (string.IsNullOrEmpty(prefix)) // check if string is empty
            {
                return BadRequest("Prefix is required for autocomplete");
            }
            
            var completions = _treeService.Autocomplete(prefix); // get autocomplete results
            
            return Ok(completions); // returns HTTP 200 ok result with completions
        }
        
    }
}