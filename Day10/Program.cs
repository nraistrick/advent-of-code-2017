using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace Day10
{
    /// <summary>
    /// Generates and calculates information for a custom hash value
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var sequence = Enumerable.Range(0, 256).ToList();
            var inputData = File.ReadAllText("Input.txt");
            var lengths = inputData.Split(",").Select(int.Parse).ToList();

            var finalScramble = ScrambleNumbers(sequence, lengths).Last();
            var finalHash = string.Join(",", finalScramble);
            Console.WriteLine($"The final hash value is {finalHash}");

            var productOfFirstTwoValues = finalScramble[0] * finalScramble[1];
            Console.WriteLine($"The product of the first two values is {productOfFirstTwoValues}");
        }

        public static IEnumerable<List<int>> ScrambleNumbers(List<int> sequence,
                                                             IEnumerable<int> lengths)
        {
            int rotations = 0, skip = 0;

            foreach (var len in lengths)
            {
                // To allow part of the string to be reversed easily, we rotate
                // the list so the required selection begins at the first index
                sequence = sequence.RotateLeft(rotations);
                sequence.Reverse(0, len);
                sequence = sequence.RotateRight(rotations);

                yield return sequence.ToList();

                // Update the shift so we point at the first character after
                // the reversed selection with an additional skip value
                rotations += len + skip;
                rotations %= sequence.Count;

                skip++;
            }
        }
    }
}