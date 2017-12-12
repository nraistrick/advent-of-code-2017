using System;
using System.IO;
using System.Linq;

namespace Day05
{
    /// <summary>
    /// A program to navigate through a sequence of jump instructions in a list
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var instructions = GetInputArray(File.ReadAllText("Input.txt"));
            var steps = NavigateInstructions(instructions);

            Console.WriteLine($"The number of steps to navigate the instructions is {steps}");
        }

        public static int[] GetInputArray(string inputData)
        {
            return inputData.Split().Select(int.Parse).ToArray();
        }

        public static int NavigateInstructions(int[] instructions)
        {
            var steps = 0;
            var index = 0;

            while (index < instructions.Length)
            {
                var lastIndex = index;
                index += instructions[index];
                instructions[lastIndex]++;

                steps++;
            }

            return steps;
        }
    }
}