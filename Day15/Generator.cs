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
        private readonly int _divisibleBy;

        public Generator(int initialValue, int factor, int divisibleBy=1)
        {
            _value = initialValue;
            _factor = factor;
            _divisibleBy = divisibleBy;
        }

        public int CalculateNext()
        {
            do
            {
                _value *= _factor;
                _value %= Divisor;
            }
            while (_value % _divisibleBy != 0);

            return (int)_value;
        }
    }
}