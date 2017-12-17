using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day07.Test
{
    [TestClass]
    public class TestProgramOrganisation
    {
        private const string RootProgram = "tknk";

        [TestMethod]
        public void TestGetBottomProgram()
        {
            var fileLines = Testing.GetTestFileContents("TestInput.txt").Split().ToList();
            var programNames = Program.GetProgramNames(fileLines);
            Assert.AreEqual(RootProgram, Program.GetRootProgram(programNames));
        }

        [TestMethod]
        public void TestCreatePrograms()
        {
            var fileData = new List<string> {"pbga (66)"};
            CollectionAssert.AreEqual(new List<string> {"pbga"},
                Program.GetProgramNames(fileData));

            fileData = new List<string> {"fwft (72) -> ktlj, cntj, xhth"};
            CollectionAssert.AreEqual(new List<string> {"fwft", "ktlj", "cntj", "xhth"},
                Program.GetProgramNames(fileData));

            fileData = Testing.GetTestFileContents("TestInput.txt").Split().ToList();
            CollectionAssert.AreEquivalent(new List<string> {"gyxo", "ugml", "ebii", "jptl",
                                                             "pbga", "padx", "havc", "qoyq",
                                                             "ktlj", "fwft", "cntj", "xhth",

                                                             "gyxo", "ugml", "ebii", "jptl",
                                                             "pbga", "padx", "havc", "qoyq",
                                                             "ktlj", "fwft", "cntj", "xhth",

                                                             "tknk"
                },
                Program.GetProgramNames(fileData));
        }

        [TestMethod]
        public void TestCreateProgramNodes()
        {
            var fileData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var programs = Program.CreateProgramNodes(fileData);
            Assert.AreEqual((programs["pbga"].Id, programs["pbga"].Weight), ("pbga", 66));
            Assert.AreEqual((programs["xhth"].Id, programs["xhth"].Weight), ("xhth", 57));
            Assert.AreEqual((programs["padx"].Id, programs["padx"].Weight), ("padx", 45));
            Assert.AreEqual((programs["cntj"].Id, programs["cntj"].Weight), ("cntj", 57));
        }

        [TestMethod]
        public void TestLinkProgramNodes()
        {
            var fileData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var programs = Program.CreateProgramNodes(fileData);
            Program.LinkProgramNodes(programs, fileData);

            Assert.AreEqual(programs["fwft"].Children[0].Id, "ktlj");
            Assert.AreEqual(programs["fwft"].Children[1].Id, "cntj");
            Assert.AreEqual(programs["fwft"].Children[2].Id, "xhth");

            Assert.AreEqual(programs["padx"].Children[0].Id, "pbga");
            Assert.AreEqual(programs["padx"].Children[1].Id, "havc");
            Assert.AreEqual(programs["padx"].Children[2].Id, "qoyq");

            Assert.AreEqual(programs["ugml"].Children[0].Id, "gyxo");
            Assert.AreEqual(programs["ugml"].Children[1].Id, "ebii");
            Assert.AreEqual(programs["ugml"].Children[2].Id, "jptl");
        }

        [TestMethod]
        public void TestCombineProgramWeights()
        {
            var fileData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var programs = Program.CreateProgramNodes(fileData);
            Program.LinkProgramNodes(programs, fileData);
            Program.CombineProgramWeights(programs[RootProgram]);

            Assert.AreEqual(programs["pbga"].CombinedWeight, 66);
            Assert.AreEqual(programs["ebii"].CombinedWeight, 61);
            Assert.AreEqual(programs["cntj"].CombinedWeight, 57);

            Assert.AreEqual(programs["fwft"].CombinedWeight, 243);
            Assert.AreEqual(programs["padx"].CombinedWeight, 243);
            Assert.AreEqual(programs["ugml"].CombinedWeight, 251);

            Assert.AreEqual(programs["tknk"].CombinedWeight, 778);
        }

        [TestMethod]
        public void TestFindImbalancedNode()
        {
            var fileData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var programs = Program.CreateProgramNodes(fileData);
            Program.LinkProgramNodes(programs, fileData);
            Program.CombineProgramWeights(programs[RootProgram]);

            (var node, var imbalance) = Program.FindImbalancedNode(programs[RootProgram]);
            Assert.AreEqual(node.Id, "ugml");
            Assert.AreEqual(imbalance, (uint?)8);
        }
    }
}