using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day04.Test
{
    [TestClass]
    public class TestPassphrase
    {
        [TestMethod]
        public void CheckForDuplicateWords()
        {
            Assert.IsFalse(Program.ContainsDuplicateWords("aa bb cc dd ee"));
            Assert.IsTrue(Program.ContainsDuplicateWords("aa bb cc dd aa"));
            Assert.IsFalse(Program.ContainsDuplicateWords("aa bb cc dd aaa"));
        }

        [TestMethod]
        public void CheckForDuplicateAnagrams()
        {
            Assert.IsTrue(Program.ContainsDuplicateAnagrams("abcde xyz ecdab"));
            Assert.IsTrue(Program.ContainsDuplicateAnagrams("oiii ioii iioi iiio"));
            Assert.IsTrue(Program.ContainsDuplicateAnagrams("una bokpr ftz ryw nau yknf fguaczl anu"));

            Assert.IsFalse(Program.ContainsDuplicateAnagrams("abcde fghij"));
            Assert.IsFalse(Program.ContainsDuplicateAnagrams("a ab abc abd abf abj"));
            Assert.IsFalse(Program.ContainsDuplicateAnagrams("iiii oiii ooii oooi oooo"));
            Assert.IsFalse(Program.ContainsDuplicateAnagrams("ac bb"));
        }
    }
}