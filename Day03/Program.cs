using System;
using System.Linq;

namespace Day03
{
    public static class Program
    {
        private static void Main()
        {
            const int puzzleInput = 347991;

            var memoryMapper = new SpiralMemory(puzzleInput);
            var entry = memoryMapper.GenerateEntries().Last();
            Console.WriteLine($"End coordinates are: {entry.X}, {entry.Y}");
            Console.WriteLine($"Distance from origin is: {memoryMapper.DistanceFromOrigin()}");

            memoryMapper = new SpiralMemory(1000);
            var stressEntry = memoryMapper.GenerateStressTestEntries(puzzleInput).Last();
            Console.WriteLine("First value written that is larger than the input is: " +
                              stressEntry.Value);
        }
    }
}