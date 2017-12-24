using System;
using System.Collections.Generic;
using System.Linq;

namespace Day25
{
    /// <summary>
    /// Simulates running computational instructions as per a Turing machine
    /// </summary>
    public class TuringMachine
    {
        private string _currentState;
        private readonly int _cycles;
        private int _index;
        private readonly int[] _tape;

        private readonly List<string> _instructions;

        public TuringMachine(List<string> instructions)
        {
            _instructions = instructions;
            _currentState = _instructions[0].Split(" ")[3][0].ToString();
            _cycles = int.Parse(_instructions[1].Split(" ")[5]);

            _instructions.RemoveAt(0);
            _instructions.RemoveAt(0);
            _instructions.RemoveAt(0);

            // Create a large enough tape to hold all possible outputs
            _tape = new int[_cycles * 2];
            _index = _cycles;
        }

        public int CalculateChecksum() => _tape.Count(x => x == 1);

        public void ExecuteBlueprint()
        {
            foreach (var _ in Enumerable.Range(0, _cycles)) InsertNextValue();
        }

        private void InsertNextValue()
        {
            var currentValue = _tape[_index];
            var current = 0;

            while (!_instructions[current].Contains($"In state {_currentState}")) current++;
            current++;

            while (!_instructions[current].Contains($"If the current value is {currentValue}")) current += 4;
            current++;

            var line = _instructions[current];
            _tape[_index] = int.Parse(line[line.Length - 2].ToString());
            current++;

            if      (_instructions[current].Contains("left"))  _index--;
            else if (_instructions[current].Contains("right")) _index++;
            else throw new InvalidOperationException("Did not recognise direction");
            current++;

            line = _instructions[current];
            _currentState = line[line.Length - 2].ToString();
        }
    }
}