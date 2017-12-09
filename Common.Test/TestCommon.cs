using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}