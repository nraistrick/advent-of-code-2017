using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day03.Test
{
    [TestClass]
    public class TestSpiralMemory
    {
        [TestMethod]
        public void CanCreateMemoryMap()
        {
            var map = new SpiralMemory(1);
            var actual = map.GenerateEntries();
            var expected = new List<SpiralMemoryEntry> { new SpiralMemoryEntry(0, 0, 1) };
            
            CheckMemoryEntriesEqual(expected, actual);

            map = new SpiralMemory(4);
            actual = map.GenerateEntries();
            expected = new List<SpiralMemoryEntry> { new SpiralMemoryEntry(0, 0, 1),
                                                     new SpiralMemoryEntry(1, 0, 2),
                                                     new SpiralMemoryEntry(1, 1, 3),
                                                     new SpiralMemoryEntry(0, 1, 4) };

            CheckMemoryEntriesEqual(expected, actual);

            map = new SpiralMemory(9);
            actual = map.GenerateEntries();
            expected = new List<SpiralMemoryEntry> { new SpiralMemoryEntry(1, 1, 1),
                                                     new SpiralMemoryEntry(2, 1, 2),
                                                     new SpiralMemoryEntry(2, 2, 3),
                                                     new SpiralMemoryEntry(1, 2, 4),
                                                     new SpiralMemoryEntry(0, 2, 5),
                                                     new SpiralMemoryEntry(0, 1, 6),
                                                     new SpiralMemoryEntry(0, 0, 7),
                                                     new SpiralMemoryEntry(1, 0, 8),
                                                     new SpiralMemoryEntry(2, 0, 9) };

            CheckMemoryEntriesEqual(expected, actual);

        }

        [TestMethod]
        public void CanCreateStressTestMemoryMap()
        {
            var map = new SpiralMemory(1);
            var actual = map.GenerateStressTestEntries(100);
            var expected = new List<SpiralMemoryEntry> { new SpiralMemoryEntry(0, 0, 1) };

            CheckMemoryEntriesEqual(expected, actual);

            map = new SpiralMemory(4);
            actual = map.GenerateStressTestEntries(100);
            expected = new List<SpiralMemoryEntry> { new SpiralMemoryEntry(0, 0, 1),
                                                     new SpiralMemoryEntry(1, 0, 1),
                                                     new SpiralMemoryEntry(1, 1, 2),
                                                     new SpiralMemoryEntry(0, 1, 4) };

            CheckMemoryEntriesEqual(expected, actual);

            map = new SpiralMemory(9);
            actual = map.GenerateStressTestEntries(100);
            expected = new List<SpiralMemoryEntry> { new SpiralMemoryEntry(1, 1, 1),
                                                     new SpiralMemoryEntry(2, 1, 1),
                                                     new SpiralMemoryEntry(2, 2, 2),
                                                     new SpiralMemoryEntry(1, 2, 4),
                                                     new SpiralMemoryEntry(0, 2, 5),
                                                     new SpiralMemoryEntry(0, 1, 10),
                                                     new SpiralMemoryEntry(0, 0, 11),
                                                     new SpiralMemoryEntry(1, 0, 23),
                                                     new SpiralMemoryEntry(2, 0, 25) };

            CheckMemoryEntriesEqual(expected, actual);

        }

        private static void CheckMemoryEntriesEqual(IEnumerable<SpiralMemoryEntry> expected,
                                                    IEnumerable<SpiralMemoryEntry> actual)
        {
            var entryPairs = expected.Zip(actual, (e, a) => new { Expected = e, Actual = a });
            foreach (var pair in entryPairs)
            {
                Assert.AreEqual(pair.Expected.X, pair.Actual.X);
                Assert.AreEqual(pair.Expected.Y, pair.Actual.Y);
                Assert.AreEqual(pair.Expected.Value, pair.Actual.Value);
            }
        }

        [TestMethod]
        public void CalculatesCorrectDistanceFromOrigin()
        {
            var map = new SpiralMemory(1);

            // Create all the entries
            foreach (var unused in map.GenerateEntries()) {}
            Assert.AreEqual(0, map.DistanceFromOrigin());

            map = new SpiralMemory(12);
            foreach (var unused in map.GenerateEntries()) {}
            Assert.AreEqual(3, map.DistanceFromOrigin());

            map = new SpiralMemory(23);
            foreach (var unused in map.GenerateEntries()) {}
            Assert.AreEqual(2, map.DistanceFromOrigin());

            map = new SpiralMemory(1024);
            foreach (var unused in map.GenerateEntries()) {}
            Assert.AreEqual(31, map.DistanceFromOrigin());
        }
    }
}