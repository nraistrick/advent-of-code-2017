using System;
using System.Collections.Generic;
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
            Assert.AreEqual("##./#../...",         loadedRules["../.#"]);
            Assert.AreEqual("#..#/..../..../#..#", loadedRules[".#./..#/###"]);

            // Rotation required
            Assert.AreEqual("##./#../...",         loadedRules["../#."]);
            Assert.AreEqual("##./#../...",         loadedRules["#./.."]);
            Assert.AreEqual("##./#../...",         loadedRules[".#/.."]);
            Assert.AreEqual("#..#/..../..../#..#", loadedRules["#../#.#/##."]);
            Assert.AreEqual("#..#/..../..../#..#", loadedRules["###/#../.#."]);
            Assert.AreEqual("#..#/..../..../#..#", loadedRules[".##/#.#/..#"]);

            // Flip required
            Assert.AreEqual("#..#/..../..../#..#", loadedRules[".#./#../###"]);
            Assert.AreEqual("#..#/..../..../#..#", loadedRules["###/..#/.#."]);

            // Should not match
            Assert.ThrowsException<KeyNotFoundException>(() => loadedRules["###/#../#.."]);
            Assert.ThrowsException<KeyNotFoundException>(() => loadedRules["###/#../#.#"]);
            Assert.ThrowsException<KeyNotFoundException>(() => loadedRules["###/.../..."]);
        }

        [TestMethod]
        public void TestExpandArt()
        {
            var ruleSet = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var loadedRules = Program.LoadRules(ruleSet);
            CollectionAssert.AreEqual(Program.Delinearize("##./#../..."),
                Program.ExpandArt(new [,] {{'.', '.'},
                                           {'.', '#'}}, loadedRules));

            CollectionAssert.AreEqual(Program.Delinearize("#..#/..../..../#..#"),
                Program.ExpandArt(new [,] {{'.', '#', '.'},
                                           {'.', '.', '#'},
                                           {'#', '#', '#'}}, loadedRules));

            CollectionAssert.AreEqual(Program.Delinearize("##.##./#..#../....../##.##./#..#../......"),
                Program.ExpandArt(new [,] {{'.', '.', '.', '.'},
                                           {'.', '#', '.', '#'},
                                           {'.', '.', '.', '.'},
                                           {'.', '#', '.', '#'}}, loadedRules));
        }

        [TestMethod]
        public void TestNumberOfLightsOn()
        {
            var ruleSet = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var loadedRules = Program.LoadRules(ruleSet);
            var image = Program.ExpandArt(Program.Input, loadedRules, 2);
            Assert.AreEqual(12, Program.CountOnPixels(image));
        }
    }
}