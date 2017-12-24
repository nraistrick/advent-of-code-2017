using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace Day24
{
    /// <summary>
    /// Constructs all possible bridges from a provided list of pieces. Each
    /// piece consists of two port numbers and must connect to another with
    /// a matching free port number.
    /// </summary>
    public static class Program
    {
        private const string PartDelimeter = "/";

        private static void Main()
        {
            var parts = File.ReadAllLines("Input.txt").ToList();
            var bridgeOptions = ConstructPossibleBridges(parts);
            var strongestBridge = FindStrongestBridge(bridgeOptions);

            Console.WriteLine($"The strongest bridge is: {strongestBridge}");
        }

        public static int FindStrongestBridge(IEnumerable<List<string>> possibleBridges)
        {
            return possibleBridges.Select(FindBridgeStrength).Concat(new[] {0}).Max();
        }

        public static int FindBridgeStrength(IEnumerable<string> parts)
        {
            return parts.Sum(p => p.Split(PartDelimeter).Sum(int.Parse));
        }

        /// <summary>
        /// Creates all possible bridges from the provided selection of parts
        /// </summary>
        public static IEnumerable<List<string>> ConstructPossibleBridges(List<string> availableParts)
        {
            var finishedBridges = new List<List<string>>();
            var bridgesUnderConstruction = new Queue<(List<string> bridge, List<string> parts)>();

            // Add all bridges with a port of zero as first possible parts
            foreach (var p in availableParts.Where(p => p.Split(PartDelimeter).Contains("0")))
            {
                var parts = availableParts.Clone().Where(a => a != p).ToList();
                bridgesUnderConstruction.Enqueue((new List<string> { p }, parts));
            }

            // Construct all possible bridges
            while (bridgesUnderConstruction.Count != 0)
            {
                var (bridge, parts) = bridgesUnderConstruction.Dequeue();

                // Get the available port to connect to
                string freePort = null;
                if (bridge.Count == 1)
                {
                    freePort = bridge.Last().Split(PartDelimeter).Max(int.Parse).ToString();
                }
                else if (bridge.Count > 1)
                {
                    var lastPorts        = bridge[bridge.Count - 1].Split(PartDelimeter).ToList();
                    var penultimatePorts = bridge[bridge.Count - 2].Split(PartDelimeter).ToList();
                    foreach (var p in penultimatePorts) lastPorts.Remove(p);

                    freePort = lastPorts.Single();
                }

                // Get all bridge pieces which can be connected
                var nextParts = parts.Where(p => p.Split(PartDelimeter).Contains(freePort)).ToList();

                // Store completed bridges
                if (nextParts.Count == 0) finishedBridges.Add(bridge);

                // Add bridges which require additional construction
                foreach (var part in nextParts)
                {
                    var updatedBridge = bridge.Clone().Append(part).ToList();
                    var remainingParts = parts.Clone().Where(p => p != part).ToList();

                    bridgesUnderConstruction.Enqueue((updatedBridge, remainingParts));
                }
            }

            return finishedBridges;
        }
    }
}