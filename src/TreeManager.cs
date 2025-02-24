using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Linq; // for select and aggregate used in one-liner later on, not needed but let me have a little fun 

namespace Carvana;

public static class TreeManager // static class which will manage the tree, will handle pruning and autocompletion
{
    public static void Prune(Node root) // wrapper function to call recursive pruning algorithm
    {
        bool outcome = PruneInternal(root);
        if (outcome)
        {
            Console.WriteLine("Tree Pruned");
        }
        else if (!outcome)
        {
            Console.WriteLine("Error while pruning");
        }
    }

    public static void PrintTree(Node root) // dfs Tree traversal that prints data of each node
    {
        Console.WriteLine(root.GetData()); // prints data
        foreach (Node child in root.GetChildren())
        {
            PrintTree(child);
        }
    }

    private static bool PruneInternal(Node root) // recursive function to prune the tree, returns false if error or true otherwise
    {
        Console.WriteLine("Called on node with following data - " + root.GetData());
        if (root == null) // handling unexpected null call
        {
            Console.WriteLine("unexpected null call");
            return false;
        }

        int numChildren = root.GetNumChildren();

        switch (numChildren)
        {
            case 0: // no child, end of branch
                Console.WriteLine("Case 0");
                return true;
            case 1:
            {
                Console.WriteLine("Case 1");
                Node child = root.GetChildren()[0];
                ReplaceData(root, child); // replace data and pointers for parent/child relationship
                return PruneInternal(child); // recur on merged child
            }
            default:
            { 
                Console.WriteLine("Default case, Num children are " + numChildren);
                return root.GetChildren().ToList().Select(child => PruneInternal(child)).Aggregate(false, (acc, result) => acc | result); // one liner because i felt quirky
                                                                                                                                      // basically ORMAPs PruneInternal onto all children and returns the output
            }
        }
        // if no children, end of branch
        // if only one child, append child data onto node data, replace child data with new node data, change child parent to node parent, recur on child, delete node
        // if multiple children, recur on children
    }

    private static void ReplaceData(Node root, Node child) // replaces data of child with parent+child data, and updates pointers modelling parent child relationship to delete root node once it goes out of scope
    {
        Node parent = root.GetParent();
        if (parent != null) // if 'root' is the actual root of the tree you need to check its parent exists
        {
             child.SetData(root.GetData() + child.GetData()); // update child data to merge the two 
             child.SetParent(root.GetParent()); // sets childs parent to the parent of 'root'
             root.RemoveChild(child); // removing the final refference to 'root', garbage collector will do its thing once it gets around to it
             root.GetParent().AddChild(child); // transfers parent relation to node above 'root', as 'root' is being removed
             root.GetParent().RemoveChild(root); // removes last pointer to 'root'
        }
    }
}