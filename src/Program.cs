// See https://aka.ms/new-console-template for more icnformation
using Carvana.Autocomplete; // autocomplete namespace for testing my code

namespace Carvana
{
    public class Program
    {
        public static void Main(string[] args)
        {
            INodeFactory nodeFactory = new DefaultNodeFactory(); // nodeFactory, handles creating nodes
            TreeManager manager = new TreeManager(nodeFactory); // manager, handles managing t
            TreeLoader loader = new TreeLoader("../../../src/TestTree.txt", nodeFactory); // hardcoded filepath because idfk c# is wonky
            Node root = loader.LoadFromFile();

            // Print the tree before pruning
            TreeVisualiser.VisualizeTree(root);
            int numNodes = TreeVisualiser.CountNodes(root);
            manager.Prune(root);
            TreeVisualiser.VisualizeTree(root);
            int numNodesAfter = TreeVisualiser.CountNodes(root);
            Console.WriteLine("Number of nodes before pruning - " + numNodes + "\nNumber of nodes after pruning - " + numNodesAfter + "\nReduced by " + ((double)(numNodes - numNodesAfter) / numNodes) * 100 + "%");

            List<string> results = manager.AutoComplete(root, "");

            foreach (var VARIABLE in results)
            {
                Console.WriteLine("" + VARIABLE);
            } 
           
            results.Clear();
            results = manager.AutoComplete(root, "f");

            foreach (var VARIABLE in results)
            {
                Console.WriteLine("f" + VARIABLE);
            } 

        }

    }
}