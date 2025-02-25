namespace Carvana.Autocomplete;

public class DefaultNodeFactory : INodeFactory
{
    public Node CreateNode(string data, Node? parent = null)
    {
        return new Node(data, parent);
    }

    public Node CreateNode(string data)
    {
        return new Node(data);
    }
}