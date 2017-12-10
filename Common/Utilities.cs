namespace Common
{
    public static class Utilities
    {   
        /// <summary>
        /// Safely try get an element from a two-dimensional array
        /// </summary>
        public static bool TryGetElement<T>(this T[,] array, int x, int y, out T element) 
        {
            if (x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1)) 
            {
                element = array[x, y];
                return true;
            }
            
            element = default(T);
            return false;
        }
    }
}