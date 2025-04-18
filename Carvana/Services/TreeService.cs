namespace Carvana.Services // Wrapper class for handling tree operations
{
    public class TreeService
    {
        private readonly TreeManager _manager; // Manages tree operations
        private readonly DefaultNodeFactory _factory; // Factory to create nodes
        private readonly TreeLoader _loader; // Loads tree data from file
        private readonly Node _root; // Root node of the tree

        public TreeService(IHostEnvironment env) // Initializes service with environment data
        {
            string filepath = Path.Combine(env.ContentRootPath, "Data", "AutocompleteNames.txt");

            _factory = new DefaultNodeFactory();
            _manager = new TreeManager(_factory);
            _loader = new TreeLoader(filepath, _factory);
            _root = _loader.LoadFromFile();
            Prune(); // Prune the tree after loading
        }

        // ------------------- INIT -------------------------

        public void Initialise()
        {
            int numNodesBefore = _root.CountAllNodes(); 
            Prune();
            int numNodesAfter = _root.CountAllNodes(); 

            // Print reduction percentage
            float percentReduction = (numNodesBefore - numNodesAfter) / (float)numNodesBefore * 100f;

            Console.WriteLine($"Nodes before: {numNodesBefore}, after: {numNodesAfter}");
            Console.WriteLine($"{percentReduction}% reduction");
        }

        private void Prune()
        {
            _manager.Prune(_root); // Remove unnecessary nodes
        }

        // ------------------- FUNCTIONAL METHODS ------------------- 

        public List<string> Autocomplete(string query)
        {
            return _manager.AutoComplete(_root, query); // Get autocomplete suggestions
        }

        public bool IncrementWeight(string word)
        {
            return _manager.IncrementNodeWeight(word, _root); // Increase node weight for word
        }

        public void VisualiseTree()
        {
            TreeVisualiser.VisualizeTree(_root); // Visualize the tree
        }
    }
}
