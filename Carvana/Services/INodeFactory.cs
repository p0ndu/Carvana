namespace Carvana.Services;

public interface INodeFactory // interface for node factories, to allow for DI instead of hardcoding new nodes
{
    Node CreateNode(string data, Node? parent = null); // default constructor
    Node CreateNode(string data); // constructor for only data, to make root node
}