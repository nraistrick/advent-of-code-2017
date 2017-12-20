using System;
using System.IO;

namespace Day19
{
    public static class Program
    {
        private static void Main()
        {
            var map = File.ReadAllLines("Input.txt");

            var pathFollower = new PathFollower(map);
            var (letters, steps) = pathFollower.FindLettersOnPath();

            Console.WriteLine($"The letters on the path are {string.Join("", letters)}");
            Console.WriteLine($"The number of steps taken is {steps}");
        }
    }
}