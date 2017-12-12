using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day05.Test
{
    [TestClass]
    public class TestNavigateList
    {
        [TestMethod]
        public void ParsesInputListCorrectly()
        {
            var inputData = Testing.GetTestFileContents("TestInput.txt");
            var instructions = Program.GetInputArray(inputData);
            CollectionAssert.AreEqual(new [] {0, 3, 0, 1, -3}, instructions);
        }

        [TestMethod]
        public void CountsCorrectNumberOfSteps()
        {
            var instructions = new [] {0, 3, 0, 1, -3};
            var steps = Program.NavigateInstructions(instructions);
            Assert.AreEqual(5, steps);
        }

        [TestMethod]
        public void CountsCorrectNumberOfStepsWithMaxOffset()
        {
            var instructions = new [] {0, 3, 0, 1, -3};
            var steps = Program.NavigateInstructions(instructions, 3);
            Assert.AreEqual(10, steps);
        }
    }
}