using System;
using System.Collections.Generic;
using System.IO;

namespace Day18
{
    /// <summary>
    /// Runs two assembly runners in parallel; they will pass messages between
    /// each other to coordinate their behaviour
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var instructions = File.ReadAllLines("Input.txt");
            var (programOneOutput, programTwoOutput) = (new Queue<long>(), new Queue<long>());

            var runnerOne = new AssemblyRunner(instructions, programTwoOutput, programOneOutput, 0);
            var runnerTwo = new AssemblyRunner(instructions, programOneOutput, programTwoOutput, 1);

            RunTwoRunnersInParallel(runnerOne, runnerTwo);
            Console.WriteLine($"The number of values the first program sent was: {runnerOne.ValuesSent}");
        }

        public static void RunTwoRunnersInParallel(AssemblyRunner runnerOne, AssemblyRunner runnerTwo)
        {
            while (true)
            {
                runnerOne.ExecuteNext();
                runnerTwo.ExecuteNext();

                if (runnerOne.CurrentLine == -1 && runnerTwo.CurrentLine == -1) { break; }
                if (runnerOne.Waiting && runnerTwo.Waiting)                     { break; }
            }
        }
    }
}