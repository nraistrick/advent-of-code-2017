using System;
using System.Collections.Generic;
using System.Linq;

namespace Day01
{
    /// <summary>
    /// Creates captcha sequences from some input text
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var input = System.IO.File.ReadAllText("Input.txt");

            Console.WriteLine("The solution to the captcha is: " +
                              CreateCaptcha(input));

            Console.WriteLine("The solution to the alternative captcha is: " +
                              CreateAlternativeCaptcha(input));
        }

        public static int CreateCaptcha(string input)
        {
            var pairs = GetNeighbouringDigitPairs(input);
            return CreateCaptcha(pairs);
        }

        public static int CreateAlternativeCaptcha(string input)
        {
            var pairs = GetHalfwayDigitPairs(input);
            return CreateCaptcha(pairs);
        }

        private static int CreateCaptcha(IEnumerable<(char, char)> digitPairs)
        {
            if (digitPairs == null) throw new ArgumentNullException(nameof(digitPairs));

            var duplicates = GetDuplicateDigits(digitPairs);
            var sum = SumDigits(duplicates);

            return sum;
        }

        /// <summary>
        /// Splits the input into a collection of neighbouring digit pairs
        /// </summary>
        public static List<(char, char)> GetNeighbouringDigitPairs(string digits)
        {
            var pairs = new List<(char, char)>();
            for (var i = 0; i < digits.Length; i += 1)
            {
                pairs.Add((digits[i], digits[(i + 1) % digits.Length]));
            }

            return pairs;
        }

        /// <summary>
        /// Splits the input into a collection of pairs with each digit being paired
        /// with another that is half the string length away
        /// </summary>
        public static List<(char, char)> GetHalfwayDigitPairs(string digits)
        {
            var pairs = new List<(char, char)>();
            for (var i = 0; i < digits.Length; i += 1)
            {
                pairs.Add((digits[i], digits[(i + digits.Length/2) % digits.Length]));
            }

            return pairs;
        }

        /// <summary>
        /// Returns all digits that match their counterpart
        /// </summary>
        public static List<char> GetDuplicateDigits(IEnumerable<(char, char)> digitPairs)
        {
            return digitPairs.Where(pair => pair.Item1 == pair.Item2)
                             .Select(pair => pair.Item1)
                             .ToList();
        }

        public static int SumDigits(IEnumerable<char> digits)
        {
            return digits.Sum(digit => (int) char.GetNumericValue(digit));
        }
    }
}
