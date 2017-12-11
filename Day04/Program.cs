using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day04
{
    /// <summary>
    /// Used to determine the number of valid passphrases in a list
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var input = File.ReadAllText("Input.txt").Split(Environment.NewLine);
            var validPassphrases = CountValidPassphrases(input);

            Console.WriteLine($"The number of valid passphrases is {validPassphrases}");
        }

        private static int CountValidPassphrases(IEnumerable<string> passphrases)
        {
            return passphrases.Count(p => !ContainsDuplicateWords(p));
        }

        public static bool ContainsDuplicateWords(string passphrase)
        {
            var words = passphrase.Split();
            return words.Length != words.Distinct().Count();
        }
    }
}