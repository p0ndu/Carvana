// See https://aka.ms/new-console-template for more icnformation

namespace Carvana
{
    public class Program
    {
        public static void Main(string[] args)
        {
           Node root = TreeManager.BuildFromFile("../../../src/TestTree.txt"); // hardcoded filepath because idfk c# is wonky

            // Print the tree before pruning
           TreeManager.VisualizeTree(root);
           int numNodes = TreeManager.CountNodes(root);
           TreeManager.Prune(root);
           TreeManager.VisualizeTree(root);
           int numNodesAfter = TreeManager.CountNodes(root);
           Console.WriteLine("Number of nodes before pruning - " + numNodes + "\nNumber of nodes after pruning - " + numNodesAfter + "\nReduced by " + ((double)(numNodes - numNodesAfter) / numNodes) * 100 + "%");

           List<Node> nodes = TreeManager.AutoComplete(root, "jo");

           foreach (var node in nodes)
           {
               Console.WriteLine(node.GetData());
           }


        }

    }
}