using System;
using System.IO;

namespace Day23
{
    public static class Program
    {
        private static void Main()
        {
            var instructions = File.ReadAllLines("Input.txt");
            var runner = new AssemblyRunner(instructions);
            while (runner.CurrentLine != -1)
            {
                runner.ExecuteNext();
            }

            Console.WriteLine($"The number of executed multiply instructions is: {runner.MultiplyInstrutionsExecuted}");
        }
    }
}