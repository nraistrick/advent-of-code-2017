using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day11.Test
{
    [TestClass]
    public class TestHexGrid
    {
        [TestMethod]
        public void TestGetDistance()
        {
            Assert.AreEqual(3, Program.CalculateStepsFromOrigin(new string[] {"ne", "ne", "ne"}));
            Assert.AreEqual(0, Program.CalculateStepsFromOrigin(new string[] {"ne", "ne", "sw", "sw"}));
            Assert.AreEqual(2, Program.CalculateStepsFromOrigin(new string[] {"ne", "ne", "s", "s"}));
            Assert.AreEqual(3, Program.CalculateStepsFromOrigin(new string[] {"se", "sw", "se", "sw", "sw"}));
        }
    }
}