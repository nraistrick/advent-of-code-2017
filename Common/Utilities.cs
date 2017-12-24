using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class Utilities
    {
        /// <summary>
        /// Safely try get an element from a two-dimensional array
        /// </summary>
        public static bool TryGetElement<T>(this T[,] array, int x, int y, out T element)
        {
            if (x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1))
            {
                element = array[x, y];
                return true;
            }

            element = default(T);
            return false;
        }

        public static List<T> RotateLeft<T>(this IEnumerable<T> items, int rotations = 1)
        {
            var linkedList = new LinkedList<T>(items);
            for (var i = 0; i < rotations; i++)
            {
                var leftmostItem = linkedList.First();
                linkedList.RemoveFirst();
                linkedList.AddLast(leftmostItem);
            }

            return linkedList.ToList();
        }

        public static List<T> RotateRight<T>(this IEnumerable<T> items, int rotations = 1)
        {
            var linkedList = new LinkedList<T>(items);
            for (var i = 0; i < rotations; i++)
            {
                var rightmostItem = linkedList.Last();
                linkedList.RemoveLast();
                linkedList.AddFirst(rightmostItem);
            }

            return linkedList.ToList();
        }

        /// <summary>
        /// Inserts a specified character at periodic intervals throughout a string
        /// </summary>
        public static string InsertCharacterAtIntervals(string input, char character, int interval)
        {
            var builder = new StringBuilder(input);
            for (var i = interval; i < input.Length; i += interval)
            {
                builder.Insert(i, character);
                i++;
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets a square section from an array
        /// </summary>
        public static T[,] GetArraySubsection<T>(T[,] array, int size, int x, int y)
        {
            var section = new T[size, size];
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    section[i, j] = array[y + i, x + j];
                }
            }

            return section;
        }

        /// <summary>
        /// Inserts a small section into a larger array
        /// </summary>
        public static T[,] InsertArraySubsection<T>(T[,] section, T[,] largerArray, int x, int y)
        {
            for (var i = 0; i < section.GetLength(0); i++)
            {
                for (var j = 0; j < section.GetLength(1); j++)
                {
                    largerArray[y + i, x + j] = section[i, j];
                }
            }

            return largerArray;
        }

        /// <summary>
        /// Creates a deep copy of a list
        /// </summary>
        public static IList<T> Clone<T>(this IList<T> listToClone) where T: ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}