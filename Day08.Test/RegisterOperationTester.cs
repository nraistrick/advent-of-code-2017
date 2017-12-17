using System;
using System.Collections.Generic;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day08.Test
{
    [TestClass]
    public class RegisterOperationTester
    {
        [TestMethod]
        public void TestRunInstructions()
        {
            var instructions = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var instructionRunner = new InstructionRunner(instructions);
            instructionRunner.RunInstructions();
            CollectionAssert.AreEquivalent(
                new Dictionary<string, int> {{"a", 1}, {"b", 0}, {"c", -10}},
                instructionRunner.Registers);
        }
    }
}