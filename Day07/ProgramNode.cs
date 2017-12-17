using System;
using System.Collections.Generic;

namespace Day07
{
    public class Node
    {
        public string Id { get; }
        public int Weight { get; }
        public List<Node> Children { get; }
        public int CombinedWeight { get; set; }

        public Node(string id, int weight)
        {
            Id = id;
            Weight = weight;
            Children = new List<Node>();
            CombinedWeight = 0;
        }

        public void PrintPretty(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }
            Console.WriteLine($"{Id}({Weight})[{CombinedWeight}]");

            for (var i = 0; i < Children.Count; i++)
            {
                Children[i].PrintPretty(indent, i == Children.Count - 1);
            }
        }
    }
}