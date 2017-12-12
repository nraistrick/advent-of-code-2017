using System.Collections.Generic;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day07.Test
{
    [TestClass]
    public class TestProgramOrganisation
    {
        [TestMethod]
        public void TestGetBottomProgram()
        {
            var fileLines = Testing.GetTestFileContents("TestInput.txt").Split().ToList();
            var programNames = Program.GetProgramNames(fileLines);
            Assert.AreEqual("tknk", Program.GetRootProgram(programNames));
        }

        [TestMethod]
        public void TestGetProgramNames()
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
    }
}