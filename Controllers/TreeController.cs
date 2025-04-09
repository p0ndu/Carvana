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

        [HttpGet]
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
        public IActionResult Initialise()
        {
            Console.WriteLine("Initialise endpoint called");
            _treeService.Initialise();
            return Ok("Results outputted to console");
        }


        [HttpGet("increment/{word}")] // TODO change this to take the word from body instead of from route
        public IActionResult IncrementWeight([FromRoute] string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return BadRequest("Word is required for autocomplete");
            }

            bool result = _treeService.IncrementWeight(word);

            if (!result)
            {
                return StatusCode(500, "Error finding word");
            }

            
            return Ok(word);
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
