using System.Reflection.Metadata.Ecma335;

namespace Carvana.Services;

public class TreeLoader : ITreeLoader // tree loader class, to move loading and building the tree into its own class
{
    private readonly string _path;
    private readonly INodeFactory _nodeFactory;

    public TreeLoader(string filePath, INodeFactory nodeFactory)
    {
        _path = filePath;
        _nodeFactory = nodeFactory;
    }

    public Node LoadFromFile()
    {
        //Node root = new Node(""); // creates root node

        Node root = _nodeFactory.CreateNode("");

        string[] lines = File.ReadAllLines(_path);
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
    
    private void InsertWord(Node root, string word) // inserts word into the trie one char at a time
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
                Node newNode = _nodeFactory.CreateNode(letter.ToString(), currentNode); // branches with current letter as data and current node as parent
                currentNode.AddChild(newNode); // set new node to child of current node
                currentNode = newNode; // continue searching for rest of word from newNode onwards, it wont be found as its a new branch meaning it will just create the word branching each time
            }
        }
        
        currentNode.setFullWord(); // once all letters have been added, set it as a complete word
    }
}