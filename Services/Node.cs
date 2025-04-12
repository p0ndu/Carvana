namespace Carvana.Services;

public class Node // class for node of tree, going to be used for autocomplete, none of this uses pointers because were in c# and its kinda pointless(stop laughing)
{
    private string _data;
    private Node? _parent; // nullable in case root node 
    private List<Node>? _children; // nullable in case final node
    private bool _isFullWord; // flag to indicate whether or not the node is a full word
    private int _weight; // number of times this node has been selected by user. Used to sort possible words

    public Node(string data) // constructor for only data, to make root node
    {
        _data = data;
        _parent = null;
        _isFullWord = false;
        _weight = 0;
        _children = new List<Node>();
    }

    public Node(string data, Node parent) // constructor without children
    {
        _data = data;
        _parent = parent;
        _isFullWord = false;
        _weight = 0;
        _children = new List<Node>();
    }

    public void IncrementWeight() // increments weight, no decrement required
    {
        _weight += 1;
    }

    public void setFullWord()
    {
        _isFullWord = true;
    }

    public bool isFullWord()
    {
        return _isFullWord;
    }

    public void AddChild(Node child) // adds child to children
    {
        try
        {
            _children.Add(child);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error adding child:");
            Console.WriteLine(e);
            throw;
        }
    }

    public void RemoveChild(Node child) // attempts to remove child from children
    {
        try
        {
            _children.Remove(child);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error removing child:");
            Console.WriteLine(e);
            throw;
        }
    }

    public string GetData()
    {
        return _data;
    }

    public void SetData(string data)
    {
        _data = data;
    }

    public Node? GetParent()
    {
        return _parent;
    }

    public void SetParent(Node parent) // changes parent
    {
        _parent = parent;
    }

    public List<Node> GetChildren() // returns entire list of children
    {
        return _children;
    }

    public int GetNumChildren()
    {
        return _children.Count;
    }

    public int GetWeight()
    {
        return _weight;
    }

    public int CountAllNodes()
    {
        int count = 1; // count this node

        if (_children != null)
        {
            foreach (var child in _children)
            {
                count += child.CountAllNodes(); // recursively count children
            }
        }

        return count;
    }
}
