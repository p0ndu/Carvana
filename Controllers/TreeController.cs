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
        
        // prunes and displays the percentage reduction in nodes, will run on startup, left as endpoint during testing
        [HttpGet("initialise")] 
        public IActionResult Initialise()
        {
            Console.WriteLine("Initialise endpoint called");
            _treeService.Initialise();
            return Ok("Results outputted to console");
        }

        // returns 5 options with highest weight that branch from prefix
        [HttpGet] 
        public IActionResult GetAutocomplete([FromQuery] string prefix)
        {
            if (string.IsNullOrEmpty(prefix)) // check if string is empty
            {
                return BadRequest("Prefix is required for autocomplete");
            }
            
            var completions = _treeService.Autocomplete(prefix); // get autocomplete results
            
            return Ok(completions); // returns HTTP 200 ok result with completions
        }

        // increments the weight of a word
        [HttpGet("increment")] 
        public IActionResult IncrementWeight([FromQuery] string word)
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

        // prints graph of tree in console, for debugging
        [HttpGet("printTree")] 
        public IActionResult VisualiseTree()
        {
            try
            {
                _treeService.VisualiseTree(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
    }
}
