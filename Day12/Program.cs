using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day12
{
    /// <summary>
    /// Finds information about groups of programs that are connected via pipes
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var inputData = File.ReadAllLines("Input.txt");
            var connectedPrograms = GetConnectedPrograms(inputData);
            var programsInGroup = ProgramsInGroup(0, connectedPrograms);

            Console.WriteLine($"The number of programs in group 0 is: {programsInGroup.Count}");

            var groups = CountGroups(connectedPrograms);
            Console.WriteLine($"The number of groups is: {groups}");
        }

        public static Dictionary<int, List<int>> GetConnectedPrograms(IEnumerable<string> programPipes)
        {
            var connectedPrograms = new Dictionary<int, List<int>>();

            foreach (var program in programPipes)
            {
                var matches = Regex.Matches(program, @"\d+");
                var currentProgram = int.Parse(matches[0].Value);
                connectedPrograms.TryAdd(currentProgram, new List<int>());
                foreach (var found in matches.Skip(1))
                {
                    connectedPrograms[currentProgram].Add(int.Parse(found.Value));
                }
            }

            return connectedPrograms;
        }

        public static List<int> ProgramsInGroup(int id, Dictionary<int, List<int>> programs)
        {
            var programsInGroup = new List<int>(programs[id]);

            for (var i = 0; i < programsInGroup.Count; i++)
            {
                var p = programsInGroup[i];
                var difference = programs[p].Except(programsInGroup);
                programsInGroup.AddRange(difference);
            }

            return programsInGroup;
        }

        public static int CountGroups(Dictionary<int, List<int>> programs)
        {
            var groups = 0;
            var id = 0;

            while (true)
            {
                var programsInGroup = ProgramsInGroup(id, programs);
                foreach (var p in programsInGroup)
                {
                    programs.Remove(p);
                }

                groups++;

                if (programs.Count == 0) { break; }

                id = programs.Keys.First();
            }

            return groups;
        }
    }
}