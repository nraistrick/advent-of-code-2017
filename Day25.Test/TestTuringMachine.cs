using System;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day25.Test
{
    [TestClass]
    public class TestTuringMachine
    {
        [TestMethod]
        public void TestExecuteBlueprint()
        {
            var instructions = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var turingMachine = new TuringMachine(instructions.ToList());

            turingMachine.ExecuteBlueprint();

            Assert.AreEqual(3, turingMachine.CalculateChecksum());
        }
    }
}