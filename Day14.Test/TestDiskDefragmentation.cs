using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day14.Test
{
    [TestClass]
    public class TestDiskDefragmentation
    {
        [TestMethod]
        public void TestGetKnotHashBinary()
        {
            Assert.AreEqual("11010100", Program.GetKnotHashBinary("flqrgnkx-0").Substring(0, 8));
        }

        [TestMethod]
        public void TestCountNumberOfOnes()
        {
            Assert.AreEqual(4, Program.CountNumberOfOnes("11010100"));
        }

        [TestMethod]
        public void TestCountUsedSquares()
        {
            var grid = Program.CreateDefragmentationGrid("flqrgnkx", 2);
            Assert.AreEqual(146, Program.GetUsedSquares(grid));
        }
    }
}