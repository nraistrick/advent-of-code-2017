using System;
using System.Collections.Generic;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day12.Test
{
    [TestClass]
    public class TestProgramPipes
    {
        [TestMethod]
        public void TestGetConnectedPrograms()
        {
            var pipeData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var connectedPrograms = Program.GetConnectedPrograms(pipeData);
            CollectionAssert.AreEqual(connectedPrograms[0], new List<int> {2});
            CollectionAssert.AreEqual(connectedPrograms[2], new List<int> {0, 3, 4});
            CollectionAssert.AreEqual(connectedPrograms[6], new List<int> {4, 5});
        }

        [TestMethod]
        public void TestGetProgramsInGroup()
        {
            var pipeData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var connectedPrograms = Program.GetConnectedPrograms(pipeData);
            CollectionAssert.AreEquivalent(new List<int> {0, 2, 3, 4, 5, 6},
                                           Program.ProgramsInGroup(0, connectedPrograms));
        }

        [TestMethod]
        public void TestCountGroups()
        {
            var pipeData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var connectedPrograms = Program.GetConnectedPrograms(pipeData);
            Assert.AreEqual(2, Program.CountGroups(connectedPrograms));
        }
    }
}