using System;
using System.Collections.Generic;
using System.Linq;

namespace Day02
{
    /// <summary>
    /// Calculates the checksum for a file
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var input = System.IO.File.ReadAllText("Input.txt");
            Console.WriteLine($"The calculated checksum is {CalculateChecksum(input)}");
        }

        /// <summary>
        /// Finds the final checksum for the provided data by summing the
        /// individual checksums for each line
        /// </summary>
        public static int CalculateChecksum(string inputData)
        {
            if (inputData == null) throw new ArgumentNullException(nameof(inputData));
            return GetFileValues(inputData).Sum(CalculateMaxDifference);
        }

        /// <summary>
        /// Finds the checksum for a collection of values by subtracting
        /// the minimum from the maximum
        /// </summary>
        public static int CalculateMaxDifference(List<int> values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            return values.Max() - values.Min();
        }

        /// <summary>
        /// Extracts a collection of integers for each file line
        /// </summary>
        public static IEnumerable<List<int>> GetFileValues(string fileData)
        {
            if (fileData == null) throw new ArgumentNullException(nameof(fileData));
            return fileData.Split(Environment.NewLine).Select(GetValues).ToList();
        }

        /// <summary>
        /// Extracts a list of available integers from the provided data
        /// </summary>
        public static List<int> GetValues(string fileLine)
        {
            if (fileLine == null) throw new ArgumentNullException(nameof(fileLine));
            return fileLine.Split()
                           .Where(value => !string.IsNullOrWhiteSpace(value))
                           .Select(value => Convert.ToInt32(value))
                           .ToList();
        }
    }
}