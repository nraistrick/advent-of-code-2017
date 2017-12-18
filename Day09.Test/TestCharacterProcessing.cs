using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day09.Test
{
    [TestClass]
    public class TestCharacterProcessing
    {
        [TestMethod]
        public void TestCountGroupScore()
        {
            Assert.AreEqual(1,  Program.CountGroupScore("{}"));
            Assert.AreEqual(6,  Program.CountGroupScore("{{{}}}"));
            Assert.AreEqual(5,  Program.CountGroupScore("{{},{}}"));
            Assert.AreEqual(16, Program.CountGroupScore("{{{},{},{{}}}}"));
            Assert.AreEqual(1,  Program.CountGroupScore("{<a>,<a>,<a>,<a>}"));
            Assert.AreEqual(9,  Program.CountGroupScore("{{<ab>},{<ab>},{<ab>},{<ab>}}"));
            Assert.AreEqual(9,  Program.CountGroupScore("{{<!!>},{<!!>},{<!!>},{<!!>}}"));
            Assert.AreEqual(3,  Program.CountGroupScore("{{<a!>},{<a!>},{<a!>},{<ab>}}"));
        }
    }
}