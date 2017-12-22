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

        private static readonly Dictionary<char[,], string> LinearizeCache = new Dictionary<char[,], string>();
        private static readonly Dictionary<string, char[,]> DelinearizeCache = new Dictionary<string, char[,]>();

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
            var expandedImage = ExpandArt(Input, rules, 18);

            Console.WriteLine($"The number of on pixels is: {CountOnPixels(expandedImage)}");
        }

        /// <summary>
        /// Counts the number of on pixels in a two-dimensional character array
        /// </summary>
        public static int CountOnPixels(char[,] image)
        {
            var count = 0;
            for (var j = 0; j < image.GetLength(0); j++)
            {
                for (var k = 0; k < image.GetLength(1); k++) if (image[j, k] == '#') count++;
            }

            return count;
        }

        /// <summary>
        /// Loads the shorthand translation rules from the given enumerable
        /// </summary>
        public static Dictionary<string, string> LoadRules(IEnumerable<string> inputRules)
        {
            var rules = new Dictionary<string, string>();
            foreach (var rule in inputRules)
            {
                var r = rule.Split(" => ");
                foreach (var _ in Enumerable.Range(0, 2))
                {
                    foreach (var __ in Enumerable.Range(0, 4))
                    {
                        rules.TryAdd(r[0], r[1]);
                        r[0] = Rotate(r[0]);
                    }

                    r[0] = Flip(r[0]);
                }
            }

            return rules;
        }

        /// <summary>
        /// Converts a rule from a two-dimensional array to it's equivalent
        /// short-hand linear form e.g. ..#/.#./###
        /// </summary>
        public static string Linearize(char[,] image)
        {
            if (LinearizeCache.TryGetValue(image, out var cachedString))
                return cachedString;

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
            if (DelinearizeCache.TryGetValue(image, out var cachedArray))
                return cachedArray;

            var dividersRemoved = string.Join("", image.Split(ConciseDelimiter));
            var length = (int)Math.Sqrt(dividersRemoved.Length);

            var delineraized = new char[length, length];
            foreach (var i in Enumerable.Range(0, length))
            {
                foreach (var j in Enumerable.Range(0, length))
                    delineraized[i, j] = dividersRemoved[i*length + j];
            }

            DelinearizeCache[image] = delineraized;

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
        /// Expands the provided image against the given ruleset for a given
        /// number of iterations
        /// </summary>
        public static char[,] ExpandArt(char[,] image, Dictionary<string, string> rules, int iterations)
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
        public static char[,] ExpandArt(char[,] image, Dictionary<string, string> rules)
        {
            int oldLength, newLength;

            if      (image.Length % 2 == 0) (oldLength, newLength) = (2, 3);
            else if (image.Length % 3 == 0) (oldLength, newLength) = (3, 4);
            else throw new ArgumentException("Received an invalid size image");

            var blockCount = (int) Math.Sqrt(image.Length) / oldLength;

            var expandedLength = blockCount * newLength;
            var expandedArray = new char[expandedLength, expandedLength];

            // Run image replacement rules over existing blocks to create new image
            foreach (var i in Enumerable.Range(0, blockCount))
            {
                foreach (var j in Enumerable.Range(0, blockCount))
                {
                    var imageSubsection = Utilities.GetArraySubsection(image, oldLength, i * oldLength, j * oldLength);

                    var linearized = Linearize(imageSubsection);
                    var replacement = rules[linearized];
                    var delinearizedReplacement = Delinearize(replacement);

                    Utilities.InsertArraySubsection(delinearizedReplacement, expandedArray, i * newLength, j * newLength);
                }
            }

            return expandedArray;
        }
    }
}