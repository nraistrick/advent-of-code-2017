namespace Day13
{
    /// <summary>
    /// Represents a security layer in a firewall
    /// </summary>
    public class Layer
    {
        public readonly int Range;
        public int ScannerLocation;

        public Layer(int range, int scannerLocation)
        {
            Range = range;
            ScannerLocation = scannerLocation;
        }
    }
}