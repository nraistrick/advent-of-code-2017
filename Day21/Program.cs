using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace Day21
{
    /// <summary>
    /// A program to generate art and enhance the detail of an image by using
    /// a provided set of array substitution rules
    /// </summary>
    public static class Program
    {
        private const string ConciseDelimiter = "/";

        public static readonly char[,] Input =
        {
            {'.', '#', '.'},
            {'.', '.', '#'},
            {'#', '#', '#'}
        };

        private static void Main()
        {
            var inputData = File.ReadAllLines("Input.txt");
            var rules = LoadRules(inputData);
            var inputImage = Linearize(Input);
            var expandedImage = ExpandArt(inputImage, rules, 5);

            Console.WriteLine($"The number of on pixels is: {expandedImage.Count(c => c == '#')}");
        }

        /// <summary>
        /// Loads the shorthand translation rules from the given enumerable
        /// </summary>
        public static Dictionary<string, string> LoadRules(IEnumerable<string> inputRules)
        {
            return inputRules.Select(rule => rule.Split(" => ")).ToDictionary(s => s[0], s => s[1]);
        }

        /// <summary>
        /// Converts a rule from a two-dimensional array to it's equivalent
        /// short-hand linear form e.g. ..#/.#./###
        /// </summary>
        public static string Linearize(char[,] image)
        {
            var linearized = "";

            foreach (var i in Enumerable.Range(0, image.GetLength(0)))
            {
                foreach (var j in Enumerable.Range(0, image.GetLength(1))) linearized += image[i, j];

                if (i != image.GetLength(0) - 1) linearized += ConciseDelimiter;
            }

            return linearized;
        }

        /// <summary>
        /// Converts a rule from it's short-hand form to a two-dimensional array
        /// </summary>
        public static char[,] Delinearize(string image)
        {
            var dividersRemoved = string.Join("", image.Split(ConciseDelimiter));
            var length = (int)Math.Sqrt(dividersRemoved.Length);

            var delineraized = new char[length, length];
            foreach (var i in Enumerable.Range(0, length))
            {
                foreach (var j in Enumerable.Range(0, length))
                    delineraized[i, j] = dividersRemoved[i*length + j];
            }

            return delineraized;
        }

        /// <summary>
        /// Flips an image vertically
        /// </summary>
        public static string Flip(string image)
        {
            var flipped = "";

            var s = image.Split(ConciseDelimiter);
            for (var i = s.Length - 1; i >= 0; i--)
            {
                flipped += s[i];
                if (i != 0) flipped += ConciseDelimiter;
            }

            return flipped;
        }

        /// <summary>
        /// Rotates an image clockwise 90 degrees
        /// </summary>
        public static string Rotate(string image)
        {
            var rotated = "";

            var s = image.Split(ConciseDelimiter);

            for (var i = 0; i < s.Length; i++)
            {
                for (var j = s.Length - 1; j >= 0; j--) rotated += s[j][i];
                if (i != s.Length - 1) rotated += ConciseDelimiter;
            }

            return rotated;
        }

        /// <summary>
        /// Finds a matching pattern by flipping and rotating the provided pattern
        /// </summary>
        public static string FindMatchingRule(string linearImage, Dictionary<string, string> rules)
        {
            var current = linearImage;
            foreach (var _ in Enumerable.Range(0, 2))
            {
                foreach (var __ in Enumerable.Range(0, 4))
                {
                    var successful = rules.TryGetValue(current, out var match);
                    if (successful) return match;

                    current = Rotate(current);
                }

                current = Flip(current);
            }

            throw new InvalidOperationException($"No matching rule found for {linearImage}");
        }

        /// <summary>
        /// Expands the provided image against the given ruleset for a given
        /// number of iterations
        /// </summary>
        public static string ExpandArt(string image, Dictionary<string, string> rules, int iterations)
        {
            foreach (var _ in Enumerable.Range(0, iterations))
            {
                image = ExpandArt(image, rules);
            }

            return image;
        }

        /// <summary>
        /// Expands the provided image against the given ruleset
        /// </summary>
        public static string ExpandArt(string image, Dictionary<string, string> rules)
        {
            var pixels = string.Join("", image.Split(ConciseDelimiter));
            int oldLength, newLength;

            if      (pixels.Length % 2 == 0) (oldLength, newLength) = (2, 3);
            else if (pixels.Length % 3 == 0) (oldLength, newLength) = (3, 4);
            else throw new ArgumentException("Received an invalid size image");

            var blockCount = (int) Math.Sqrt(pixels.Length) / oldLength;

            var expandedLength = blockCount * newLength;
            var expandedArray = new char[expandedLength, expandedLength];

            var delinearized = Delinearize(image);

            // Run image replacement rules over existing blocks to create new image
            foreach (var i in Enumerable.Range(0, blockCount))
            {
                foreach (var j in Enumerable.Range(0, blockCount))
                {
                    var subsection = Utilities.GetArraySubsection(delinearized, oldLength, i * oldLength, j * oldLength);

                    var linearized = Linearize(subsection);
                    var replacement = FindMatchingRule(linearized, rules);
                    var delinearizedReplacement = Delinearize(replacement);

                    Utilities.InsertArraySubsection(delinearizedReplacement, expandedArray, i * newLength, j * newLength);
                }
            }

            return Linearize(expandedArray);
        }
    }
}