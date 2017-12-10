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
        /// structure and populates it with values
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