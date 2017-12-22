using System;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day22.Test
{
    [TestClass]
    public class TestVirusCarrier
    {
        [TestMethod]
        public void TestCountInfectedNodes()
        {
            var inputData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);

            Assert.AreEqual(5, Program.CountInfections(7, inputData));
            Assert.AreEqual(41, Program.CountInfections(70, inputData));
            Assert.AreEqual(5587, Program.CountInfections(10000, inputData));
        }
    }
}