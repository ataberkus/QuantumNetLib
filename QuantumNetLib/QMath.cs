namespace QuantumNetLib
{
    public static class QMath
    {
        public const float PI = 3.14159265359f;
        public const float E = 2.71828182846f;

        public static float Abs(float a) => (float)System.Math.Abs(a);

        public static float Sqrt(float a)
        {
            if (a < 0f) throw new QException("Sqrt is undefined for negative numbers", 32);
            return (float)System.Math.Sqrt(a);
        }

        public static float Pow(float a, float b) => (float)System.Math.Pow(a, b);

        public static float Sin(float a) => (float)System.Math.Sin(a);

        public static float Cos(float a) => (float)System.Math.Cos(a);

        public static float Tan(float a) => (float)System.Math.Tan(a);

        public static long Factorial(int n)
        {
            if (n < 0) throw new QException("Factorial is undefined for negative numbers", 30);
            if (n > 20) throw new QException("Factorial overflow for values greater than 20", 31);

            long result = 1;
            for (var i = 2; i <= n; i++) result *= i;
            return result;
        }

        public static float Log(float a)
        {
            if (a <= 0f) throw new QException("Log is undefined for non-positive numbers", 33);
            return (float)System.Math.Log(a);
        }

        public static float Log(float a, float b)
        {
            if (a <= 0f) throw new QException("Log is undefined for non-positive numbers", 33);
            if (b <= 0f || b == 1f) throw new QException("Log base must be positive and not equal to 1", 34);
            return (float)System.Math.Log(a, b);
        }

        public static float Log10(float a)
        {
            if (a <= 0f) throw new QException("Log is undefined for non-positive numbers", 33);
            return (float)System.Math.Log10(a);
        }

        public static float Log2(float a)
        {
            if (a <= 0f) throw new QException("Log is undefined for non-positive numbers", 33);
            return (float)System.Math.Log(a, 2);
        }

        public static float Min(float a, float b) => a < b ? a : b;

        public static float Max(float a, float b) => a > b ? a : b;

        public static float Clamp(float a, float min, float max)
        {
            if (min > max) throw new QException("min must be less than or equal to max", 35);
            return a < min ? min : a > max ? max : a;
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static float Map(float a, float min1, float max1, float min2, float max2)
        {
            if (max1 == min1) throw new QException("Source range must be non-empty", 36);
            return (a - min1) / (max1 - min1) * (max2 - min2) + min2;
        }

        private static readonly QRandom SharedRandom = new QRandom();
        private static readonly object SharedRandomLock = new object();

        public static float Random(float min, float max)
        {
            if (max < min) throw new QException("max must be greater than or equal to min", 20);
            lock (SharedRandomLock)
            {
                return SharedRandom.NextFloat(min, max);
            }
        }

        public static float Random(float max)
        {
            return Random(0, max);
        }
    }
}
