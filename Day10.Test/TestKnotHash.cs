using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day10.Test
{
    [TestClass]
    public class TestKnotHash
    {
        [TestMethod]
        public void TestScrambleNumbers()
        {
            var hashIterator = Program.ScrambleNumbers(new List<int> {0, 1, 2, 3, 4},
                                                               new List<int> {3, 4, 1, 5});

            var hashes = hashIterator.ToList();

            CollectionAssert.AreEqual(new List<int> {2, 1, 0, 3, 4}, hashes[0]);
            CollectionAssert.AreEqual(new List<int> {4, 3, 0, 1, 2}, hashes[1]);
            CollectionAssert.AreEqual(new List<int> {4, 3, 0, 1, 2}, hashes[2]);
            CollectionAssert.AreEqual(new List<int> {3, 4, 2, 1, 0}, hashes[3]);
        }
    }
}