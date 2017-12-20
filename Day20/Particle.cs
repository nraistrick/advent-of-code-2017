using System;

namespace Day20
{
    /// <summary>
    /// Models the movement of a particle
    /// </summary>
    public class Particle
    {
        public (int X, int Y, int Z) Position;
        public (int X, int Y, int Z) Velocity;
        public (int X, int Y, int Z) Acceleration;

        public int OriginDistance;

        public Particle((int x, int y, int z) position,
                        (int x, int y, int z) velocity,
                        (int x, int y, int z) acceleration)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;

            OriginDistance = CalculateOriginDistance();
        }

        public void Move()
        {
            Velocity.X += Acceleration.X;
            Velocity.Y += Acceleration.Y;
            Velocity.Z += Acceleration.Z;

            Position.X += Velocity.X;
            Position.Y += Velocity.Y;
            Position.Z += Velocity.Z;

            OriginDistance = CalculateOriginDistance();
        }

        public int CalculateOriginDistance()
        {
            return Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);
        }
    }
}