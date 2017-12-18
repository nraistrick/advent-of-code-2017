using System;
using System.IO;

namespace Day09
{
    /// <summary>
    /// Calculates statistics for a given stream of characters
    /// </summary>
    ///
    /// ReSharper disable ConvertIfStatementToSwitchStatement
    public static class Program
    {
        private const char Ignore       = '!';
        private const char GroupStart   = '{';
        private const char GroupEnd     = '}';
        private const char RubbishStart = '<';
        private const char RubbishEnd   = '>';

        private static void Main()
        {
            var inputData = File.ReadAllText("Input.txt");
            var score = CountGroupScore(inputData);
            Console.WriteLine($"The final group score is: {score}");

            var rubbishCharacters = CountRubbishCharacters(inputData);
            Console.WriteLine($"The number of removable garbage characters is: {rubbishCharacters}");
        }

        /// <summary>
        /// Calculates the total score for a collection of groups in a character stream
        /// </summary>
        public static int CountGroupScore(string inputData)
        {
            var score = 0;

            var currentGroupLevel = 0;
            var isRubbish = false;

            var current = 0;
            while (current < inputData.Length)
            {
                var character = inputData[current];

                if      (character == Ignore)       { current++; }
                else if (character == RubbishEnd)   { isRubbish = false; }
                else if (isRubbish)                 { }
                else if (character == RubbishStart) { isRubbish = true; }
                else if (character == GroupStart)   { score += ++currentGroupLevel; }
                else if (character == GroupEnd)     { currentGroupLevel--; }

                current++;
            }

            return score;
        }

        /// <summary>
        /// Calculates the number of rubbish characters which can be removed
        /// from the character stream
        /// </summary>
        public static int CountRubbishCharacters(string inputData)
        {
            var garbageCharacters = 0;
            var isRubbish = false;

            var current = 0;
            while (current < inputData.Length)
            {
                var character = inputData[current];

                if      (character == Ignore)       { current++; }
                else if (character == RubbishEnd)   { isRubbish = false; }
                else if (isRubbish)                 { garbageCharacters += 1; }
                else if (character == RubbishStart) { isRubbish = true; }

                current++;
            }

            return garbageCharacters;
        }
    }
}