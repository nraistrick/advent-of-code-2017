using System;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day18.Test
{
    [TestClass]
    public class TestAssemblyRunner
    {
        [TestMethod]
        public void TestExecuteInstructions()
        {
            var instructions = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var runner = new AssemblyRunner(instructions);

            runner.ExecuteInstructions();
            Assert.AreEqual(4, runner.LastSoundPlayed);
        }
    }
}