using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day07
{
    /// <summary>
    /// Finds the root program from a program tree structure
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var programNames = GetProgramNames(File.ReadAllLines("Input.txt").ToList());
            var rootProgram = GetRootProgram(programNames);

            Console.WriteLine($"The root program is {rootProgram}");
        }

        /// <summary>
        /// Determines the root program by figuring out which program
        /// is only referenced once in the provided program names. Due to the
        /// nature of the input, all other programs will be referenced at
        /// one or more times.
        /// </summary>
        public static string GetRootProgram(IList<string> programNames)
        {
            var rootProgram = from   name in programNames
                              where  programNames.Count(n => n == name) == 1
                              select name;

            return rootProgram.Single();
        }

        /// <summary>
        /// Parse the input file to extract all provided program names
        /// </summary>
        public static List<string> GetProgramNames(IEnumerable<string> fileData)
        {
            var programNames = new List<string>();
            foreach (var line in fileData)
            {
                var entries = line.Replace(",", string.Empty).Split().ToList();
                entries = entries.Where(e => Regex.IsMatch(e, @"^[a-zA-Z]+$")).ToList();
                programNames.AddRange(entries);
            }

            return programNames;
        }
    }
}