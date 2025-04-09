using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Linq; // for select and aggregate used in one-liner later on, not needed but let me have a little fun 

namespace Carvana.Services;

public class TreeManager // class which will manage the tree, will handle pruning and autocompletion
{
    private readonly INodeFactory _nodeFactory; // used to make nodes


    public TreeManager(INodeFactory nodeFactory)
    {
        _nodeFactory = nodeFactory;
    }
    public void Prune(Node root) // wrapper function to call recursive pruning algorithm
    {
        if (root == null)
        {
            throw new ArgumentNullException(nameof(root));
        }
        bool outcome = PruneInternal(root);
       Console.WriteLine(outcome ? "Tree Pruned" : "Error: Pruning Failed"); // output success or failure
    }
    
    public List<string> AutoComplete(Node root, string prefix = "") // returns top 5 results branching from node corresponding to prefix, default prefix value = "" so it always returns something
    {
        if (root == null)
        {
            throw new ArgumentNullException(nameof(root));
        }

        List<string> results = new List<string>();
        List<Node> nodes = GetSuggestions(root, prefix); // gets all words that the current prefix could be completed to

        QuickSort(nodes, 0, nodes.Count - 1); // sort nodes by weight
        
        foreach (Node word in nodes) // for each possible completion
        {
            GetFullWord(word, "", results); // explore branch, complete the word and add it to 'results'
        }

        return results.Take(5).ToList(); // returns first 5 nodes in list
    }

    public bool IncrementNodeWeight(string word, Node root) // finds branch corresponding to word, and increments its weight
    {
        Node endNode = FindBranch(root, word);

        if (endNode == null || !endNode.isFullWord()) // if endnode doesnt exist or is not a full word (error findig the correct node)
        {
            return false;
        }
        endNode.IncrementWeight(); // increment the weight of the node
        return true;
    }

  
    private void QuickSort(List<Node> nodes, int low, int high) // Qsort - aqab
    {
        if (low < high)
        {
            int pivotIndex = Partition(nodes, low, high);
            QuickSort(nodes, low, pivotIndex - 1);
            QuickSort(nodes, pivotIndex + 1, high);
        }
    }

    private int Partition(List<Node> nodes, int low, int high) // partition function - aqab
    {
        Node pivot = nodes[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (CompareNodes(nodes[j], pivot) < 0)
            {
                i++;
                Swap(nodes, i, j);
            }
        }

        Swap(nodes, i + 1, high);
        return i + 1;
    }

    private void Swap(List<Node> nodes, int i, int j) // swaps nodes - aqab
    {
        Node temp = nodes[i];
        nodes[i] = nodes[j];
        nodes[j] = temp;
    }

    private int CompareNodes(Node a, Node b) // compares nodes, first on weight then alphanumerically - aqab
    {
        if (b.GetWeight() != a.GetWeight())
        {
            return b.GetWeight() - a.GetWeight();
        }
        return string.Compare(a.GetData(), b.GetData(), StringComparison.OrdinalIgnoreCase);
    }

    private Node? FindBranch(Node root, string prefix) // searches for a branch from 'root' matching input prefix, output node can be NULL
    {
        Node currentNode = root;

        while(!string.IsNullOrEmpty(prefix)) // keep iterating until prefix is empty 
        {
            bool found = false;
            foreach (Node child in currentNode.GetChildren()) // check each child
            {
                if (prefix.StartsWith(child.GetData(), StringComparison.OrdinalIgnoreCase)) // if the beginning of prefix is the same as the data in child 
                {
                    prefix = prefix.Substring(child.GetData().Length); // remove part of prefix that was just found
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

    private void GetFullWord(Node node, string currentWord, List<string> words) // fully explores each branch of the tree, building the current word and then adding it to the list 'words'
    {
        currentWord += node.GetData(); // append data to end of word

        if (node.isFullWord()) // add word once its complete 
        {
            words.Add(currentWord);
        }

        foreach (Node child in node.GetChildren()) // recur for all children
        {
            GetFullWord(child, currentWord, words);
        }
    }

    private List<Node> GetSuggestions(Node root, string prefix) // returns list of all children from branch corresponding to prefix, if the branch cannot be found or there are no children the list will be empty
    {
        Node? foundBranch = FindBranch(root, prefix); // search for node corresponding to prefix
        return foundBranch?.GetChildren() ?? new List<Node>(); // quirky one-liner. ?? operator means if whatever is on its left is null, then it returns whats on its right. TLDR if foundbranch.getchildren gives null it returns an empty list as a default
    }

    private bool PruneInternal(Node root) // recursive function to prune the tree, returns false if error or true otherwise
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
                    Node child = root.GetChildren()[0]; // not using factory for readability

                    if (!root.isFullWord()) // only prune if node is not a full word
                    {
                        ReplaceData(root, child); // replace data and pointers for parent/child relationship
                    }

                    return PruneInternal(child); // recur on merged child
                }
            default:
                return root.GetChildren().ToList().Select(child => PruneInternal(child)).Aggregate(false, (acc, result) => acc | result); // one liner because i felt quirky
                                                                                                                                          // basically ORMAPs PruneInternal onto all children and returns the output

        }
        // if no children, end of branch
        // if only one child, append child data onto node data, replace child data with new node data, change child parent to node parent, recur on child, delete node
        // if multiple children, recur on children
    }

    private void ReplaceData(Node root, Node child) // replaces data of child with parent+child data, and updates pointers modelling parent child relationship to delete root node once it goes out of scope
    {
        Node parent = root.GetParent();

        if (parent != null) // if 'root' is the actual root of the tree you need to check its parent exists
        {
            child.SetData(root.GetData() + child.GetData()); // update child data to merge the two 
            child.SetParent(parent); // sets childs parent to the parent of 'root'
            root.RemoveChild(child); // removing the final refference to 'root', garbage collector will do its thing once it gets around to it
            parent.AddChild(child); // transfers parent relation to node above 'root', as 'root' is being removed
            parent.RemoveChild(root); // removes last pointer to 'root'
        }
    }
}
