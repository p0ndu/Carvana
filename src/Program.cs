// See https://aka.ms/new-console-template for more icnformation

namespace Carvana
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create the root node (empty string)
            Node root = new Node("");

            // Branch for "apple":
            Node N_a = new Node("a", root);
            Node N_p = new Node("p", N_a);
            Node N_p2 = new Node("p", N_p);
            Node N_l = new Node("l", N_p2);
            Node N_e = new Node("e", N_l);

            // Branch for "ape" (shares "a" and "p", then diverges)
            Node N_e2 = new Node("e", N_p); // directly from N_p to form "ape"

            // Branch for "and":
            Node N_n = new Node("n", N_a);
            Node N_d = new Node("d", N_n);

            // Branch for "ant":
            Node N_t = new Node("t", N_n);

            // Branch for "apricot":
            Node N_r = new Node("r", N_p); // diverges from N_p for "apricot"
            Node N_i = new Node("i", N_r);
            Node N_c = new Node("c", N_i);
            Node N_o = new Node("o", N_c);
            Node N_t2 = new Node("t", N_o);

            // Fully set up parent/child relationships:
            root.AddChild(N_a);
    
            // For the "apple" branch:
            N_a.AddChild(N_p);
            N_p.AddChild(N_p2);
            N_p2.AddChild(N_l);
            N_l.AddChild(N_e);
    
            // Also from "a", add the branch for "and/ant":
            N_a.AddChild(N_n);
            N_n.AddChild(N_d);
            N_n.AddChild(N_t);
    
            // Also from N_p, add the "ape" branch and "apricot" branch:
            N_p.AddChild(N_e2);
            N_p.AddChild(N_r);
    
            // Build the "apricot" branch:
            N_r.AddChild(N_i);
            N_i.AddChild(N_c);
            N_c.AddChild(N_o);
            N_o.AddChild(N_t2);

            // Print the tree before pruning
            TreeManager.PrintTree(root);
    
            // Prune the trie (this will apply your pruning logic)
            TreeManager.Prune(root);
    
            Console.WriteLine("Printing Tree again");
            TreeManager.PrintTree(root);
        }

    }
}