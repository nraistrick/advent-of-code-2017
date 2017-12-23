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
        public int MultiplyInstrutionsExecuted;

        private readonly IList<string> _instructions;
        private readonly Dictionary<string, long> _registers;

        public AssemblyRunner(IList<string> instructions)
        {
            _instructions = instructions;
            _registers = new Dictionary<string, long>
            {
                {"a", 0}, {"b", 0}, {"c", 0},
                {"d", 0}, {"e", 0}, {"f", 0},
                {"g", 0}, {"h", 0}
            };

            CurrentLine = 0;
            MultiplyInstrutionsExecuted = 0;
        }

        /// <summary>
        /// Translates an input argument into the number it represents.
        /// This could be a register identifier (e.g. "a", "b", "c") or a
        /// number (e.g. "1", "3", "-2").
        /// </summary>
        private long Translate(string value)
        {
            return long.TryParse(value, out var result) ? result : _registers[value];
        }

        private void EnsureRegisterExists(string argument)
        {
            if (!int.TryParse(argument, out var unused))
                _registers.TryAdd(argument, 0);
        }

        private void Subtract(string register, string value)
        {
            EnsureRegisterExists(register);
            _registers[register] -= Translate(value);
        }

        private void Multiply(string register, string value)
        {
            EnsureRegisterExists(register);
            _registers[register] *= Translate(value);
            MultiplyInstrutionsExecuted++;
        }

        private void Set(string register, string value)
        {
            EnsureRegisterExists(register);
            _registers[register] = Translate(value);
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