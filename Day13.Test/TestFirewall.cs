using System;
using System.Collections.Generic;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day13.Test
{
    [TestClass]
    public class TestFirewall
    {
        [TestMethod]
        public void TestCreateLayers()
        {
            var inputData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var layers = Program.CreateLayers(inputData);

            Assert.AreEqual((2, 0), (layers[0].Range, layers[0].ScannerLocation));
            Assert.AreEqual((1, 0), (layers[1].Range, layers[1].ScannerLocation));
            Assert.AreEqual((3, 0), (layers[4].Range, layers[4].ScannerLocation));
            Assert.AreEqual((3, 0), (layers[6].Range, layers[6].ScannerLocation));
        }

        [TestMethod]
        public void TestUpdateScannerLocations()
        {
            var inputData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var layers = Program.CreateLayers(inputData);

            Program.UpdateScannerLocations(layers);
            Assert.AreEqual((2, 1), (layers[0].Range, layers[0].ScannerLocation));
            Assert.AreEqual((1, 1), (layers[1].Range, layers[1].ScannerLocation));
            Assert.AreEqual((3, 1), (layers[4].Range, layers[4].ScannerLocation));
            Assert.AreEqual((3, 1), (layers[6].Range, layers[6].ScannerLocation));

            Program.UpdateScannerLocations(layers);
            Assert.AreEqual((2, 2), (layers[0].Range, layers[0].ScannerLocation));
            Assert.AreEqual((1, 0), (layers[1].Range, layers[1].ScannerLocation));
            Assert.AreEqual((3, 2), (layers[4].Range, layers[4].ScannerLocation));
            Assert.AreEqual((3, 2), (layers[6].Range, layers[6].ScannerLocation));

            Program.UpdateScannerLocations(layers);
            Assert.AreEqual((2, 3), (layers[0].Range, layers[0].ScannerLocation));
            Assert.AreEqual((1, 1), (layers[1].Range, layers[1].ScannerLocation));
            Assert.AreEqual((3, 3), (layers[4].Range, layers[4].ScannerLocation));
            Assert.AreEqual((3, 3), (layers[6].Range, layers[6].ScannerLocation));

            Program.UpdateScannerLocations(layers);
            Assert.AreEqual((2, 0), (layers[0].Range, layers[0].ScannerLocation));
            Assert.AreEqual((1, 0), (layers[1].Range, layers[1].ScannerLocation));
            Assert.AreEqual((3, 4), (layers[4].Range, layers[4].ScannerLocation));
            Assert.AreEqual((3, 4), (layers[6].Range, layers[6].ScannerLocation));
        }

        [TestMethod]
        public void TestCalculateSeverity()
        {
            var inputData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
            var layers = Program.CreateLayers(inputData);
            Assert.AreEqual(24, Program.CalculateSeverity(layers, out var caught));
        }

        [TestMethod]
        public void TestFindDelayToAvoidBeingCaught()
        {
             var inputData = Testing.GetTestFileContents("TestInput.txt").Split(Environment.NewLine);
             var layers = Program.CreateLayers(inputData);
             Assert.AreEqual(10, Program.CalculateDelayToAvoidBeingCaught(inputData));
        }
    }
}