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

        [TestMethod]
        public void TestGetCustomInputLengths()
        {
            CollectionAssert.AreEqual(new List<int> {49, 44, 50, 44, 51, 17, 31, 73, 47, 23},
                                      Program.GetAsciiLengths("1,2,3"));

        }

        [TestMethod]
        public void TestCalculateDenseHash()
        {
            Assert.AreEqual(64, Program.CalculateDenseHashValue(new List<int> {65, 27, 9, 1,
                                                                               4, 3, 40, 50,
                                                                               91, 7, 6, 0,
                                                                               2, 5, 68, 22}));
        }

        [TestMethod]
        public void TestGetHexadecimalString()
        {
            Assert.AreEqual("4007ff", Program.GetHexadecimalString(new List<int> {64, 7, 255}));
        }

        [TestMethod]
        public void TestCalculateKnotHash()
        {
            Assert.AreEqual("a2582a3a0e66e6e86e3812dcb672a272", Program.CalculateKnotHash(""));
            Assert.AreEqual("33efeb34ea91902bb2f59c9920caa6cd", Program.CalculateKnotHash("AoC 2017"));
            Assert.AreEqual("3efbe78a8d82f29979031a4aa0b16a9d", Program.CalculateKnotHash("1,2,3"));
            Assert.AreEqual("63960835bcdc130f0b66d7ff4f6a5a8e", Program.CalculateKnotHash("1,2,4"));
        }
    }
}