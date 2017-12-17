using System;
using System.IO;
using System.Linq;

namespace Day08
{
    public static class Program
    {
        private static void Main()
        {
            var instructions = File.ReadAllText("Input.txt").Split(Environment.NewLine);
            var instructionRunner = new InstructionRunner(instructions);
            instructionRunner.RunInstructions();

            Console.WriteLine("The final register values are:");
            var lines = instructionRunner.Registers.Select(kvp => kvp.Key + ": " + kvp.Value.ToString());
            Console.WriteLine(string.Join(Environment.NewLine, lines));

            Console.WriteLine($"The maximum value held was: {instructionRunner.MaxValueHeld}");
        }
    }
}