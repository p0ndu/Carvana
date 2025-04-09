using System.IO;
namespace Carvana.Services; // wrapper class to handle all input and output from tree


public class TreeService
{
    private readonly TreeManager _manager;
    private readonly DefaultNodeFactory _factory; // node factory instance
    private readonly TreeLoader _loader; // tree loader instance
    private readonly Node _root; // root node pointing to the tree


    public TreeService(IHostEnvironment env) // environment passed to access testTree data file 
    {
        string filepath = Path.Combine(env.ContentRootPath, "Data", "TestTree.txt");

        _factory = new DefaultNodeFactory();
        _manager = new TreeManager(_factory);
        _loader = new TreeLoader(filepath, _factory);
        _root = _loader.LoadFromFile();
        Prune(); 
    }

    public void VisualiseTree()
    {
        TreeVisualiser.VisualizeTree(_root);
    }

    private void Prune()
    {
        _manager.Prune(_root);
    }

    public List<string> Autocomplete(string query)
    {
        return _manager.AutoComplete(_root, query);
    }


    public bool IncrementWeight(string word)
    {
       bool result =  _manager.IncrementNodeWeight(word, _root);
       return result;
    }

    public void CountReduction()
    {
        int numNodesBefore = _root.CountAllNodes();
        Prune();
        int numNodesAfter = _root.CountAllNodes();
        float percentReduction = (numNodesBefore - numNodesAfter) / (float)numNodesBefore * 100f;
        
        Console.WriteLine("Number of nodes before pruning - " + numNodesBefore + "\n" + "Number of nodes after pruning - " + numNodesAfter);
        Console.WriteLine(percentReduction + "% reduction in node count");
    }

    public bool IncrementWeight(string word)
    {
        bool result = _manager.IncrementNodeWeight(word, _root);
        return result;
    }

}

