using System;
using System.Collections.Generic;

namespace Day23
{
    /// <summary>
    /// Executes a set of provided assembly instructions
    /// </summary>
    public class AssemblyRunner
    {
        public int CurrentLine;

        private readonly IList<string> _instructions;
        public readonly Dictionary<string, long> Registers;

        public AssemblyRunner(IList<string> instructions)
        {
            _instructions = instructions;
            Registers = new Dictionary<string, long>
            {
                {"a", 1}, {"b", 0}, {"c", 0},
                {"d", 0}, {"e", 0}, {"f", 0},
                {"g", 0}, {"h", 0}
            };

            CurrentLine = 0;
        }

        /// <summary>
        /// Translates an input argument into the number it represents.
        /// This could be a register identifier (e.g. "a", "b", "c") or a
        /// number (e.g. "1", "3", "-2").
        /// </summary>
        private long Translate(string value)
        {
            return long.TryParse(value, out var result) ? result : Registers[value];
        }

        private void EnsureRegisterExists(string argument)
        {
            if (!int.TryParse(argument, out var unused))
                Registers.TryAdd(argument, 0);
        }

        private void Subtract(string register, string value)
        {
            EnsureRegisterExists(register);
            Registers[register] -= Translate(value);
        }

        private void Multiply(string register, string value)
        {
            EnsureRegisterExists(register);
            Registers[register] *= Translate(value);
        }

        private void Set(string register, string value)
        {
            EnsureRegisterExists(register);
            Registers[register] = Translate(value);
        }

        public void ExecuteNext()
        {
            if (CurrentLine >= _instructions.Count)
            {
                CurrentLine = -1;
                return;
            }

            var split = _instructions[CurrentLine].Split(" ");

            switch (split[0])
            {
                case "sub":
                    Subtract(split[1], split[2]);
                    break;
                case "mul":
                    Multiply(split[1], split[2]);
                    break;
                case "set":
                    Set(split[1], split[2]);
                    break;
                case "jnz":
                    if (Translate(split[1]) != 0) { CurrentLine += (int)Translate(split[2]); CurrentLine--; }
                    break;
                default:
                    throw new InvalidOperationException($"Did not recognise instruction: {split[0]}");
            }

            CurrentLine++;
        }
    }
}