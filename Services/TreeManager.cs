namespace Carvana.Services;

public class TreeManager // class which will manage the tree, will handle pruning and autocompletion
{
    private readonly INodeFactory _nodeFactory; // used to make nodes

    public TreeManager(INodeFactory nodeFactory)
    {
        _nodeFactory = nodeFactory;
    }

    // -------- Public API Methods --------

    public void Prune(Node root) // wrapper function to call recursive pruning algorithm
    {
        if (root == null)
        {
            throw new ArgumentNullException(nameof(root));
        }
        bool outcome = PruneInternal(root);
        Console.WriteLine(outcome ? "Tree Pruned" : "Error: Pruning Failed"); // output success or failure
    }

    public List<string> AutoComplete(Node root, string prefix = "") // returns top 5 results branching from node corresponding to prefix
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
        Node? endNode = FindBranch(root, word);

        if (endNode == null || !endNode.isFullWord()) // if endnode doesn't exist or is not a full word
        {
            return false;
        }
        endNode.IncrementWeight(); // increment the weight of the node
        return true;
    }

    // -------- Tree Manipulation (Private) --------

    private bool PruneInternal(Node root) // recursive function to prune the tree
    {
        if (root == null)
        {
            Console.WriteLine("unexpected null call");
            return false;
        }

        int numChildren = root.GetNumChildren();

        switch (numChildren)
        {
            case 0:
                return true;

            case 1:
                {
                    Node child = root.GetChildren()[0];

                    if (!root.isFullWord())
                    {
                        ReplaceData(root, child);
                    }

                    return PruneInternal(child);
                }

            default:
                return root.GetChildren().ToList().Select(child => PruneInternal(child)).Aggregate(false, (acc, result) => acc | result);
        }
    }

    private void ReplaceData(Node root, Node child) // replaces data of child with parent+child data
    {
        Node? parent = root.GetParent();

        if (parent != null)
        {
            child.SetData(root.GetData() + child.GetData());
            child.SetParent(parent);
            root.RemoveChild(child);
            parent.AddChild(child);
            parent.RemoveChild(root);
        }
    }

    // -------- Autocomplete Helpers (Private) --------

    private List<Node> GetSuggestions(Node root, string prefix) // returns list of children from branch corresponding to prefix
    {
        Node? foundBranch = FindBranch(root, prefix);
        return foundBranch?.GetChildren() ?? new List<Node>();
    }

    private void GetFullWord(Node node, string currentWord, List<string> words) // explores each branch to build complete words
    {
        currentWord += node.GetData();

        if (node.isFullWord())
        {
            words.Add(currentWord);
        }

        foreach (Node child in node.GetChildren())
        {
            GetFullWord(child, currentWord, words);
        }
    }

    private Node? FindBranch(Node root, string prefix) // searches for a branch matching input prefix
    {
        Node currentNode = root;

        while (!string.IsNullOrEmpty(prefix))
        {
            bool found = false;
            foreach (Node child in currentNode.GetChildren())
            {
                if (prefix.StartsWith(child.GetData(), StringComparison.OrdinalIgnoreCase))
                {
                    prefix = prefix.Substring(child.GetData().Length);
                    currentNode = child;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return null;
            }
        }

        return currentNode;
    }

    // -------- Sorting Utilities (Private) --------

    private void QuickSort(List<Node> nodes, int low, int high) // Qsort - written by aqab
    {
        if (low < high)
        {
            int pivotIndex = Partition(nodes, low, high);
            QuickSort(nodes, low, pivotIndex - 1);
            QuickSort(nodes, pivotIndex + 1, high);
        }
    }

    private int Partition(List<Node> nodes, int low, int high) // partition function - written by aqab
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

    private void Swap(List<Node> nodes, int i, int j) // swaps nodes - written by aqab
    {
        Node temp = nodes[i];
        nodes[i] = nodes[j];
        nodes[j] = temp;
    }

    private int CompareNodes(Node a, Node b) // compares nodes by weight then alphabetically - written by aqab
    {
        if (b.GetWeight() != a.GetWeight())
        {
            return b.GetWeight() - a.GetWeight();
        }
        return string.Compare(a.GetData(), b.GetData(), StringComparison.OrdinalIgnoreCase);
    }
}
