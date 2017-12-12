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
            var inputData = File.ReadAllText("Input.txt");
            var instructions = GetInputArray(inputData);
            var steps = NavigateInstructions(instructions);

            Console.WriteLine($"The number of steps to navigate the instructions is {steps}");

            instructions = GetInputArray(inputData);
            steps = NavigateInstructions(instructions, 3);

            Console.WriteLine($"The number of complex steps to navigate the instructions is {steps}");
        }

        public static int[] GetInputArray(string inputData)
        {
            return inputData.Split().Select(int.Parse).ToArray();
        }

        public static int NavigateInstructions(int[] instructions, int? maxOffset=null)
        {
            var steps = 0;
            var index = 0;

            while (index < instructions.Length)
            {
                var lastIndex = index;
                index += instructions[index];
                if (maxOffset != null && instructions[lastIndex] >= maxOffset)
                {
                    instructions[lastIndex]--;
                }
                else
                {
                    instructions[lastIndex]++;
                }

                steps++;
            }

            return steps;
        }
    }
}