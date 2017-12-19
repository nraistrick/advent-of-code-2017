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
        public int CurrentLine;
        public int ValuesSent;
        public bool Waiting;

        private readonly IList<string> _instructions;
        private readonly Dictionary<string, long> _registers;
        private readonly Queue<long> _input, _output;

        public AssemblyRunner(IList<string> instructions, Queue<long> input, Queue<long> output, int id)
        {
            _instructions = instructions;
            _registers = new Dictionary<string, long> {{"p", id}};

            (_input, _output) = (input, output);

            CurrentLine = 0;
            ValuesSent = 0;
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
                    if (Translate(split[1]) > 0) { CurrentLine += (int)Translate(split[2]); CurrentLine--; }
                    break;
                case "snd":
                    _output.Enqueue(Translate(split[1]));
                    ValuesSent++;
                    break;
                case "rcv":
                    if (_input.Count == 0) { CurrentLine--; Waiting = true; break; }
                    Waiting = false;
                    _registers[split[1]] = _input.Dequeue();
                    break;
                default:
                    throw new InvalidOperationException($"Did not recognise instruction: {split[0]}");
            }

            CurrentLine++;
        }
    }
}