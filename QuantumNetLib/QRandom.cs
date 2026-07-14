namespace QuantumNetLib
{
    /// <summary>
    /// Park–Miller LCG. Range methods use the same conventions as <see cref="System.Random"/>:
    /// <c>Next(min, max)</c> is inclusive of <paramref name="min"/> and exclusive of <paramref name="max"/>.
    /// </summary>
    public class QRandom
    {
        private const long A = 48271;
        private const long M = 2147483647; // 2^31 - 1
        private long _seed;

        public QRandom(long seed)
        {
            _seed = NormalizeSeed(seed);
        }

        public QRandom() : this(GenerateSeed())
        {
        }

        private static long GenerateSeed()
        {
            return System.DateTime.UtcNow.Ticks ^ System.Environment.TickCount;
        }

        private static long NormalizeSeed(long seed)
        {
            seed %= M;
            if (seed < 0) seed += M;
            if (seed == 0) seed = 1;
            return seed;
        }

        public int Next()
        {
            _seed = A * _seed % M;
            return (int)_seed;
        }

        /// <summary>Returns a random integer in [<paramref name="min"/>, <paramref name="max"/>).</summary>
        public int Next(int min, int max)
        {
            if (max < min)
                throw new QException("Maximum value must be greater than or equal to minimum value", 20);
            if (max == min) return min;

            var range = (long)max - min;
            return (int)(Next() % range + min);
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
            return (double)Next() / M;
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
            if (array == null) throw new QException("Array cannot be null", 21);
            for (var i = array.Length - 1; i > 0; i--)
            {
                var j = Next(0, i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        public void Shuffle<T>(Vector<T> list)
        {
            if (list == null) throw new QException("List cannot be null", 21);
            for (var i = list.Size - 1; i > 0; i--)
            {
                var j = Next(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        public T Choose<T>(T[] array)
        {
            if (array == null || array.Length == 0)
                throw new QException("Cannot choose from an empty array", 22);
            return array[Next(0, array.Length)];
        }

        public T Choose<T>(Vector<T> list)
        {
            if (list == null || list.Size == 0)
                throw new QException("Cannot choose from an empty list", 22);
            return list[Next(0, list.Size)];
        }

        public T Choose<T>(T[] array, float[] probabilities)
        {
            ValidateWeighted(array, probabilities);
            var sum = QLinq.Sum(probabilities);
            var random = NextFloat(0, sum);
            float cumulative = 0;
            for (var i = 0; i < probabilities.Length; i++)
            {
                cumulative += probabilities[i];
                if (random < cumulative) return array[i];
            }

            return array[array.Length - 1];
        }

        public T Choose<T>(Vector<T> list, Vector<float> probabilities)
        {
            ValidateWeighted(list, probabilities);
            float sum = 0;
            for (var i = 0; i < probabilities.Size; i++) sum += probabilities[i];

            var random = NextFloat(0, sum);
            float cumulative = 0;
            for (var i = 0; i < probabilities.Size; i++)
            {
                cumulative += probabilities[i];
                if (random < cumulative) return list[i];
            }

            return list[list.Size - 1];
        }

        public T Choose<T>(T[] array, double[] probabilities)
        {
            ValidateWeighted(array, probabilities);
            var sum = QLinq.Sum(probabilities);
            var random = NextDouble(0, sum);
            double cumulative = 0;
            for (var i = 0; i < probabilities.Length; i++)
            {
                cumulative += probabilities[i];
                if (random < cumulative) return array[i];
            }

            return array[array.Length - 1];
        }

        public T Choose<T>(Vector<T> list, Vector<double> probabilities)
        {
            ValidateWeighted(list, probabilities);
            double sum = 0;
            for (var i = 0; i < probabilities.Size; i++) sum += probabilities[i];

            var random = NextDouble(0, sum);
            double cumulative = 0;
            for (var i = 0; i < probabilities.Size; i++)
            {
                cumulative += probabilities[i];
                if (random < cumulative) return list[i];
            }

            return list[list.Size - 1];
        }

        public T Choose<T>(T[] array, int[] weights)
        {
            ValidateWeighted(array, weights);
            var sum = QLinq.Sum(weights);
            if (sum <= 0) throw new QException("Weight sum must be positive", 24);

            var random = Next(0, sum);
            var cumulative = 0;
            for (var i = 0; i < weights.Length; i++)
            {
                cumulative += weights[i];
                if (random < cumulative) return array[i];
            }

            return array[array.Length - 1];
        }

        public T Choose<T>(Vector<T> list, Vector<int> weights)
        {
            ValidateWeighted(list, weights);
            var sum = 0;
            for (var i = 0; i < weights.Size; i++) sum += weights[i];
            if (sum <= 0) throw new QException("Weight sum must be positive", 24);

            var random = Next(0, sum);
            var cumulative = 0;
            for (var i = 0; i < weights.Size; i++)
            {
                cumulative += weights[i];
                if (random < cumulative) return list[i];
            }

            return list[list.Size - 1];
        }

        public T GetRandom<T>(T[] array) => Choose(array);

        public T GetRandom<T>(Vector<T> list) => Choose(list);

        public T GetRandom<T>(T[] array, float[] probabilities) => Choose(array, probabilities);

        public float GetRandom(float min, float max) => NextFloat(min, max);

        public double GetRandom(double min, double max) => NextDouble(min, max);

        public int GetRandom(int min, int max) => Next(min, max);

        public bool GetRandomBool() => NextBool();

        public bool GetRandomBool(float probability) => NextBool(probability);

        public bool GetRandomBool(double probability) => NextBool(probability);

        public string GetRandomString(int length)
        {
            if (length < 0) throw new QException("Length must be non-negative", 25);

            var chars = new char[length];
            for (var i = 0; i < length; i++) chars[i] = (char)Next(32, 127);
            return new string(chars);
        }

        private static void ValidateWeighted<T, TWeight>(T[] array, TWeight[] weights)
        {
            if (array == null || array.Length == 0)
                throw new QException("Cannot choose from an empty array", 22);
            if (weights == null || weights.Length != array.Length)
                throw new QException("Weights length must match array length", 23);
        }

        private static void ValidateWeighted<T, TWeight>(Vector<T> list, Vector<TWeight> weights)
        {
            if (list == null || list.Size == 0)
                throw new QException("Cannot choose from an empty list", 22);
            if (weights == null || weights.Size != list.Size)
                throw new QException("Weights length must match list size", 23);
        }
    }
}
