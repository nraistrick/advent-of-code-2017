using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            var regions = CountRegions(grid);
            Console.WriteLine($"The number of regions is: {regions}");
        }

        /// <summary>
        /// Counts the number of regions; a region is a number of adjacent binary 1's.
        /// Note this doesn't include diagonals.
        /// </summary>
        public static int CountRegions(List<string> grid)
        {
            var regions = 0;

            for (var y = 0; y < grid.Count; y++)
            {
                var x = grid[y].IndexOf('1');
                while (x != -1)
                {
                    RemoveConnectedSquares(x, y, grid);
                    x = grid[y].IndexOf('1');
                    regions++;
                }
            }

            return regions;
        }

        /// <summary>
        /// Recursively removes all squares connected to the current; these are
        /// the ones that make up a region
        /// </summary>
        private static void RemoveConnectedSquares(int x, int y, IList<string> grid)
        {
            var removedSquare = new StringBuilder(grid[y]) {[x] = '0'};
            grid[y] = removedSquare.ToString();

            if (x > 0 && grid[y][x - 1] == '1')
            {
                RemoveConnectedSquares(x - 1, y, grid);
            }

            if (x < grid[y].Length - 1 && grid[y][x + 1] == '1')
            {
                RemoveConnectedSquares(x + 1, y, grid);
            }

            if (y > 0 && grid[y - 1][x] == '1')
            {
                RemoveConnectedSquares(x, y - 1, grid);
            }

            if (y < grid.Count - 1 && grid[y + 1][x] == '1')
            {
                RemoveConnectedSquares(x, y + 1, grid);
            }
        }

        public static int GetUsedSquares(IEnumerable<string> grid)
        {
            return grid.Sum(CountNumberOfOnes);
        }

        public static List<string> CreateDefragmentationGrid(string key, int rows)
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