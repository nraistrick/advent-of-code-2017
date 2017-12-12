using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
    /// <summary>
    /// A program to balance memory allocation between various blocks
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var allocations = GetMemoryAllocations("Input.txt");
            var steps = FindStepsToRepeatConfiguration(allocations);

            Console.WriteLine($"Number of steps before we find a repeat configuration is: {steps}");
        }

        private static List<int> GetMemoryAllocations(string inputFile)
        {
            return File.ReadAllText(inputFile).Split().Select(int.Parse).ToList();
        }

        /// <summary>
        /// Count the number of steps before we see a cycle we've seen previously
        /// </summary>
        public static int FindStepsToRepeatConfiguration(List<int> memoryAllocations)
        {
            var configurations = new List<string>();
            var steps = 0;

            while (true)
            {
                configurations.Add(string.Join(",", memoryAllocations));
                if (configurations.Count != configurations.Distinct().Count()) { return steps; }
                ReallocateBlocks(memoryAllocations);
                steps++;
            }
        }

        /// <summary>
        /// Performs a redistribution cycle to fairly share memory usage between banks.
        /// This means picking the fullest memory bank and then evenly
        /// distributing its contents between all available memory banks.
        /// </summary>
        public static List<int> ReallocateBlocks(List<int> memoryBanks)
        {
            var maxValue = memoryBanks.Max();
            var maxIndex = memoryBanks.IndexOf(maxValue);

            // Remove the existing blocks for reallocatiion
            memoryBanks[maxIndex] = 0;

            // Reallocate the blocks
            while(maxValue > 0)
            {
                var index = ++maxIndex % memoryBanks.Count;
                memoryBanks[index]++;
                maxValue--;
            }

            return memoryBanks.ToList();
        }
    }
}