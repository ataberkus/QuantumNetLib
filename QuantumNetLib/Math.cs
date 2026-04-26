namespace QuantumNetLib
{
    public class Math
    {
        public static float PI = 3.14159265359f;
        public static float E = 2.71828182846f;

        public static float Abs(float a)
        {
            return a < 0 ? -a : a;
        }

        public static float Sqrt(float a)
        {
            return (float)System.Math.Sqrt(a);
        }

        public static float Pow(float a, float b)
        {
            return (float)System.Math.Pow(a, b);
        }

        public static float Sin(float a)
        {
            float result = 0;
            for (var i = 0; i < 10; i++) result += Pow(-1, i) * Pow(a, 2 * i + 1) / Factorial(2 * i + 1);
            return result;
        }

        public static float Cos(float a)
        {
            float result = 0;
            for (var i = 0; i < 10; i++) result += Pow(-1, i) * Pow(a, 2 * i) / Factorial(2 * i);
            return result;
        }

        public static float Tan(float a)
        {
            return Sin(a) / Cos(a);
        }

        public static float Factorial(float a)
        {
            float result = 1;
            for (var i = 1; i <= a; i++) result *= i;
            return result;
        }

        public static float Log(float a)
        {
            float result = 0;
            for (var i = 1; i < 100; i++) result += Pow(-1, i + 1) * Pow(a - 1, i) / i;
            return result;
        }

        public static float Log(float a, float b)
        {
            return Log(a) / Log(b);
        }

        public static float Log10(float a)
        {
            return Log(a) / Log(10);
        }

        public static float Log2(float a)
        {
            return Log(a) / Log(2);
        }

        public static float Min(float a, float b)
        {
            return a < b ? a : b;
        }

        public static float Max(float a, float b)
        {
            return a > b ? a : b;
        }

        public static float Clamp(float a, float min, float max)
        {
            return a < min ? min : a > max ? max : a;
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static float Map(float a, float min1, float max1, float min2, float max2)
        {
            return (a - min1) / (max1 - min1) * (max2 - min2) + min2;
        }

        public static float Random(float min, float max)
        {
            var random = new Random();
            return random.NextFloat(min, max);
        }

        public static float Random(float max)
        {
            return Random(0, max);
        }
    }
}
