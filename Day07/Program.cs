using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day07
{
    /// <summary>
    /// Finds the root program from a program tree structure and checks for a
    /// single imbalance program
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var fileData = File.ReadAllLines("Input.txt").ToList();

            var programNames = GetProgramNames(fileData);
            var rootProgram = GetRootProgram(programNames);

            Console.WriteLine($"The root program is {rootProgram}");

            var programs = CreateProgramNodes(fileData);
            LinkProgramNodes(programs, fileData);

            var tree = programs[rootProgram];
            CombineProgramWeights(tree);

            tree.PrintPretty("", true);

            var imbalancedNode = FindImbalancedNode(tree);
            Console.WriteLine();
            Console.WriteLine($"There is an imbalance of {imbalancedNode.Imbalance} " +
                              $"in program: {imbalancedNode.Node.Id}({imbalancedNode.Node.Weight})");
        }

        /// <summary>
        /// Determines the root program by figuring out which program
        /// is only referenced once in the provided program names. Due to the
        /// nature of the input, all other programs will be referenced at
        /// one or more times.
        /// </summary>
        public static string GetRootProgram(IList<string> programNames)
        {
            var rootProgram = from   name in programNames
                              where  programNames.Count(n => n == name) == 1
                              select name;

            return rootProgram.Single();
        }

        /// <summary>
        /// Parse the input file to extract all provided program names
        /// </summary>
        public static List<string> GetProgramNames(IEnumerable<string> fileData)
        {
            var programNames = new List<string>();
            foreach (var line in fileData)
            {
                var entries = line.Replace(",", string.Empty).Split().ToList();
                entries = entries.Where(e => Regex.IsMatch(e, @"^[a-zA-Z]+$")).ToList();
                programNames.AddRange(entries);
            }

            return programNames;
        }

        /// <summary>
        /// Creates a set of nodes to represent each program
        /// </summary>
        public static Dictionary<string, Node> CreateProgramNodes(IEnumerable<string> fileData)
        {
            const string inputPattern = @"^([a-z]{4,}) \((\d+)\)";

            var programs = new Dictionary<string, Node>();
            foreach (var line in fileData)
            {
                var match = Regex.Match(line, inputPattern);
                var name = match.Groups[1].Value;
                var weight = int.Parse(match.Groups[2].Value);

                programs.Add(name, new Node(name, weight));
            }

            return programs;
        }

        /// <summary>
        /// Links the existing program nodes together
        /// </summary>
        public static void LinkProgramNodes(Dictionary<string, Node> programs,
                                            IEnumerable<string> fileData)
        {
            const string inputPattern = @"^([a-z]{4,}) .* -> (.*)$";

            foreach (var line in fileData)
            {
                var match = Regex.Match(line, inputPattern);
                if (!match.Success) { continue; }

                var name = match.Groups[1].Value;
                var children = match.Groups[2].Value.Split(", ");
                foreach (var child in children)
                {
                    programs[name].Children.Add(programs[child]);
                }
            }
        }

        /// <summary>
        /// Cumulatively sums the weights of all the nodes in the tree
        /// </summary>
        public static int CombineProgramWeights(Node node)
        {
            node.CombinedWeight = node.Weight + node.Children.Sum(CombineProgramWeights);
            return node.CombinedWeight;
        }

        /// <summary>
        /// Finds the first weight that has children with an imbalanced
        /// cumulative weight
        /// </summary>
        public static (Node Node, uint? Imbalance) FindImbalancedNode(Node node)
        {
            uint? imbalance = null;

            while (true)
            {
                var weights = node.Children.GroupBy(x => x.CombinedWeight).ToList();
                var imbalancedWeight = weights.SingleOrDefault(g => g.Count() == 1);
                var balancedWeight = weights.SingleOrDefault(g => g.Count() > 1);

                if (imbalancedWeight == null || balancedWeight == null) { break; }

                node = node.Children.Find(n => n.CombinedWeight == imbalancedWeight.Key);
                imbalance = (uint)imbalancedWeight.Key - (uint)balancedWeight.Key;
            }

            return (node, imbalance);
        }
    }
}