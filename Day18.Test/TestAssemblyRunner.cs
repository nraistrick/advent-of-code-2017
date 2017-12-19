using System;
using System.Collections.Generic;
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
            var (programOneOutput, programTwoOutput) = (new Queue<long>(), new Queue<long>());

            var firstRunner  = new AssemblyRunner(instructions, programTwoOutput, programOneOutput, 0);
            var secondRunner = new AssemblyRunner(instructions, programOneOutput, programTwoOutput, 1);

            Program.RunTwoRunnersInParallel(firstRunner, secondRunner);
            Assert.AreEqual(3, firstRunner.ValuesSent);
        }
    }
}