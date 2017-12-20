using System;
using System.IO;

namespace Day19
{
    public static class Program
    {
        private static void Main()
        {
            var map = File.ReadAllLines("Input.txt");
            var mapFollower = new PathFollower(map);
            var letters = mapFollower.FindLettersOnPath();
            Console.WriteLine($"The letters on the path are {string.Join("", letters)}");
        }
    }
}