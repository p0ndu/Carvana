namespace Carvana.Services;

public static class TreeVisualiser // TreeVisualiser class, to move anything to do with printing/displaying the tree away from TreeManager
{
    public static void PrintTree(Node root) // dfs Tree traversal that prints data of each node, not going to be maintained as VisualiseTree is just better
    {
        Console.WriteLine(root.GetData()); // prints data
        foreach (Node child in root.GetChildren())
        {
            PrintTree(child);
        }
    }
    
    public static int CountNodes(Node root) // counts number of nodes
    {
        int count = 1; // count current node
        
        var children = root.GetChildren();
        for (int i = 0; i < children.Count; i++)
        {
            count += CountNodes(children[i]); // recur on chldren incrementing count, count increases kinda like a fibbonacci function
        }

        return count; 
    }
    
    public static void VisualizeTree(Node root, string indent = "", bool isLast = true) // prints out tree strucutre with nodes values
    {
        Console.Write(indent);
        Console.Write(isLast ? "└── " : "├── "); // found online, basically emojis to help lay out tree structure, if isLast then do L otherwise T
        Console.WriteLine(root.GetData());
        
        indent += isLast ? "    " : "│   "; // if it isLast (end of a branch) then add nothing to the end, otherwise add a pipe
        
        var children = root.GetChildren();
        for (int i = 0; i < children.Count; i++) // recur for all children of node
        {
            VisualizeTree(children[i], indent, i == children.Count - 1);
        }
    }
    
}