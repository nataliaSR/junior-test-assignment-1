using System;

namespace ThirdTask
{
    class Program
    {
        static void Main(string[] args)
        {   
            BinTree<int> tree = new BinTree<int>();

            /*
                     5
                    / \
                    3  9
                   / \  \
                  1   4  15
                         / \
                        12 20
            */
            tree.Add(5);
            tree.Add(3);
            tree.Add(9);
            tree.Add(1);
            tree.Add(4);
            tree.Add(15);
            tree.Add(12);
            tree.Add(20);

            foreach (var node in tree)
            {
                Console.WriteLine(node);
            }

            int height = tree.GetHeight(tree.Root);
            Console.WriteLine("Высота дерева:{0}", height);
        }
    }
}
