using System.Collections.Generic;
using System.Linq;

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
    }
}