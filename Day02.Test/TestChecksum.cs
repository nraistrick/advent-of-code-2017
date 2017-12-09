using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using static Day02.Program;

namespace Day02.Test
{
    [TestClass]
    public class TestChecksum
    {
        [TestMethod]
        public void CanCalculateFileDivisibleChecksum()
        {
            Assert.ThrowsException<ArgumentNullException>(() => CalculateDivisibleChecksum(null));
            var data = Testing.GetTestFileContents("DivisibleTestInput.txt");
            Assert.AreEqual(9, CalculateDivisibleChecksum(data));
        }

        [TestMethod]
        public void CanFindDivisibleNumberSum()
        {
            var exception = Assert.ThrowsException<InvalidOperationException>(()
                                => CalculateDivisibleSum(new List<int> { 5, 7, 3 }));
            Assert.IsTrue(exception.Message.Contains("5, 7, 3"));

            Assert.AreEqual(4, CalculateDivisibleSum(new List<int> { 5, 9, 2, 8 }));
            Assert.AreEqual(3, CalculateDivisibleSum(new List<int> { 9, 4, 7, 3 }));
            Assert.AreEqual(2, CalculateDivisibleSum(new List<int> { 3, 8, 6, 5 }));
        }

        [TestMethod]
        public void CanCalculateFileChecksum()
        {
            Assert.ThrowsException<ArgumentNullException>(() => CalculateChecksum(null));
            var data = Testing.GetTestFileContents("TestInput.txt");
            Assert.AreEqual(18, CalculateChecksum(data));
        }

        [TestMethod]
        public void CanCalculateMaxDifference()
        {
            Assert.ThrowsException<ArgumentNullException>(() => CalculateMaxDifference(null));
            Assert.AreEqual(8, CalculateMaxDifference(new List<int> {5, 1, 9, 5}));
            Assert.AreEqual(4, CalculateMaxDifference(new List<int> {7, 5, 3}));
            Assert.AreEqual(6, CalculateMaxDifference(new List<int> {2, 4, 6, 8}));
        }

        [TestMethod]
        public void CanExtractValues()
        {
            Assert.ThrowsException<ArgumentNullException>(() => GetValues(null));
            CollectionAssert.AreEqual(new List<int> {5, 1, 9, 5}, GetValues("5 1 9 5"));
            CollectionAssert.AreEqual(new List<int> {116, 1470, 2610}, GetValues("116	1470	2610"));
        }

        [TestMethod]
        public void CanParseInputData()
        {
            var data = Testing.GetTestFileContents("TestInput.txt");
            var actual = GetFileValues(data);
            var expected = new List<List<int>>
            {
                new List<int> {5, 1, 9, 5},
                new List<int> {7, 5, 3},
                new List<int> {2, 4, 6, 8}
            };

            Testing.AssertNestedListsEqual(expected, actual);
        }
    }
}