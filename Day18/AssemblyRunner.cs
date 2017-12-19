using System;
using System.Collections.Generic;
using System.Linq;

namespace Day18
{
    /// <summary>
    /// Executes a set of provided assembly instructions
    /// </summary>
    public class AssemblyRunner
    {
        public long LastSoundPlayed;

        private readonly IList<string> _instructions;
        private readonly Dictionary<string, long> _registers;

        public AssemblyRunner(IList<string> instructions)
        {
            _instructions = instructions;
            _registers = new Dictionary<string, long>();
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

        private void Add(string register, string value)
        {
            EnsureRegisterExists(register);
            _registers[register] += Translate(value);
        }

        private void Multiply(string register, string value)
        {
            EnsureRegisterExists(register);
            _registers[register] *= Translate(value);
        }

        private void Modulus(string register, string value)
        {
            EnsureRegisterExists(register);
            _registers[register] %= Translate(value);
        }

        private void Set(string register, string value)
        {
            EnsureRegisterExists(register);
            _registers[register] = Translate(value);
        }

        public void ExecuteInstructions()
        {
            for (var i = 0; i < _instructions.Count; i++)
            {
                var split = _instructions[i].Split(" ");

                switch (split[0])
                {
                    case "add":
                        Add(split[1], split[2]);
                        break;
                    case "mul":
                        Multiply(split[1], split[2]);
                        break;
                    case "mod":
                        Modulus(split[1], split[2]);
                        break;
                    case "set":
                        Set(split[1], split[2]);
                        break;
                    case "jgz":
                        if (Translate(split[1]) > 0) { i += (int)Translate(split[2]); i--; }
                        break;
                    case "snd":
                        LastSoundPlayed = Translate(split[1]);
                        break;
                    case "rcv":
                        if (Translate(split[1]) != 0) { return; }
                        break;
                    default:
                        throw new InvalidOperationException($"Did not recognise instruction: {split[0]}");
                }
            }
        }
    }
}