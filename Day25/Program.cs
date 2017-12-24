using System;
using System.IO;
using System.Linq;

namespace Day25
{
    public static class Program
    {
        private static void Main()
        {
            var instructions = File.ReadAllLines("Input.txt");
            var turingMachine = new TuringMachine(instructions.ToList());
            turingMachine.ExecuteBlueprint();

            Console.WriteLine($"The diagnostic checksum is: {turingMachine.CalculateChecksum()}");
        }
    }
}