using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            var knotHash = CalculateKnotHash(inputData);
            Console.WriteLine($"The calculated knot-hash is {knotHash}");
        }

        public static string CalculateKnotHash(string inputLengths)
        {
            var customLengths = GetAsciiLengths(inputLengths);
            var sequence = Enumerable.Range(0, 256).ToList();

            var iterations = ScrambleNumbers(sequence, customLengths, 64).Last();
            var denseHash = CalculateDenseHash(iterations);
            var hashString = GetHexadecimalString(denseHash);

            return hashString;
        }

        public static IEnumerable<List<int>> ScrambleNumbers(List<int> sequence,
                                                             IEnumerable<int> lengths,
                                                             int cycles = 1)
        {
            int rotations = 0, skip = 0;

            for (var c = 0; c < cycles; c++)
            {
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

        public static List<int> GetAsciiLengths(string inputData)
        {
            var additionalBytes = new List<int> {17, 31, 73, 47, 23};

            var inputBytes = Encoding.ASCII.GetBytes(inputData).Select(x => (int)x).ToList();
            inputBytes.AddRange(additionalBytes);

            return inputBytes;
        }

        public static List<int> CalculateDenseHash(List<int> inputBytes)
        {
            var denseHash = new List<int>();
            for (var i = 0; i < inputBytes.Count; i += 16)
            {
                denseHash.Add(CalculateDenseHashValue(inputBytes.GetRange(i, 16)));
            }

            return denseHash;
        }

        public static int CalculateDenseHashValue(List<int> inputBytes)
        {
            if (inputBytes.Count % 16 != 0)
            {
                throw new ArgumentException("Expect there to be a number of input bytes" +
                                            "that are divisible by 16");
            }

            var result = inputBytes.First();
            result = inputBytes.Skip(1).Aggregate(result, (current, b) => current ^ b);

            return result;
        }

        public static string GetHexadecimalString(IEnumerable<int> denseHashValues)
        {
            return denseHashValues.Aggregate("", (current, value) => current + value.ToString("X2").ToLower());
        }
    }
}