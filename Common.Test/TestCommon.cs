using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Common.Test
{
    [TestClass]
    public class TestCommon
    {
        [TestMethod]
        public void CanCompareNestedLists()
        {
            try
            {
                Testing.AssertNestedListsEqual(
                    new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 3, 4 } },
                    new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 3, 4 } });

                Testing.AssertNestedListsEqual(
                    new List<List<string>> { new List<string> { "a", "b" }, new List<string> { "c", "d" } },
                    new List<List<string>> { new List<string> { "a", "b" }, new List<string> { "c", "d" } });

                Testing.AssertNestedListsEqual(
                    new List<List<bool>> { new List<bool> { true, false }, new List<bool> { false, true } },
                    new List<List<bool>> { new List<bool> { true, false }, new List<bool> { false, true } });
            }
            catch (Exception e)
            {
                Assert.Fail($"Expected no exception, but got {e.Message}");
            }

            Assert.ThrowsException<AssertFailedException>(() =>
                Testing.AssertNestedListsEqual(
                    new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 3, 4 } },
                    new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 3, 9 } })
                );
        }

        [TestMethod]
        public void CanGetTestFileContents()
        {
            try
            {
                var data = Testing.GetTestFileContents(Path.Combine("Data", "DummyTestFile.txt"));
                Assert.AreEqual("For testing file input...", data);
            }
            catch (Exception e)
            {
                Assert.Fail($"Unable to load test file contents: {e.Message}");
            }
        }

        [TestMethod]
        public void TestOrientation()
        {
            var orientation = new Orientation(Direction.Up);
            Assert.AreEqual(orientation.Current, Direction.Up);

            Assert.AreEqual(orientation.TurnLeft(), Direction.Left);
            Assert.AreEqual(orientation.TurnLeft(), Direction.Down);
            Assert.AreEqual(orientation.TurnLeft(), Direction.Right);
            Assert.AreEqual(orientation.TurnLeft(), Direction.Up);

            Assert.AreEqual(orientation.TurnRight(), Direction.Right);
            Assert.AreEqual(orientation.TurnRight(), Direction.Down);
            Assert.AreEqual(orientation.TurnRight(), Direction.Left);
            Assert.AreEqual(orientation.TurnRight(), Direction.Up);
        }

        [TestMethod]
        public void CorrectlyPrintsTwoDimensionalArray()
        {
            var outputText = "";
            var output = new Mock<Print.IOutputWriter>();
            output.Setup(s => s.Write(It.IsAny<string>())).Callback<string>(s => outputText += s);

            var grid = new [,] {{0, 1}, {2, 3}};
            Print.PrintGrid(grid, 2, output.Object);

            var expectedOutput = " 1  3 " + Environment.NewLine + Environment.NewLine +
                                 " 0  2 " + Environment.NewLine + Environment.NewLine;

            Assert.AreEqual(expectedOutput , outputText);
        }

        [TestMethod]
        public void TestSafeArrayElementAccess()
        {
            var array = new [,] {{1, 2}, {3, 4}};

            var retrieved = array.TryGetElement(0, 0, out var element);
            Assert.AreEqual(true, retrieved);
            Assert.AreEqual(element, 1);

            retrieved = array.TryGetElement(1, 1, out element);
            Assert.AreEqual(true, retrieved);
            Assert.AreEqual(element, 4);

            retrieved = array.TryGetElement(2, 1, out element);
            Assert.AreEqual(false, retrieved);

            retrieved = array.TryGetElement(-1, 0, out element);
            Assert.AreEqual(false, retrieved);
        }
    }
}