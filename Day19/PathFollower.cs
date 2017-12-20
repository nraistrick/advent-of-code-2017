using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Day19
{
    /// <summary>
    /// Collects information the letters passed while following the one
    /// allowable route on a _map
    ///
    ///  +--->
    ///  |   x
    ///  v y
    ///
    /// </summary>
    public class PathFollower
    {
        private const char Corner     = '+';
        private const char Horizontal = '-';
        private const char Vertical   = '|';
        private const char EndOfMap   = ' ';

        private readonly string[] _map;

        public PathFollower(string[] map)
        {
            _map = NormaliseMapWidth(map);
        }

        /// <summary>
        /// Pads all rows in a _map with spaces so they have equivalent width
        /// </summary>
        private static string[] NormaliseMapWidth(string[] map)
        {
            var maxWidth = map.Select(line => line.Length).Max();

            for (var y = 0; y < map.Length; y++)
            {
                map[y] = map[y].PadRight(maxWidth);
            }

            return map;
        }

        /// <summary>
        /// Retrieves the letters encountered while following the provided _map
        /// </summary>
        public (List<string> letters, int steps) FindLettersOnPath()
        {
            var letters = new List<string>();
            var steps = 0;

            var direction = Direction.Down;
            var (x, y) = (_map[0].IndexOf(Vertical), 0);

            while (_map[y][x] != EndOfMap)
            {
                steps++;

                if (char.IsLetter(_map[y][x]))
                {
                    letters.Add(char.ToString(_map[y][x]));
                }

                else if (_map[y][x] == Corner)
                {
                    if      (direction != Direction.Right && PathOnLeft (x, y)) direction = Direction.Left;
                    else if (direction != Direction.Left  && PathOnRight(x, y)) direction = Direction.Right;
                    else if (direction != Direction.Down  && PathAbove  (x, y)) direction = Direction.Up;
                    else if (direction != Direction.Up    && PathBelow  (x, y)) direction = Direction.Down;
                }

                (x, y) = NextCoordinates(x, y, direction);
            }

            return (letters, steps);
        }

        private bool PathOnLeft (int x, int y) => x > 0                  && (_map[y][x - 1] == Horizontal || char.IsLetter(_map[y][x - 1]));
        private bool PathOnRight(int x, int y) => x < _map[0].Length - 1 && (_map[y][x + 1] == Horizontal || char.IsLetter(_map[y][x + 1]));
        private bool PathAbove  (int x, int y) => y < _map.Length - 1    && (_map[y - 1][x] == Vertical   || char.IsLetter(_map[y - 1][x]));
        private bool PathBelow  (int x, int y) => y > 0                  && (_map[y + 1][x] == Vertical   || char.IsLetter(_map[y + 1][x]));

        private static (int x, int y) NextCoordinates(int x, int y, Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:  y++; break;
                case Direction.Up:    y--; break;
                case Direction.Right: x++; break;
                case Direction.Left:  x--; break;
                default: throw new ArgumentException($"Did not recognise direction: {direction}");
            }

            return (x, y);
        }
    }
}