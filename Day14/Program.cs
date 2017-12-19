using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    /// <summary>
    /// Calculates information for a 128x128 defragementation file grid
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var grid = CreateDefragmentationGrid("hwlqcszp", 128);
            var usedSquares = GetUsedSquares(grid);
            Console.WriteLine($"The number of used squares is: {usedSquares}");
        }

        public static int GetUsedSquares(IEnumerable<string> grid)
        {
            return grid.Sum(CountNumberOfOnes);
        }

        public static IEnumerable<string> CreateDefragmentationGrid(string key, int rows)
        {
            return Enumerable.Range(0, rows).Select(row => GetKnotHashBinary($"{key}-{row}")).ToList();
        }

        public static string GetKnotHashBinary(string inputData)
        {
            var knotHash = Day10.Program.CalculateKnotHash(inputData);
            var binarystring = string.Join(string.Empty,
                knotHash.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            return binarystring;
        }

        public static int CountNumberOfOnes(string binaryData)
        {
            return binaryData.Count(c => c == '1');
        }
    }
}