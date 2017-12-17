using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day08
{
    /// <summary>
    /// Runs a series of conditional operations to calculate the final values
    /// of a set of instruction registers
    /// </summary>
    public class InstructionRunner
    {
        private const string Pattern = @"([a-z]+) (inc|dec) ((?:-)?\d+) if ([a-z]+) (==|<=|>=|<|>|!=) ((?:-)?\d+)";

        public readonly Dictionary<string, int> Registers;
        public int MaxValueHeld;
        private readonly string[] _instructions;

        public InstructionRunner(string[] instructions)
        {
            _instructions = instructions;
            Registers = new Dictionary<string, int>();
            MaxValueHeld = 0;
        }

        public void RunInstructions()
        {
            foreach (var instruction in _instructions)
            {
                var match = Regex.Match(instruction, Pattern);
                var (firstRegister, secondRegister) = (match.Groups[1].Value, match.Groups[4].Value);
                var (command, commandArgument)      = (match.Groups[2].Value, int.Parse(match.Groups[3].Value));
                var (condition, conditionArgument)  = (match.Groups[5].Value, int.Parse(match.Groups[6].Value));

                Registers.TryAdd(firstRegister, 0);
                Registers.TryAdd(secondRegister, 0);

                if (!condition.Operator(Registers[secondRegister], conditionArgument)) { continue; }

                switch (command)
                {
                    case "inc":
                        Registers[firstRegister] += commandArgument;
                        break;
                    case "dec":
                        Registers[firstRegister] -= commandArgument;
                        break;
                    default:
                        throw new InvalidOperationException($"Command not implemented {command}");
                }

                if (Registers[firstRegister] > MaxValueHeld)
                {
                    MaxValueHeld = Registers[firstRegister];
                }
            }
        }
    }
}