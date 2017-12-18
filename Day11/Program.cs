using System;
using System.Collections.Generic;
using System.IO;

namespace Day11
{
    /// <summary>
    /// Counts the steps taken through an infinite hexagonal grid with the
    /// below directional layout.
    ///
    ///     \ n  /
    ///   nw +--+ ne
    ///     /    \
    ///   -+      +-
    ///     \    /
    ///   sw +--+ se
    ///     / s  \
    ///
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var inputData = File.ReadAllText("Input.txt").Split(",");
            var steps = CalculateStepsFromOrigin(inputData);
            Console.WriteLine($"The steps from origin are: {steps}");
        }

        public static int CalculateStepsFromOrigin(IEnumerable<string> instructions)
        {
            var (north, south, east, west) = (0, 0, 0, 0);

            foreach (var i in instructions)
            {
                switch (i)
                {
                    case "n":
                        north += 2;
                        break;
                    case "ne":
                        north += 1; east += 1;
                        break;
                    case "se":
                        south += 1; east += 1;
                        break;
                    case "s":
                        south += 2;
                        break;
                    case "sw":
                        south += 1; west += 1;
                        break;
                    case "nw":
                        north += 1; west += 1;
                        break;
                    default:
                        throw new InvalidOperationException($"Did not recognise direction {i}");
                }
            }

            return (Math.Abs(north - south) + Math.Abs(east - west)) / 2;
        }
    }
}