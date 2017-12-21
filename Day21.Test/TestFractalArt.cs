using System;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day21.Test
{
    [TestClass]
    public class TestFractalArt
    {
        private const string LinearizedInput = ".#./..#/###";

        [TestMethod]
        public void TestInputLinearization()
        {
            Assert.AreEqual(LinearizedInput, Program.Linearize(Program.Input));
        }

        [TestMethod]
        public void TestInputDelinearization()
        {
            var delinearized = Program.Delinearize(LinearizedInput);
            CollectionAssert.AreEqual(Program.Input, delinearized);

            delinearized = Program.Delinearize("#./.#");
            var expected = new [,] {{'#', '.'},
                                    {'.', '#'}};
            CollectionAssert.AreEqual(expected, delinearized);

            delinearized = Program.Delinearize("##./#../...");
            expected = new [,] {{'#', '#', '.'},
                                {'#', '.', '.'},
                                {'.', '.', '.'}};
            CollectionAssert.AreEqual(expected, delinearized);
        }

        [TestMethod]
        public void TestRotateImage()
        {
            var image = ".#/..";
            image = Program.Rotate(image);
            Assert.AreEqual("../.#", image);

            image = Program.Rotate(image);
            Assert.AreEqual("../#.", image);

            image = Program.Rotate(image);
            Assert.AreEqual("#./..", image);

            image = Program.Rotate(image);
            Assert.AreEqual(".#/..", image);

            image = LinearizedInput;
            image = Program.Rotate(image);
            Assert.AreEqual("#../#.#/##.", image);

            image = Program.Rotate(image);
            Assert.AreEqual("###/#../.#.", image);

            image = Program.Rotate(image);
            Assert.AreEqual(".##/#.#/..#", image);

            image = Program.Rotate(image);
            Assert.AreEqual(LinearizedInput, image);

            image = "#..#/..../..../....";
            image = Program.Rotate(image);
            Assert.AreEqual("...#/..../..../...#", image);

            image = Program.Rotate(image);
            Assert.AreEqual("..../..../..../#..#", image);

            image = Program.Rotate(image);
            Assert.AreEqual("#.../..../..../#...", image);

            image = Program.Rotate(image);
            Assert.AreEqual("#..#/..../..../....", image);
        }

        [TestMethod]
        public void TestFlipImage()
        {
            Assert.AreEqual("../#.", Program.Flip("#./.."));
            Assert.AreEqual("../.#", Program.Flip(".#/.."));
            Assert.AreEqual("#./..", Program.Flip("../#."));
            Assert.AreEqual(".#/..", Program.Flip("../.#"));

            Assert.AreEqual("###/..#/.#.", Program.Flip(LinearizedInput));
            Assert.AreEqual(".../..#/.#.", Program.Flip(".#./..#/..."));

            Assert.AreEqual("#.../...#/.#../....", Program.Flip("..../.#../...#/#..."));
        }

        [TestMethod]
        public void TestLoadRules()
        {
            var ruleSet = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var loadedRules = Program.LoadRules(ruleSet);
            Assert.AreEqual("##./#../...", loadedRules["../.#"]);
            Assert.AreEqual("#..#/..../..../#..#", loadedRules[".#./..#/###"]);
        }

        [TestMethod]
        public void TestFindMatchingRule()
        {
            var ruleSet = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var loadedRules = Program.LoadRules(ruleSet);

            // No rotation required
            Assert.AreEqual("##./#../...", Program.FindMatchingRule("../.#", loadedRules));
            Assert.AreEqual("#..#/..../..../#..#", Program.FindMatchingRule(".#./..#/###", loadedRules));

            // Rotation required
            Assert.AreEqual("##./#../...", Program.FindMatchingRule("../#.", loadedRules));
            Assert.AreEqual("##./#../...", Program.FindMatchingRule("#./..", loadedRules));
            Assert.AreEqual("##./#../...", Program.FindMatchingRule(".#/..", loadedRules));
            Assert.AreEqual("#..#/..../..../#..#", Program.FindMatchingRule("#../#.#/##.", loadedRules));
            Assert.AreEqual("#..#/..../..../#..#", Program.FindMatchingRule("###/#../.#.", loadedRules));
            Assert.AreEqual("#..#/..../..../#..#", Program.FindMatchingRule(".##/#.#/..#", loadedRules));

            // Flip required
            Assert.AreEqual("#..#/..../..../#..#", Program.FindMatchingRule(".#./#../###", loadedRules));
            Assert.AreEqual("#..#/..../..../#..#", Program.FindMatchingRule("###/..#/.#.", loadedRules));

            // Should not match
            Assert.ThrowsException<InvalidOperationException>(
                () => Program.FindMatchingRule("###/#../#..", loadedRules));
            Assert.ThrowsException<InvalidOperationException>(
                () => Program.FindMatchingRule("###/#../#.#", loadedRules));
            Assert.ThrowsException<InvalidOperationException>(
                () => Program.FindMatchingRule("###/.../...", loadedRules));
        }

        [TestMethod]
        public void TestExpandArt()
        {
            var ruleSet = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var loadedRules = Program.LoadRules(ruleSet);
            Assert.AreEqual("##./#../...", Program.ExpandArt("../.#", loadedRules));
            Assert.AreEqual("#..#/..../..../#..#", Program.ExpandArt(".#./..#/###/", loadedRules));
            Assert.AreEqual("##.##./#..#../....../##.##./#..#../......", Program.ExpandArt("..../.#.#/..../.#.#", loadedRules));
        }

        [TestMethod]
        public void TestNumberOfLightsOn()
        {
            var ruleSet = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var loadedRules = Program.LoadRules(ruleSet);
            var linearizedInput = Program.Linearize(Program.Input);
            var image = Program.ExpandArt(linearizedInput, loadedRules, 2);
            Assert.AreEqual("##.##./#..#../....../##.##./#..#../......", image);
            Assert.AreEqual(12, image.Count(c => c == '#'));
        }
    }
}