using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day19.Test
{
    [TestClass]
    public class TestPathFollower
    {
        [TestMethod]
        public void TestGetLetters()
        {
            var map = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var mapFollower = new PathFollower(map);

            var letters = mapFollower.FindLettersOnPath();
            Assert.AreEqual("ABCDEF", string.Join("", letters));
        }
    }
}