using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Day01.Program;
using static Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert;

namespace Day01.Test
{
    [TestClass]
    public class TestCaptcha
    {
        [TestMethod]
        public void TestGetDuplicateDigits()
        {
            AreEqual(GetDuplicateDigits(new List<(char, char)> {('1', '1'), ('1', '2'), ('2', '2')}),
                     new List<char> { '1', '2' });
            
            AreEqual(GetDuplicateDigits(new List<(char, char)> {('1', '1'), ('1', '1'), ('1', '1')}),
                     new List<char> { '1', '1', '1' });
            
            AreEqual(GetDuplicateDigits(new List<(char, char)> {('1', '2'), ('2', '3'), ('3', '4')}),
                     new List<char>());
        }

        [TestMethod]
        public void TestGetDigitPairs()
        {
            AreEqual(new List<(char, char)> { ('1', '1'), ('1', '1'), ('1', '1'), ('1', '1') },
                     GetDigitPairs("1111"));

            AreEqual(new List<(char, char)> { ('1', '1'), ('1', '2'), ('2', '2'), ('2', '1') },
                     GetDigitPairs("1122"));

            AreEqual(new List<(char, char)> { ('1', '2'), ('2', '3'), ('3', '4'), ('4', '1') },
                     GetDigitPairs("1234"));
        }

        [TestMethod]
        public void TestSumDigits()
        {
            Assert.AreEqual(SumDigits(new List<char> { '1', '2' }), 3);
            Assert.AreEqual(SumDigits(new List<char> { '1', '1', '1', '1' }), 4);
            Assert.AreEqual(SumDigits(new List<char>()), 0);
            Assert.AreEqual(SumDigits(new List<char> { '9' }), 9);
        }
        
        [TestMethod]
        public void TestGetCaptcha()
        {
            Assert.AreEqual(3, CreateCaptcha("1122"));
            Assert.AreEqual(4, CreateCaptcha("1111"));
            Assert.AreEqual(0, CreateCaptcha("1234"));
            Assert.AreEqual(9, CreateCaptcha("91212129"));
        }
    }
}
