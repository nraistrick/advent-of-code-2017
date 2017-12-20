using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    /// <summary>
    /// Finds out information about a packet being caught by security
    /// scanners in a firewall
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            var inputData = File.ReadAllLines("Input.txt");
            var layers = CreateLayers(inputData);
            var severity = CalculateSeverity(layers, out var unused);
            Console.WriteLine($"The calculated severity is: {severity}");

            var delayToAvoidBeingCaught = CalculateDelayToAvoidBeingCaught(inputData);
            Console.WriteLine($"The delay required to avoid being caught is: {delayToAvoidBeingCaught}");
        }

        public static int CalculateDelayToAvoidBeingCaught(IEnumerable<string> inputData)
        {
            var delay = 0;
            bool isCaught;
            var layers = CreateLayers(inputData);

            do
            {
                delay++;
                UpdateScannerLocations(layers);
                var clonedLayer = layers.ToDictionary(entry => entry.Key, entry
                    => new Layer(entry.Value.Range, entry.Value.ScannerLocation));
                CalculateSeverity(clonedLayer, out isCaught, true);
            }
            while (isCaught);

            return delay;
        }

        public static int CalculateSeverity(Dictionary<int, Layer> layers, out bool caught, bool breakOnCapture=false)
        {
            var maxLevel = layers.Keys.Max();
            var severity = 0;
            var picoseconds = 0;
            caught = false;

            while (true)
            {
                var layerExists = layers.TryGetValue(picoseconds, out var layer);
                if (layerExists && layer.ScannerLocation == 0)
                {
                    severity += picoseconds * (layer.Range + 1);
                    caught = true;
                    if (breakOnCapture) break;
                }

                UpdateScannerLocations(layers);

                if (++picoseconds > maxLevel) break;
            }

            return severity;
        }



        public static void UpdateScannerLocations(Dictionary<int, Layer> layers)
        {
            foreach (var key in layers.Keys.ToArray())
            {
                var range = layers[key].Range;
                var location = layers[key].ScannerLocation;

                location = ++location % (2 * range);

                layers[key].ScannerLocation = location;
            }
        }

        public static Dictionary<int, Layer> CreateLayers(IEnumerable<string> inputData)
        {
            var layers = new Dictionary<int, Layer>();

            foreach (var line in inputData)
            {
                var input = line.Split(": ");
                var (layer, range) = (int.Parse(input[0]), int.Parse(input[1]));
                layers.Add(layer, new Layer(range - 1, 0));
            }

            return layers;
        }
    }
}