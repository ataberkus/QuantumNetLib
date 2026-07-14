namespace QuantumNetLib
{
    public struct Vec3 : System.IEquatable<Vec3>
    {
        public Vec3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float Length => QMath.Sqrt(X * X + Y * Y + Z * Z);

        public float LengthSquared => X * X + Y * Y + Z * Z;

        public Vec3 Normalized
        {
            get
            {
                var len = Length;
                if (len == 0f) return new Vec3(0f, 0f, 0f);
                return this / len;
            }
        }

        public static float Dot(Vec3 a, Vec3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            return new Vec3(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X);
        }

        public bool Equals(Vec3 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            return obj is Vec3 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vec3 operator -(Vec3 a)
        {
            return new Vec3(-a.X, -a.Y, -a.Z);
        }

        public static Vec3 operator *(Vec3 a, float b)
        {
            return new Vec3(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vec3 operator *(float a, Vec3 b)
        {
            return new Vec3(a * b.X, a * b.Y, a * b.Z);
        }

        public static Vec3 operator /(Vec3 a, float b)
        {
            return new Vec3(a.X / b, a.Y / b, a.Z / b);
        }

        public static bool operator ==(Vec3 a, Vec3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(Vec3 a, Vec3 b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}
