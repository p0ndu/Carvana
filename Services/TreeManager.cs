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
       Console.WriteLine(outcome ? "Tree Pruned" : "Error: Pruning Failed"); 
    }

    public List<string> AutoComplete(Node root, string prefix = "") // returns top 5 results branching from node corresponding to prefix, default prefix value = "" so it always returns something
    {
        if (root == null)
        {
            throw new ArgumentNullException(nameof(root));
        }
        
        List<string> results = new List<string>();
        List<Node> nodes = GetSuggestions(root, prefix); // gets all words that the current prefix could be completed to
        
        foreach (Node word in nodes) // for each possible completion
        {
            GetFullWord(word, "", results); // explore branch, complete the word and add it to 'results'
        } 

        results.Sort(); // sort results, will be changed later to implement a weight system
        return results.Take(5).ToList(); // returns first 5 nodes in list
    }

    private Node? FindBranch(Node root, string prefix) // searches for a branch from 'root' matching input prefix, output node can be NULL
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