namespace QuantumNetLib
{
    public struct Vec2 : System.IEquatable<Vec2>
    {
        public Vec2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public static Vec2 Zero => new Vec2(0f, 0f);
        public static Vec2 One => new Vec2(1f, 1f);
        public static Vec2 UnitX => new Vec2(1f, 0f);
        public static Vec2 UnitY => new Vec2(0f, 1f);

        public float Length => QMath.Sqrt(X * X + Y * Y);

        public float LengthSquared => X * X + Y * Y;

        public Vec2 Normalized
        {
            get
            {
                var len = Length;
                if (len == 0f) return new Vec2(0f, 0f);
                return this / len;
            }
        }

        public static float Dot(Vec2 a, Vec2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        /// <summary>2D cross product (signed parallelogram area).</summary>
        public static float Cross(Vec2 a, Vec2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static float Distance(Vec2 a, Vec2 b)
        {
            return (a - b).Length;
        }

        public bool Equals(Vec2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            return obj is Vec2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X + b.X, a.Y + b.Y);
        }

        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X - b.X, a.Y - b.Y);
        }

        public static Vec2 operator -(Vec2 a)
        {
            return new Vec2(-a.X, -a.Y);
        }

        public static Vec2 operator *(Vec2 a, float b)
        {
            return new Vec2(a.X * b, a.Y * b);
        }

        public static Vec2 operator *(float a, Vec2 b)
        {
            return new Vec2(a * b.X, a * b.Y);
        }

        public static Vec2 operator /(Vec2 a, float b)
        {
            return new Vec2(a.X / b, a.Y / b);
        }

        public static bool operator ==(Vec2 a, Vec2 b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Vec2 a, Vec2 b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
