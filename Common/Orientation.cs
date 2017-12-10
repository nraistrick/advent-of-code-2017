using System;

namespace Common
{
    public enum Direction { Up, Right, Down, Left }
    
    /// <summary>
    /// Manages turning left and right to change direction
    /// </summary>
    public class Orientation
    {
        public Direction Current;

        public Orientation(Direction starting)
        {
            Current = starting;
        }

        public Direction TurnLeft()
        {
            switch (Current)
            {
                case Direction.Up:
                    Current = Direction.Left;
                    break;
                case Direction.Right:
                    Current = Direction.Up;
                    break;
                case Direction.Down:
                    Current = Direction.Right;
                    break;
                case Direction.Left:
                    Current = Direction.Down;
                    break;
                default:
                    var error = $"Currently not pointing in a valid direction: {Current}";
                    throw new InvalidOperationException(error);
            }

            return Current;
        }
        
        public Direction TurnRight()
        {
            switch (Current)
            {
                case Direction.Up:
                    Current = Direction.Right;
                    break;
                case Direction.Right:
                    Current = Direction.Down;
                    break;
                case Direction.Down:
                    Current = Direction.Left;
                    break;
                case Direction.Left:
                    Current = Direction.Up;
                    break;
                default:
                    var error = $"Currently not pointing in a valid direction: {Current}";
                    throw new InvalidOperationException(error);
            }

            return Current;
        }
    }
}