using System;
using System.Linq;

namespace Day03
{
    public static class Program
    {
        private static void Main()
        {
            var memoryMapper = new SpiralMemory(347991);
            var entry = memoryMapper.GenerateEntries().Last();
            Console.WriteLine($"End coordinates are: {entry.X}, {entry.Y}");
            Console.WriteLine($"Distance from origin is: {memoryMapper.DistanceFromOrigin()}");
        }
    }
}