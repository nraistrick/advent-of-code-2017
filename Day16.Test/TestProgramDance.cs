using System.Collections.Generic;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day16.Test
{
    [TestClass]
    public class TestProgramDance
    {
        [TestMethod]
        public void TestSpin()
        {
            Program.Programs = "abcde".ToCharArray().ToList();

            Program.Spin(1);
            Assert.AreEqual("eabcd", string.Join("", Program.Programs));

            Program.Spin(3);
            Assert.AreEqual("bcdea", string.Join("", Program.Programs));

            Program.Spin(5);
            Assert.AreEqual("bcdea", string.Join("", Program.Programs));
        }

        [TestMethod]
        public void TestExchange()
        {
            Program.Programs = "abcde".ToCharArray().ToList();

            Program.Exchange(3, 4);
            Assert.AreEqual("abced", string.Join("", Program.Programs));

            Program.Exchange(0, 4);
            Assert.AreEqual("dbcea", string.Join("", Program.Programs));

            Program.Exchange(4, 2);
            Assert.AreEqual("dbaec", string.Join("", Program.Programs));
        }

        [TestMethod]
        public void TestSwap()
        {
            Program.Programs = "abcde".ToCharArray().ToList();
            
            Program.Partner('b', 'e');
            Assert.AreEqual("aecdb", string.Join("", Program.Programs));

            Program.Partner('b', 'c');
            Assert.AreEqual("aebdc", string.Join("", Program.Programs));

            Program.Partner('e', 'a');
            Assert.AreEqual("eabdc", string.Join("", Program.Programs));
        }

        [TestMethod]
        public void TestExecuteInstruction()
        {
            Program.Programs = "abcde".ToCharArray().ToList();

            Program.ExecuteInstruction("s1");
            Assert.AreEqual("eabcd", string.Join("", Program.Programs));

            Program.ExecuteInstruction("x3/4");
            Assert.AreEqual("eabdc", string.Join("", Program.Programs));

            Program.ExecuteInstruction("pe/b");
            Assert.AreEqual("baedc", string.Join("", Program.Programs));

            Program.Programs = "abcdefghijkl".ToCharArray().ToList();

            Program.ExecuteInstruction("x11/10");
            Assert.AreEqual("abcdefghijlk", string.Join("", Program.Programs));

            Program.ExecuteInstruction("x0/10");
            Assert.AreEqual("lbcdefghijak", string.Join("", Program.Programs));

            Program.ExecuteInstruction("s12");
            Assert.AreEqual("lbcdefghijak", string.Join("", Program.Programs));
        }

        [TestMethod]
        public void TestExecuteInstructions()
        {
            Program.Programs = "abcde".ToCharArray().ToList();
            var instructions = Testing.GetTestFileContents("TestInput.txt").Split(",");
            Program.ExecuteInstructions(instructions);

            Assert.AreEqual("baedc", string.Join("", Program.Programs));
        }
    }
}