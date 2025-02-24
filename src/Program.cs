// See https://aka.ms/new-console-template for more icnformation

namespace Carvana
{
    public class Program
    {
        public static void Main(string[] args)
        {
           Node root = TreeManager.BuildFromFile("../../../src/TestTree.txt"); // hardcoded filepath because idfk c# is wonky

            // Print the tree before pruning
            TreeManager.PrintTree(root);
    
            // Prune the trie (this will apply your pruning logic)
            TreeManager.Prune(root);
    
            Console.WriteLine("Printing Tree again");
            TreeManager.PrintTree(root);
        }

    }
}