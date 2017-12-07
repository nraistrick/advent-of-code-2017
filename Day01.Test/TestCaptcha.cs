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
                     GetNeighbouringDigitPairs("1111"));

            AreEqual(new List<(char, char)> { ('1', '1'), ('1', '2'), ('2', '2'), ('2', '1') },
                     GetNeighbouringDigitPairs("1122"));

            AreEqual(new List<(char, char)> { ('1', '2'), ('2', '3'), ('3', '4'), ('4', '1') },
                     GetNeighbouringDigitPairs("1234"));
        }

        [TestMethod]
        public void TestGetHalfwayDigitPairs()
        {
            AreEqual(new List<(char, char)> { ('1', '1'), ('1', '1'), ('1', '1'), ('1', '1') },
                     GetHalfwayDigitPairs("1111"));
            
            AreEqual(new List<(char, char)> { ('1', '2'), ('1', '2'), ('2', '1'), ('2', '1') },
                     GetHalfwayDigitPairs("1122"));

            AreEqual(new List<(char, char)> { ('1', '3'), ('2', '4'), ('3', '1'), ('4', '2') },
                     GetHalfwayDigitPairs("1234"));
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

        [TestMethod]
        public void TestGetAlternativeCaptcha()
        {
            Assert.AreEqual(6,  CreateAlternativeCaptcha("1212"));
            Assert.AreEqual(0,  CreateAlternativeCaptcha("1221"));
            Assert.AreEqual(4,  CreateAlternativeCaptcha("123425"));
            Assert.AreEqual(12, CreateAlternativeCaptcha("123123"));
            Assert.AreEqual(4,  CreateAlternativeCaptcha("12131415"));
        }
    }
}
