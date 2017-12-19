using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace Day16
{
    /// <summary>
    /// Calculate the final order for a group of programs performing a
    /// sequence of dance moves
    /// </summary>
    public static class Program
    {
        public static List<char> Programs;

        private static void Main()
        {
            var instructions = File.ReadAllText("Input.txt").Split(",");
            Programs = "abcdefghijklmnop".ToCharArray().ToList();
            ExecuteInstructions(instructions);

            Console.WriteLine($"The programs are now standing in order: {string.Join("", Programs)}");
        }

        public static void Spin(int number)
        {
            Programs = Programs.RotateRight(number);
        }

        public static void Exchange(int first, int second)
        {
            (Programs[first], Programs[second]) = (Programs[second], Programs[first]);
        }

        public static void Partner(char firstProgramName, char secondProgramName)
        {
            Exchange(Programs.IndexOf(firstProgramName), Programs.IndexOf(secondProgramName));
        }

        public static void ExecuteInstructions(IEnumerable<string> instructions)
        {
            foreach (var i in instructions) { ExecuteInstruction(i); }
        }

        public static void ExecuteInstruction(string instruction)
        {
            var (command, arguments) = (instruction[0], instruction.Substring(1));

            switch (command)
            {
                case 's':
                    Spin(int.Parse(arguments));
                    break;
                case 'x':
                    var split = arguments.Split("/");
                    Exchange(int.Parse(split[0]), int.Parse(split[1]));
                    break;
                case 'p':
                    Partner(char.ToLower(arguments[0]), char.ToLower(arguments[2]));
                    break;
                default:
                    throw new InvalidOperationException($"Could not find instruction {command}");
            }
        }
    }
}