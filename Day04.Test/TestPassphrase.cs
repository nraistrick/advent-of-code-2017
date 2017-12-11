using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day04.Test
{
    [TestClass]
    public class TestPassphrase
    {
        [TestMethod]
        public void CheckForDuplicateWords()
        {
            Assert.IsTrue(Program.ContainsDuplicateWords("aa bb cc dd ee"));
            Assert.IsFalse(Program.ContainsDuplicateWords("aa bb cc dd aa"));
            Assert.IsTrue(Program.ContainsDuplicateWords("aa bb cc dd aaa"));
        }
    }
}