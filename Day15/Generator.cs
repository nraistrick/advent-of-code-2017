namespace Day15
{
    /// <summary>
    /// Creates a sequence of values based on an initial value
    /// and some multiplication factor
    /// </summary>
    public class Generator
    {
        private const int Divisor = 2147483647;

        private long _value;
        private readonly long _factor;

        public Generator(int initialValue, int factor)
        {
            _value = initialValue;
            _factor = factor;
        }

        public int CalculateNext()
        {
            _value *= _factor;
            _value %= Divisor;

            return (int)_value;
        }
    }
}