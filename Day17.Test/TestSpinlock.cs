using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day17.Test
{
    [TestClass]
    public class TestSpinlock
    {
        [TestMethod]
        public void TestRunSpinlock()
        {
            var circularBuffer = new List<int> {0};
            Program.RunSpinlock(circularBuffer, 9, 3);
            CollectionAssert.AreEqual(new List<int> { 0, 9, 5, 7, 2, 4, 3, 8, 6, 1 }, circularBuffer);
        }

        [TestMethod]
        public void TestGetFinalSpinlockIndex()
        {
            var circularBuffer = new List<int> {0};
            var finalIndex = Program.RunSpinlock(circularBuffer, 2017, 3);
            Assert.AreEqual(638, circularBuffer[finalIndex + 1]);
        }

        [TestMethod]
        public void TestGetSpinlockValueAfterZero()
        {
            Assert.AreEqual(9, Program.GetSpinlockValueAfterZero(9, 3));
        }
    }
}