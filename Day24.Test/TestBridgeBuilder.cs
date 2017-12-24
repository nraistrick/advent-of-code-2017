using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day24.Test
{
    [TestClass]
    public class TestBridgeBuilder
    {
        [TestMethod]
        public void TestFindBridgeStrength()
        {
            var parts = new List<string> {"0/1", "10/1", "9/10"};
            Assert.AreEqual(31, Program.FindBridgeStrength(parts));
        }

        [TestMethod]
        public void TestConstructPossibleBridges()
        {
            var parts = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine).ToList();

            var bridgeOptions = Program.ConstructPossibleBridges(parts);
            var options = new List<List<string>>
            {
                new List<string> {"0/2", "2/3", "3/4"},
                new List<string> {"0/2", "2/3", "3/5"},
                new List<string> {"0/1", "10/1", "9/10"},
                new List<string> {"0/2", "2/2", "2/3", "3/4"},
                new List<string> {"0/2", "2/2", "2/3", "3/5"},
            };

            Testing.AssertNestedListsEqual(options, bridgeOptions);
        }

        [TestMethod]
        public void TestFindStrongestBridge()
        {
            var options = new List<List<string>>
            {
                new List<string> {"0/2", "2/3", "3/4"},
                new List<string> {"0/2", "2/3", "3/5"},
                new List<string> {"0/1", "10/1", "9/10"},
                new List<string> {"0/2", "2/2", "2/3", "3/4"},
                new List<string> {"0/2", "2/2", "2/3", "3/5"},
            };

            Assert.AreEqual(31, Program.FindStrongestBridge(options));
        }
    }
}