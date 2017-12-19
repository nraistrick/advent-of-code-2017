using System;

namespace Day15
{
    /// <summary>
    /// Generates two series of values and finds the number of values
    /// that have matching last significant bits
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var generatorA = new Generator(516, 16807);
            var generatorB = new Generator(190, 48271);
            var partialMatchCount = CountPartialMatches(40000000, generatorA, generatorB);
            Console.WriteLine($"The number of partial matches is: {partialMatchCount}");

            generatorA = new Generator(516, 16807, 4);
            generatorB = new Generator(190, 48271, 8);
            partialMatchCount = CountPartialMatches(5000000, generatorA, generatorB);
            Console.WriteLine($"The number of divisible partial matches is: {partialMatchCount}");
        }

        public static int CountPartialMatches(int range, Generator generatorA, Generator generatorB)
        {
            var matches = 0;

            for (var i = 0; i < range; i++)
            {
                var first  = generatorA.CalculateNext().GetBinaryRepresentation();
                var second = generatorB.CalculateNext().GetBinaryRepresentation();

                if (CheckLeastSignificantBitsMatch(first, second))
                {
                    matches++;
                }
            }

            return matches;
        }

        public static string GetBinaryRepresentation(this int value)
        {
            return Convert.ToString(value, 2).PadLeft(32, '0');
        }

        public static bool CheckLeastSignificantBitsMatch(string binarySequenceA, string binarySequenceB)
        {
            return binarySequenceA.Substring(binarySequenceA.Length - 16, 16) ==
                   binarySequenceB.Substring(binarySequenceB.Length - 16, 16);
        }
    }
}