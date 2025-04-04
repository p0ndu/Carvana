using Microsoft.AspNetCore.Mvc;
using Carvana.Services;

namespace Carvana.Controllers
{
    [ApiController]
    [Route("search")] // not sure what route to use yet so im defaulting to this
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

        [HttpGet("initialise")] // Endpoint to manually prune and discplay the percentage reductin in nodes, For testing remove later
        public IActionResult PruneTree()
        {
            try
            {
                // prunes and counts the percentage reduction in nodes, then outputs results to console
                _treeService.CountReduction(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpGet("printTree")] // visualising tree endpoint
        public IActionResult VisualiseTree()
        {
            try
            {
                _treeService.VisualiseTree(); // should output to console(?)
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
    }
}