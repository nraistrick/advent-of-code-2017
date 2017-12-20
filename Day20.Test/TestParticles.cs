using System;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day20.Test
{
    [TestClass]
    public class TestParticles
    {
        [TestMethod]
        public void TestDistanceFromOrigin()
        {
            var particle = new Particle((0, 0, 0), (0, 0, 0), (0, 0, 0));
            Assert.AreEqual(0, particle.CalculateOriginDistance());

            particle = new Particle((1, 2, 3), (0, 0, 0), (0, 0, 0));
            Assert.AreEqual(6, particle.CalculateOriginDistance());

            particle = new Particle((1, 2, 3), (5, 5, 5), (5, 5, 5));
            Assert.AreEqual(6, particle.CalculateOriginDistance());

            particle = new Particle((4, 0, -5), (0, 0, 0), (0, 0, 0));
            Assert.AreEqual(9, particle.CalculateOriginDistance());
        }

        [TestMethod]
        public void TestMove()
        {
            var particle = new Particle((0, 0, 0), (0, 0, 0), (1, 1, 1));

            Assert.AreEqual((0, 0, 0), particle.Position);
            Assert.AreEqual((0, 0, 0), particle.Velocity);
            Assert.AreEqual((1, 1, 1), particle.Acceleration);
            Assert.AreEqual(0, particle.OriginDistance);

            particle.Move();
            Assert.AreEqual((1, 1, 1), particle.Position);
            Assert.AreEqual((1, 1, 1), particle.Velocity);
            Assert.AreEqual((1, 1, 1), particle.Acceleration);
            Assert.AreEqual(3, particle.OriginDistance);

            particle.Move();
            Assert.AreEqual((3, 3, 3), particle.Position);
            Assert.AreEqual((2, 2, 2), particle.Velocity);
            Assert.AreEqual((1, 1, 1), particle.Acceleration);
            Assert.AreEqual(9, particle.OriginDistance);
        }

        [TestMethod]
        public void TestLoad()
        {
            var inputData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var particles = Program.LoadParticles(inputData);

            Assert.AreEqual(( 4, 0, 0), particles[0].Position);
            Assert.AreEqual(( 1, 0 ,0), particles[0].Velocity);
            Assert.AreEqual((-1, 0, 0), particles[0].Acceleration);
            Assert.AreEqual(4, particles[0].OriginDistance);

            Assert.AreEqual(( 2, 0, 0), particles[1].Position);
            Assert.AreEqual((-2, 0, 0), particles[1].Velocity);
            Assert.AreEqual((-2, 0, 0), particles[1].Acceleration);
            Assert.AreEqual(2, particles[1].OriginDistance);
        }

        [TestMethod]
        public void TestFindClosestToOrigin()
        {
            var inputData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var particles = Program.LoadParticles(inputData);
            Assert.AreEqual(1, Program.ParticleClosestToOrigin(particles));
        }

        [TestMethod]
        public void TestFindWhichStaysClosestToTheOrigin()
        {
            var inputData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var particles = Program.LoadParticles(inputData);
            Assert.AreEqual(0, Program.ParticleWhichStaysClosestToTheOrigin(particles));
        }
    }
}