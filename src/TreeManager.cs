using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Linq; // for select and aggregate used in one-liner later on, not needed but let me have a little fun 

namespace Carvana;

public static class TreeManager // static class which will manage the tree, will handle pruning and autocompletion
{

    public static Node BuildFromFile(string filePath)
    {
        Node root = new Node(""); // creates root node

        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();
            if (!string.IsNullOrEmpty(trimmedLine)) // if line is not null
            {
                InsertWord(root, trimmedLine); // inserts the word into the trie
            }
        }
        return root; // returns root node as sort of header for whole tree
    }
    
    
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

    public static List<Node> AutoComplete(Node root, string prefix = "") // returns top 5 results branching from node corresponding to prefix, default prefix value = "" so it always returns something
    {
        List<Node> nodes = GetSuggestions(root, prefix); // gets all possible completions of prefix
        
        nodes.Sort(); // sorting list alphabetically for now, will add proper weights to it later

        return nodes.Take(5).ToList(); // returns first 5 nodes in list
    }

    private static Node? FindBranch(Node root, string prefix) // searches for a branch from 'root' matching input prefix, output node can be NULL
    { 
        Node currentNode = root;
        
        foreach (char character in prefix) // iterate over each charafter 
        {
            bool found = false;
            foreach (Node child in currentNode.GetChildren()) // check each child
            {
                if (child.GetData().Equals(character.ToString(), StringComparison.OrdinalIgnoreCase)) // try find character in child data
                {
                    currentNode = child; // update node being checked
                    found = true; // tell program to continue searching
                    break; // break inner foreach
                }
            }

            if (!found) // if data is not in branch
            {
                return null;
            }
        }
        
       return currentNode;
    }

    private static List<Node> GetSuggestions(Node root, string prefix) // returns list of all children from branch corresponding to prefix, if the branch cannot be found or there are no children the list will be empty
    {
        Node foundBranch = FindBranch(root, prefix); // search for branch

        if (foundBranch == null) // if branch cant be found
        {
            return new List<Node>(); // return empty list
        }
        else
        {
            List<Node> suggestions = foundBranch.GetChildren(); // return all children of branch
            return suggestions;
        }
    }
    private static bool PruneInternal(Node root) // recursive function to prune the tree, returns false if error or true otherwise
    {
        if (root == null) // handling unexpected null call
        {
            Console.WriteLine("unexpected null call");
            return false;
        }

        int numChildren = root.GetNumChildren();

        switch (numChildren)
        {
            case 0: // no child, end of branch
                return true;
            case 1:
            {
                Node child = root.GetChildren()[0];
                ReplaceData(root, child); // replace data and pointers for parent/child relationship
                return PruneInternal(child); // recur on merged child
            }
            default:
            { 
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

    private static void InsertWord(Node root, string word) // inserts word into the trie one char at a time
    {
        bool isNewBranch = false; // flag to mark wether we are inserting to a new branch, to avoid checking the children of nodes we just created
        Node currentNode = root;
        
        foreach (char letter in word) // try to find each letter and if not found, add it. 
        {
            bool found = false; // flag tracking if a letter was found or not

            if (!isNewBranch) // only checks children if it is not a new branch
            {
                foreach (Node child in currentNode.GetChildren()) // check eah child
                {
                    if (child.GetData().ToLower().Equals(letter.ToString(), StringComparison.OrdinalIgnoreCase)) // if node matches data being inserted
                    {
                        found = true;
                        currentNode = child; // move to child node and search for the next letter
                        break;
                    }
                }
            }

            if (!found) // if no matching node is found then make one
            {
                isNewBranch = true; // updates flag
                Node newNode = new Node(letter.ToString(), currentNode); // create new node containing letter as data with current node as parent
                currentNode.AddChild(newNode); // set new node to child of current node
                currentNode = newNode; // continue searching for rest of word from newNode onwards, it wont be found as its a new branch meaning it will just create the word branching each time
            }
        }
    }
}