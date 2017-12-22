using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace Day22
{
    /// <summary>
    /// Counts the number of elements infected by a virus in an infinite-grid
    /// for a given number of bursts (steps)
    ///
    ///   +--- >
    ///   |    x
    ///   v y
    ///
    /// </summary>
    public static class Program
    {
        private const char Clean = '.';
        private const char Infected = '#';

        public static void Main()
        {
            var inputData = File.ReadAllLines("Input.txt");
            var infections = CountInfections(10000, inputData);
            Console.WriteLine($"The total number of infections is: {infections}");
        }

        /// <summary>
        /// Counts the number of times a virus infects for a given number of iterations
        /// </summary>
        public static int CountInfections(int bursts, IEnumerable<string> mapData)
        {
            var infections = 0;

            var (infiniteGrid, (x, y)) = CreateInfiniteGrid(500, mapData.ToList());
            var orientation = new Orientation(Direction.Up);

            foreach (var _ in Enumerable.Range(1, bursts))
            {
                var currentElement = infiniteGrid[y, x];
                switch (currentElement)
                {
                    case Clean:
                        infiniteGrid[y, x] = Infected;
                        orientation.TurnLeft();
                        infections++;
                        break;

                    case Infected:
                        infiniteGrid[y, x] = Clean;
                        orientation.TurnRight();
                        break;

                    default:
                        throw new InvalidOperationException($"Got unexpected element: {currentElement}");
                }

                (x, y) = GetNextLocation((x, y), orientation.Current);
            }

            return infections;
        }

        /// <summary>
        /// Creates a dummy infinite grid of the provided size
        /// </summary>
        private static (char[,] grid, (int x, int y) start) CreateInfiniteGrid(int length, IList<string> mapData)
        {
            var grid = CreateGrid(length, '.');

            (int x, int y) start = (length / 2, length / 2);

            // Insert the specified map into the centre of the input grid
            var lines = mapData.Count;
            foreach (var i in Enumerable.Range(0, lines))
            {
                var line = mapData[i];

                foreach (var j in Enumerable.Range(0, line.Length))
                {
                    var x = start.x - line.Length / 2 + j;
                    var y = start.y - lines / 2 + i;
                    grid[y, x] = line[j];
                }
            }

            return (grid, start);
        }

        private static char[,] CreateGrid(int length, char defaultCharacter)
        {
            var grid = new char[length, length];
            foreach (var i in Enumerable.Range(0, grid.GetLength(0)))
            {
                foreach (var j in Enumerable.Range(0, grid.GetLength(1)))
                {
                    grid[i, j] = defaultCharacter;
                }
            }

            return grid;
        }

        private static (int x, int y) GetNextLocation((int x, int y) current, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    current.y--;
                    break;
                case Direction.Down:
                    current.y++;
                    break;
                case Direction.Left:
                    current.x--;
                    break;
                case Direction.Right:
                    current.x++;
                    break;
                default:
                    throw new InvalidOperationException($"Could not find provided direction: {direction}");
            }

            return (current.x, current.y);
        }
    }
}