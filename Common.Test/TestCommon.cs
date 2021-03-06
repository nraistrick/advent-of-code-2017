using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.TestHost;
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

        [TestMethod]
        public void TestRotateLeft()
        {
            var items = new List<int> {1, 2, 3};

            items = items.RotateLeft();
            CollectionAssert.AreEqual(new List<int> {2, 3, 1}, items);

            items = items.RotateLeft();
            CollectionAssert.AreEqual(new List<int> {3, 1, 2}, items);

            items = items.RotateLeft();
            CollectionAssert.AreEqual(new List<int> {1, 2, 3}, items);

            items = items.RotateLeft(2);
            CollectionAssert.AreEqual(new List<int> {3, 1, 2}, items);

            items = items.RotateLeft(5);
            CollectionAssert.AreEqual(new List<int> {2, 3, 1}, items);
        }

        [TestMethod]
        public void TestRotateRight()
        {
            var items = new List<int> {1, 2, 3};

            items = items.RotateRight();
            CollectionAssert.AreEqual(new List<int> {3, 1, 2}, items);

            items = items.RotateRight();
            CollectionAssert.AreEqual(new List<int> {2, 3, 1}, items);

            items = items.RotateRight();
            CollectionAssert.AreEqual(new List<int> {1, 2, 3}, items);

            items = items.RotateRight(2);
            CollectionAssert.AreEqual(new List<int> {2, 3, 1}, items);

            items = items.RotateRight(5);
            CollectionAssert.AreEqual(new List<int> {3, 1, 2}, items);
        }

        [TestMethod]
        public void TestInsertCharacter()
        {
            Assert.AreEqual("##./#../...", Utilities.InsertCharacterAtIntervals("##.#.....", '/', 3));
        }

        [TestMethod]
        public void TestGetArraySubsection()
        {
            char[,] testArray =
            {
                {'.', '#', '.'},
                {'.', '.', '#'},
                {'#', '#', '#'}
            };

            var subsection = new [,]
            {
                {'.', '#',},
                {'.', '.',}
            };
            CollectionAssert.AreEqual(subsection, Utilities.GetArraySubsection(testArray, 2, 0, 0));

            subsection = new [,]
            {
                {'#', '.',},
                {'.', '#',}
            };
            CollectionAssert.AreEqual(subsection, Utilities.GetArraySubsection(testArray, 2, 1, 0));

            subsection = new [,]
            {
                {'.', '.',},
                {'#', '#',}
            };
            CollectionAssert.AreEqual(subsection, Utilities.GetArraySubsection(testArray, 2, 0, 1));

            subsection = new [,]
            {
                {'.', '#',},
                {'#', '#',}
            };
            CollectionAssert.AreEqual(subsection, Utilities.GetArraySubsection(testArray, 2, 1, 1));
        }

        [TestMethod]
        public void TestInsertArraySubsection()
        {
            var subsection = new [,]
            {
                {'.', '#',},
                {'.', '.',}
            };

            var expected = new[,]
            {
                {'.',   '#', '\0', '\0'},
                {'.',   '.', '\0', '\0'},
                {'\0', '\0', '\0', '\0'},
                {'\0', '\0', '\0', '\0'}
            };
            var newArray = new char[4, 4];
            CollectionAssert.AreEqual(expected, Utilities.InsertArraySubsection(subsection, newArray, 0, 0));

            expected = new[,]
            {
                {'\0', '\0',  '.',  '#'},
                {'\0', '\0',  '.',  '.'},
                {'\0', '\0', '\0', '\0'},
                {'\0', '\0', '\0', '\0'}
            };
            newArray = new char[4, 4];
            CollectionAssert.AreEqual(expected, Utilities.InsertArraySubsection(subsection, newArray, 2, 0));

            expected = new[,]
            {
                {'\0', '\0', '\0', '\0'},
                {'\0', '\0', '\0', '\0'},
                {'\0', '\0',  '.',  '#'},
                {'\0', '\0',  '.',  '.'}
            };
            newArray = new char[4, 4];
            CollectionAssert.AreEqual(expected, Utilities.InsertArraySubsection(subsection, newArray, 2, 2));
        }

        [TestMethod]
        public void TestCloneList()
        {
            var firstList = new List<string> {"hi", "there"};
            var secondList = firstList.Clone();

            firstList.Add("bob");

            CollectionAssert.AreNotEqual(firstList, secondList.ToList());
            Assert.AreEqual(3, firstList.Count);
            Assert.AreEqual(2, secondList.Count);
        }
    }
}