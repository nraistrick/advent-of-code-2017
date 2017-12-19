using System;
using System.IO;

namespace Day18
{
    public static class Program
    {
        private static void Main()
        {
            var instructions = File.ReadAllLines("Input.txt");
            var runner = new AssemblyRunner(instructions);

            runner.ExecuteInstructions();
            Console.WriteLine($"The frequency of the last sound played is: {runner.LastSoundPlayed}");
        }
    }
}