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
            var validPassphrases = CountValidPassphrases(input, ContainsDuplicateWords);

            Console.WriteLine($"The number of valid passphrases is {validPassphrases}");

            validPassphrases = CountValidPassphrases(input, ContainsDuplicateAnagrams);
            Console.WriteLine($"The number of valid passphrases including anagrams is {validPassphrases}");
        }

        private static int CountValidPassphrases(IEnumerable<string> passphrases,
                                                 Func<string, bool> checkValid)
        {
            return passphrases.Count(p => !checkValid(p));
        }

        public static bool ContainsDuplicateWords(string passphrase)
        {
            var words = passphrase.Split();
            return words.Length != words.Distinct().Count();
        }

        public static bool ContainsDuplicateAnagrams(string passphrase)
        {
            var words = passphrase.Split();
            var sortedWords = words.Select(word => string.Concat(word.OrderBy(c => c)));

            return words.Length != sortedWords.Distinct().Count();
        }
    }
}