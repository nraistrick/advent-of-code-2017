using System;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day23.Test
{
    [TestClass]
    public class TestRunner
    {
        [TestMethod]
        public void TestRunInstructions()
        {
            var instructions = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var runner = new AssemblyRunner(instructions);

            while (runner.CurrentLine != -1) runner.ExecuteNext();

            Assert.AreEqual(57, runner.Registers["b"]);
            Assert.AreEqual(57, runner.Registers["c"]);
            Assert.AreEqual(57, runner.Registers["d"]);
            Assert.AreEqual(57, runner.Registers["e"]);
            Assert.AreEqual(0,  runner.Registers["f"]);
            Assert.AreEqual(0,  runner.Registers["g"]);
            Assert.AreEqual(1,  runner.Registers["h"]);
        }
    }
}