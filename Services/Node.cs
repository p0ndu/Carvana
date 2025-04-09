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
        this._data = data;
        this._parent = null;
        this._isFullWord = false;
        this._weight = 0;

        this._children = new List<Node>();
    }

    public Node(string data, Node parent) // constructor without children
    {
        this._data = data;
        this._parent = parent;
        this._isFullWord = false;
        this._weight = 0;

        this._children = new List<Node>();
    }

    public
        Node(string data, Node parent, List<Node> children) // constructor in case you have the children ahead of time
    {
        this._data = data;
        this._parent = parent;
        this._isFullWord = false;
        this._weight = 0;
        this._children = children;
    }


    public void IncrementWeight() // increments weight, no decrement required
    {
        this._weight += 1;
    }

    public void setFullWord()
    {
        this._isFullWord = true;
    }


    public bool isFullWord()
    {
        return this._isFullWord;
    }

    public void AddChild(Node child) // adds child to children
    {
        try
        {
            this._children.Add(child);
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
            this._children.Remove(child);
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
        return this._data;
    }

    public void SetData(string data)
    {
        this._data = data;
    }

    public Node? GetParent()
    {
        return this._parent;
    }

    public List<Node> GetChildren() // returns entire list of children
    {
        return this._children;
    }

    public int GetNumChildren()
    {
        return this._children.Count;
    }

    public void SetParent(Node parent) // changes parent
    {
        this._parent = parent;
    }

    public int GetWeight()
    {
        return this._weight;
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


    public int GetWeight()
    {
        return this.weight;
    }
}

