using System;
using System.Collections.Generic;
using Common;

namespace Day03
{
    /// <summary>
    /// Represents an entry in a spiral memory structure
    /// </summary>
    public struct SpiralMemoryEntry
    {
        public int X { get; }
        public int Y { get; }
        public int Value { get; }

        public SpiralMemoryEntry(int x, int y, int value)
        {
            X = x;
            Y = y;
            Value = value;
        }
    }

    /// <summary>
    /// Represents an experimental spiral memory structure
    /// </summary>
    public class SpiralMemory
    {
        private readonly int[,] _map;
        private readonly int _entries;
        private int _x, _y;
        private readonly int _xOrigin, _yOrigin;
        private readonly Orientation _orientation;

        public SpiralMemory(int entries)
        {
            int width, height;
            width = height = (int)Math.Ceiling(Math.Sqrt(entries));

            _map = new int[width, height];
            _entries = entries;

            _xOrigin = (int)Math.Ceiling((double)width / 2) - 1;
            _yOrigin = (int)Math.Ceiling((double)height / 2) - 1;
            _x = _xOrigin;
            _y = _yOrigin;

            _orientation = new Orientation(Direction.Right);
        }

        /// <summary>
        /// Beginning in the centre of the structure, uterates through the memory
        /// structure and populates it with sequential values
        /// </summary>
        public IEnumerable<SpiralMemoryEntry> GenerateEntries()
        {
            var value = 1;
            while (true)
            {
                _map[_x, _y] = value;
                yield return new SpiralMemoryEntry(_x, _y, value);

                if (value >= _entries) { break; }

                MoveToNextEntry();
                value++;
            }
        }

        /// <summary>
        /// Beginning in the centre of the structure, uterates through the memory
        /// structure and populates it with sequential values
        /// </summary>
        public IEnumerable<SpiralMemoryEntry> GenerateStressTestEntries(int maximum)
        {
            while (true)
            {
                var value = CalculateStressTestValue();
                if (value == 0) { value = 1; }

                _map[_x, _y] = value;
                yield return new SpiralMemoryEntry(_x, _y, value);

                if (value > maximum) { break; }

                MoveToNextEntry();
            }
        }

        /// <summary>
        /// Updates the coordinates to point to the next location in the memory map
        /// </summary>
        private void MoveToNextEntry()
        {
            switch (_orientation.Current)
            {
                case Direction.Up:
                    _y += 1;
                    if (_map[_x - 1, _y] == 0) { _orientation.TurnLeft(); }
                    break;
                case Direction.Right:
                    _x += 1;
                    if (_map[_x, _y + 1] == 0) { _orientation.TurnLeft(); }
                    break;
                case Direction.Down:
                    _y -= 1;
                    if (_map[_x + 1, _y] == 0) { _orientation.TurnLeft(); }
                    break;
                case Direction.Left:
                    _x -= 1;
                    if (_map[_x, _y - 1] == 0) { _orientation.TurnLeft(); }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int CalculateStressTestValue()
        {
            var value = 0;

            for (var i = _x - 1; i <= _x + 1; i++)
            {
                for (var j = _y - 1; j <= _y + 1; j++)
                {
                    if (i == _x && j == _y)
                        continue;

                    if (_map.TryGetElement(i, j, out var elementValue))
                        value += elementValue;
                }
            }

            return value;
        }

        /// <summary>
        /// Gets the manhattan distance of the last generated <see cref="SpiralMemoryEntry"/>
        /// from the first entry in the memory grid
        /// </summary>
        public int DistanceFromOrigin()
        {
            return Math.Abs(_x - _xOrigin) + Math.Abs(_y - _yOrigin);
        }

        // ReSharper disable once UnusedMember.Global
        public void PrintGrid()
        {
            _map.PrintGrid();
        }
    }
}