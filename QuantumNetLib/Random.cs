using static QuantumNetLib.LINQ;

namespace QuantumNetLib
{
    public class Random
    {
        private const long A = 48271; // Multiplier
        private const long C = 0; // Increment
        private const long M = 2147483647; // Modulus (2^31 - 1, a prime number)
        private long _seed;

        public Random(long seed)
        {
            _seed = seed;
        }

        public Random() : this(GenerateSeed())
        {
        }

        private static long GenerateSeed()
        {
            // Create random seed based on current time
            var seed = System.DateTime.Now.Ticks;
            return seed;
        }

        public int Next()
        {
            // Cast to ulong to avoid negative numbers on overflow
            _seed = (A * _seed + C) % M;
            return (int)(_seed & 0x7FFFFFFF); // Mask all but the first bit to ensure a non-negative result
        }

        public int Next(int min, int max)
        {
            return Next() % (max - min + 1) + min;
        }

        public float NextFloat()
        {
            return (float)Next() / M;
        }

        public float NextFloat(float min, float max)
        {
            return NextFloat() * (max - min) + min;
        }

        public double NextDouble()
        {
            // Use Next to get the next random integer and scale it to [0, 1)
            return (double)Next() / (M - 1);
        }

        public double NextDouble(double min, double max)
        {
            return NextDouble() * (max - min) + min;
        }

        public bool NextBool()
        {
            return Next() % 2 == 0;
        }

        public bool NextBool(float probability)
        {
            return NextFloat() < probability;
        }

        public bool NextBool(double probability)
        {
            return NextDouble() < probability;
        }

        public void Shuffle<T>(T[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var j = Next(0, array.Length - 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        public void Shuffle<T>(Vector<T> list)
        {
            for (var i = 0; i < list.Size; i++)
            {
                var j = Next(0, list.Size - 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        public T Choose<T>(T[] array)
        {
            return array[Next(0, array.Length - 1)];
        }

        public T Choose<T>(Vector<T> list)
        {
            return list[Next(0, list.Size - 1)];
        }

        public T Choose<T>(T[] array, float[] probabilities)
        {
            var sum = Sum(probabilities);

            var random = NextFloat(0, sum);
            sum = 0;
            for (var i = 0; i < probabilities.Length; i++)
            {
                sum += probabilities[i];
                if (random < sum) return array[i];
            }

            return array[array.Length - 1];
        }

        public T Choose<T>(Vector<T> list, Vector<float> probabilities)
        {
            float sum = 0;
            for (var i = 0; i < probabilities.Size; i++) sum += probabilities[i];

            var random = NextFloat(0, sum);
            sum = 0;
            for (var i = 0; i < probabilities.Size; i++)
            {
                sum += probabilities[i];
                if (random < sum) return list[i];
            }

            return list[list.Size - 1];
        }

        public T Choose<T>(T[] array, double[] probabilities)
        {
            var sum = Sum(probabilities);

            var random = NextDouble(0, sum);
            sum = 0;
            for (var i = 0; i < probabilities.Length; i++)
            {
                sum += probabilities[i];
                if (random < sum) return array[i];
            }

            return array[array.Length - 1];
        }

        public T Choose<T>(Vector<T> list, Vector<double> probabilities)
        {
            double sum = 0;
            for (var i = 0; i < probabilities.Size; i++) sum += probabilities[i];

            var random = NextDouble(0, sum);
            sum = 0;
            for (var i = 0; i < probabilities.Size; i++)
            {
                sum += probabilities[i];
                if (random < sum) return list[i];
            }

            return list[list.Size - 1];
        }

        public T Choose<T>(T[] array, int[] weights)
        {
            var sum = Sum(weights);

            var random = Next(0, sum);
            sum = 0;
            for (var i = 0; i < weights.Length; i++)
            {
                sum += weights[i];
                if (random < sum) return array[i];
            }

            return array[array.Length - 1];
        }

        public T Choose<T>(Vector<T> list, Vector<int> weights)
        {
            var sum = 0;
            for (var i = 0; i < weights.Size; i++) sum += weights[i];

            var random = Next(0, sum);
            sum = 0;
            for (var i = 0; i < weights.Size; i++)
            {
                sum += weights[i];
                if (random < sum) return list[i];
            }

            return list[list.Size - 1];
        }
        public T GetRandom<T>(T[] array, float[] probabilities)
        {
            var sum = Sum(probabilities);

            var random = NextFloat(0, sum);
            sum = 0;
            for (var i = 0; i < probabilities.Length; i++)
            {
                sum += probabilities[i];
                if (random < sum) return array[i];
            }

            return array[array.Length - 1];
        }

        public float GetRandom(float min, float max)
        {
            return NextFloat() * (max - min) + min;
        }

        public double GetRandom(double min, double max)
        {
            return NextDouble() * (max - min) + min;
        }

        // Method to get a random integer within a range
        public int GetRandom(int min, int max)
        {
            if (max <= min)
                throw new Exception("Maximum value must be greater than minimum value", 20);

            var range = (long)max - min;
            return (int)((Next() % range) + min);
        }

        public bool GetRandomBool()
        {
            return Next() % 2 == 0;
        }

        public bool GetRandomBool(float probability)
        {
            return NextFloat() < probability;
        }

        public bool GetRandomBool(double probability)
        {
            return NextDouble() < probability;
        }

        public T GetRandom<T>(T[] array)
        {
            return array[Next(0, array.Length - 1)];
        }

        public T GetRandom<T>(Vector<T> list)
        {
            return list[Next(0, list.Size - 1)];
        }

        public string GetRandomString(int length)
        {
            var result = "";
            for (var i = 0; i < length; i++) result += (char)Next(32, 126);
            return result;
        }
    }
}
