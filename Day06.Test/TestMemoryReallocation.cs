using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day06.Test
{
    [TestClass]
    public class TestMemoryReallocation
    {
        [TestMethod]
        public void TestReallocateBlocks()
        {
            CollectionAssert.AreEqual(new List<int> {2, 4, 1, 2},
                                      Program.ReallocateBlocks(new List<int> {0, 2, 7, 0}));

            CollectionAssert.AreEqual(new List<int> {3, 1, 2, 3},
                                      Program.ReallocateBlocks(new List<int> {2, 4, 1, 2}));

            CollectionAssert.AreEqual(new List<int> {0, 2, 3, 4},
                                      Program.ReallocateBlocks(new List<int> {3, 1, 2, 3}));

            CollectionAssert.AreEqual(new List<int> {1, 3, 4, 1},
                                      Program.ReallocateBlocks(new List<int> {0, 2, 3, 4}));

            CollectionAssert.AreEqual(new List<int> {2, 4, 1, 2},
                                      Program.ReallocateBlocks(new List<int> {1, 3, 4, 1}));
        }

        [TestMethod]
        public void TestFindRepeatConfiguration()
        {
            var allocations = new List<int> {0, 2, 7, 0};
            Assert.AreEqual(5, Program.FindStepsToRepeatConfiguration(allocations));
            Assert.AreEqual(4, Program.FindStepsToRepeatConfiguration(allocations));
        }
    }
}