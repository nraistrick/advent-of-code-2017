using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day20
{
    /// <summary>
    /// Calculates information about the movement of a particle swarm
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var inputData = File.ReadAllLines("Input.txt");
            var particles = LoadParticles(inputData);
            var closestToOrigin = ParticleWhichStaysClosestToTheOrigin(particles);
            Console.WriteLine($"The particle which stays nearest the origin is: {closestToOrigin}");

            particles = LoadParticles(inputData);
            var particlesAfterCollisons = ParticlesLeftAfterCollisions(particles);
            Console.WriteLine($"The number of particles left after collisions is {particlesAfterCollisons}");
        }

        /// <summary>
        /// Instantiates a collection of particle objects from a provided list of input data
        /// </summary>
        public static List<Particle> LoadParticles(IEnumerable<string> inputData)
        {
            (int, int, int) ToTuple(Capture capture)
            {
                var s = capture.Value.Split(",").Select(int.Parse).ToList();
                return (s[0], s[1], s[2]);
            }

            var particles = new List<Particle>();
            foreach (var line in inputData)
            {
                var m = Regex.Match(line, @"p=<(?<position>.*)>, v=<(?<velocity>.*)>, a=<(?<acceleration>.*)>");

                particles.Add(new Particle(ToTuple(m.Groups["position"]),
                                           ToTuple(m.Groups["velocity"]),
                                           ToTuple(m.Groups["acceleration"])));
            }

            return particles;
        }

        /// <summary>
        /// Calculates the number of particles left after the ones that collide
        /// are cancelled out
        /// </summary>
        public static int ParticlesLeftAfterCollisions(IList<Particle> particles)
        {
            const int moves = 5000;

            for (var i = 0; i < moves; i++)
            {
                particles = RemoveCollidedParticles(particles);
                foreach (var particle in particles) { particle.Move(); }
            }

            return particles.Count;
        }

        /// <summary>
        /// Removes all particles that have the same position
        /// </summary>
        private static IList<Particle> RemoveCollidedParticles(IList<Particle> particles)
        {
            // Groups particles by their position
            var positions = new Dictionary<(int, int, int), List<int>>();
            for (var i = 0; i < particles.Count; i++)
            {
                var particle = particles[i];
                if (positions.ContainsKey(particle.Position))
                {
                    positions[particle.Position].Add(i);
                    continue;
                }

                positions.Add(particle.Position, new List<int> { i });
            }

            // Group all particles which share a position
            var collisions = new List<int>();
            foreach (var item in positions)
            {
                if (item.Value.Count > 1)
                {
                    collisions.AddRange(item.Value);
                }
            }

            // Remove collided particles
            collisions.Sort();
            collisions.Reverse();
            foreach (var i in collisions) { particles.RemoveAt(i); }

            return particles;
        }

        /// <summary>
        /// Finds the particle that is closest to the origin after many movements
        /// </summary>
        public static int ParticleWhichStaysClosestToTheOrigin(IList<Particle> particles)
        {
            const int moves = 5000;

            var particleId = -1;

            for (var i = 0; i < moves; i++)
            {
                particleId = ParticleClosestToOrigin(particles);

                foreach (var p in particles) { p.Move(); }
            }

            return particleId;
        }

        /// <summary>
        /// Finds the particle closest to the origin for the current state
        /// </summary>
        public static int ParticleClosestToOrigin(IEnumerable<Particle> particles)
        {
            var shortestDistance = 0;
            var closestToOrigin = -1;

            foreach (var item in particles.Select((particle, id) => new { id, particle }))
            {
                if (item.particle.OriginDistance >= shortestDistance && shortestDistance != 0) continue;

                shortestDistance = item.particle.OriginDistance;
                closestToOrigin = item.id;
            }

            return closestToOrigin;
        }
    }
}