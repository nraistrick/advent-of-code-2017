using System;

namespace Common
{
    public static class Print
    {
        public interface IOutputWriter
        {
            void Write(string s);
        }

        private class ConsoleOutputWriter : IOutputWriter
        {
            public void Write(string s)
            {
                Console.Write(s);
            }
        }

        /// <summary>
        /// Prints a two-dimensional array of [x, y] to the console.
        /// 'x' is represented as the horizontal axis and 'y' is represented
        /// as the vertical axis.
        /// </summary>
        public static void PrintGrid<T>(this T[,] array, int entryWidth=2)
        {
            PrintGrid(array, entryWidth, new ConsoleOutputWriter());
        }

        /// <summary>
        /// Prints a two-dimensional array of [x, y] to the provided output writer.
        /// 'x' is represented as the horizontal axis and 'y' is represented
        /// as the vertical axis.
        /// </summary>
        public static void PrintGrid<T>(T[,] array, int entryWidth, IOutputWriter output)
        {
            var rowSize = array.GetLength(0);
            var columnSize = array.GetLength(1);

            for (var j = columnSize - 1; j >= 0; j--)
            {
                for (var i = 0; i < rowSize; i++)
                {
                    output.Write(string.Format($"{{0, {entryWidth}}} ", array[i, j]));
                }

                output.Write(Environment.NewLine + Environment.NewLine);
            }
        }
    }
}