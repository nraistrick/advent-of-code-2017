using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day15.Test
{
    [TestClass]
    public class TestGenerators
    {
        [TestMethod]
        public void TestGeneratedValues()
        {
            var generatorA = new Generator(65, 16807);
            Assert.AreEqual(1092455, generatorA.CalculateNext());
            Assert.AreEqual(1181022009, generatorA.CalculateNext());
            Assert.AreEqual(245556042, generatorA.CalculateNext());
            Assert.AreEqual(1744312007, generatorA.CalculateNext());
            Assert.AreEqual(1352636452, generatorA.CalculateNext());

            var generatorB = new Generator(8921, 48271);
            Assert.AreEqual(430625591, generatorB.CalculateNext());
            Assert.AreEqual(1233683848, generatorB.CalculateNext());
            Assert.AreEqual(1431495498, generatorB.CalculateNext());
            Assert.AreEqual(137874439, generatorB.CalculateNext());
            Assert.AreEqual(285222916, generatorB.CalculateNext());
        }

        [TestMethod]
        public void TestDivisibleGeneratedValues()
        {
            var generatorA = new Generator(65, 16807, 4);
            Assert.AreEqual(1352636452, generatorA.CalculateNext());
            Assert.AreEqual(1992081072, generatorA.CalculateNext());
            Assert.AreEqual(530830436, generatorA.CalculateNext());
            Assert.AreEqual(1980017072, generatorA.CalculateNext());
            Assert.AreEqual(740335192, generatorA.CalculateNext());

            var generatorB = new Generator(8921, 48271, 8);
            Assert.AreEqual(1233683848, generatorB.CalculateNext());
            Assert.AreEqual(862516352, generatorB.CalculateNext());
            Assert.AreEqual(1159784568, generatorB.CalculateNext());
            Assert.AreEqual(1616057672, generatorB.CalculateNext());
            Assert.AreEqual(412269392, generatorB.CalculateNext());
        }

        [TestMethod]
        public void TestGetBinaryRepresentation()
        {
            Assert.AreEqual("00000000000100001010101101100111", 1092455.GetBinaryRepresentation());
            Assert.AreEqual("01000110011001001111011100111001", 1181022009.GetBinaryRepresentation());
            Assert.AreEqual("00001110101000101110001101001010", 245556042.GetBinaryRepresentation());
        }

        [TestMethod]
        public void TestCheckLeastSignificantBitsMatch()
        {
            Assert.AreEqual(false, Program.CheckLeastSignificantBitsMatch("00000000000100001010101101100111",
                                                                          "00011001101010101101001100110111"));

            Assert.AreEqual(true, Program.CheckLeastSignificantBitsMatch("00001110101000101110001101001010",
                                                                         "01010101010100101110001101001010"));
        }

        [TestMethod]
        public void TestCountPartialMatches()
        {
            var generatorA = new Generator(65, 16807);
            var generatorB = new Generator(8921, 48271);

            Assert.AreEqual(1, Program.CountPartialMatches(5, generatorA, generatorB));
        }

        [TestMethod]
        public void TestCountDivisiblePartialMatches()
        {
            var generatorA = new Generator(65, 16807, 4);
            var generatorB = new Generator(8921, 48271, 8);

            Assert.AreEqual(1, Program.CountPartialMatches(1056, generatorA, generatorB));
        }
    }
}