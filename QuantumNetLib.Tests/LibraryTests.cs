using System;
using Xunit;

namespace QuantumNetLib.Tests
{
    public class VectorTests
    {
        [Fact]
        public void PushBack_GrowsAndPreservesElements()
        {
            var vector = new Vector<int>();
            for (var i = 0; i < 10; i++) vector.PushBack(i);

            Assert.Equal(10, vector.Size);
            Assert.True(vector.Capacity >= 10);
            Assert.Equal(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, vector.ToArray());
        }

        [Fact]
        public void Indexer_OutOfRange_Throws()
        {
            var vector = new Vector<int>();
            vector.PushBack(1);

            Assert.Throws<QException>(() => _ = vector[-1]);
            Assert.Throws<QException>(() => _ = vector[1]);
            Assert.Throws<QException>(() => vector[1] = 2);
        }

        [Fact]
        public void Insert_And_Erase_UpdateContents()
        {
            var vector = new Vector<int>();
            vector.PushBack(1);
            vector.PushBack(3);
            vector.Insert(1, 2);
            Assert.Equal(new[] { 1, 2, 3 }, vector.ToArray());

            vector.Erase(1);
            Assert.Equal(new[] { 1, 3 }, vector.ToArray());
        }

        [Fact]
        public void PopBack_OnEmpty_Throws()
        {
            var vector = new Vector<int>();
            Assert.Throws<QException>(() => vector.PopBack());
        }

        [Fact]
        public void Clone_IsIndependentCopy()
        {
            var vector = new Vector<int>();
            vector.PushBack(1);
            vector.PushBack(2);

            var clone = vector.Clone();
            clone[0] = 99;

            Assert.Equal(1, vector[0]);
            Assert.Equal(99, clone[0]);
        }
    }

    public class QMathTests
    {
        [Fact]
        public void TrigAndLog_MatchSystemMath()
        {
            Assert.Equal((float)Math.Sin(2.5f), QMath.Sin(2.5f), 5);
            Assert.Equal((float)Math.Cos(2.5f), QMath.Cos(2.5f), 5);
            Assert.Equal((float)Math.Log(10f), QMath.Log(10f), 5);
            Assert.Equal((float)Math.Log10(100f), QMath.Log10(100f), 5);
        }

        [Fact]
        public void Factorial_ComputesAndGuardsBounds()
        {
            Assert.Equal(120, QMath.Factorial(5));
            Assert.Throws<QException>(() => QMath.Factorial(-1));
            Assert.Throws<QException>(() => QMath.Factorial(21));
        }

        [Fact]
        public void Clamp_And_Lerp_Work()
        {
            Assert.Equal(5f, QMath.Clamp(10f, 0f, 5f));
            Assert.Equal(0f, QMath.Clamp(-1f, 0f, 5f));
            Assert.Equal(5f, QMath.Lerp(0f, 10f, 0.5f));
        }
    }

    public class QRandomTests
    {
        [Fact]
        public void ZeroSeed_DoesNotProduceOnlyZeros()
        {
            var random = new QRandom(0);
            Assert.NotEqual(0, random.Next());
            Assert.NotEqual(0, random.Next());
        }

        [Fact]
        public void Next_RangeIsExclusiveMax()
        {
            var random = new QRandom(123);
            for (var i = 0; i < 200; i++)
            {
                var value = random.Next(5, 10);
                Assert.InRange(value, 5, 9);
            }
        }

        [Fact]
        public void Shuffle_PreservesElements()
        {
            var random = new QRandom(42);
            var array = new[] { 1, 2, 3, 4, 5 };
            random.Shuffle(array);

            Array.Sort(array);
            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, array);
        }

        [Fact]
        public void Choose_Weighted_RespectsWeights()
        {
            var random = new QRandom(7);
            var counts = new int[2];
            var options = new[] { "a", "b" };
            var weights = new[] { 9, 1 };

            for (var i = 0; i < 1000; i++)
            {
                var choice = random.Choose(options, weights);
                counts[choice == "a" ? 0 : 1]++;
            }

            Assert.True(counts[0] > counts[1]);
        }
    }

    public class QLinqTests
    {
        [Fact]
        public void First_ThrowsWhenNoMatch()
        {
            var numbers = new[] { 1, 2, 3 };
            Assert.Throws<QException>(() => QLinq.First(numbers, x => x > 10));
            Assert.Equal(2, QLinq.FirstOrDefault(numbers, x => x == 2));
            Assert.Equal(0, QLinq.FirstOrDefault(numbers, x => x > 10));
        }

        [Fact]
        public void Last_ThrowsWhenEmpty()
        {
            Assert.Throws<QException>(() => QLinq.Last(Array.Empty<int>()));
            Assert.Equal(0, QLinq.LastOrDefault(Array.Empty<int>()));
        }

        [Fact]
        public void Select_CanChangeType()
        {
            var numbers = new[] { 1, 2, 3 };
            var text = QLinq.Select(numbers, x => x.ToString());
            Assert.Equal(new[] { "1", "2", "3" }, text);
        }

        [Fact]
        public void Where_And_Concat_Work()
        {
            var numbers = new[] { 1, 2, 3, 4 };
            Assert.Equal(new[] { 2, 4 }, QLinq.Where(numbers, x => x % 2 == 0));
            Assert.Equal(new[] { 1, 2, 3, 4 }, QLinq.Concat(new[] { 1, 2 }, new[] { 3, 4 }));
        }
    }

    public class VecTests
    {
        [Fact]
        public void Vec2_LengthNormalizeAndDot()
        {
            var a = new Vec2(3, 4);
            Assert.Equal(5f, a.Length, 5);
            Assert.Equal(1f, a.Normalized.Length, 5);
            Assert.Equal(11f, Vec2.Dot(a, new Vec2(1, 2)), 5);
            Assert.Equal(-2f, Vec2.Cross(new Vec2(1, 2), new Vec2(3, 4)), 5);
        }

        [Fact]
        public void Vec3_CrossProduct()
        {
            var result = Vec3.Cross(new Vec3(1, 0, 0), new Vec3(0, 1, 0));
            Assert.Equal(new Vec3(0, 0, 1), result);
            Assert.Equal(1f, new Vec3(0, 0, 1).Length, 5);
        }
    }

    public class QMathGuardTests
    {
        [Fact]
        public void Sqrt_Negative_Throws()
        {
            Assert.Throws<QException>(() => QMath.Sqrt(-1f));
            Assert.Equal(3f, QMath.Sqrt(9f));
        }

        [Fact]
        public void Log_NonPositive_Throws()
        {
            Assert.Throws<QException>(() => QMath.Log(0f));
            Assert.Throws<QException>(() => QMath.Log(-2f));
            Assert.Throws<QException>(() => QMath.Log10(0f));
            Assert.Throws<QException>(() => QMath.Log2(-1f));
        }

        [Fact]
        public void Log_BadBase_Throws()
        {
            Assert.Throws<QException>(() => QMath.Log(10f, 1f));
            Assert.Throws<QException>(() => QMath.Log(10f, 0f));
            Assert.Equal(2f, QMath.Log(100f, 10f), 4);
        }

        [Fact]
        public void Clamp_MinGreaterMax_Throws()
        {
            Assert.Throws<QException>(() => QMath.Clamp(0f, 1f, 0f));
            Assert.Equal(1f, QMath.Clamp(5f, 0f, 1f));
        }

        [Fact]
        public void Map_EmptyRange_Throws()
        {
            Assert.Throws<QException>(() => QMath.Map(1f, 2f, 2f, 0f, 1f));
            Assert.Equal(50f, QMath.Map(0.5f, 0f, 1f, 0f, 100f));
        }

        [Fact]
        public void Random_StaysInRange()
        {
            for (var i = 0; i < 100; i++)
            {
                var value = QMath.Random(2f, 5f);
                Assert.InRange(value, 2f, 5f);
            }
            Assert.Throws<QException>(() => QMath.Random(5f, 2f));
        }
    }

    public class QRandomGuardTests
    {
        [Fact]
        public void SameSeed_SameSequence()
        {
            var a = new QRandom(12345);
            var b = new QRandom(12345);
            Assert.Equal(a.Next(), b.Next());
            Assert.Equal(a.Next(), b.Next());
        }

        [Fact]
        public void RangedMethods_ValidateBounds()
        {
            var random = new QRandom(7);
            Assert.Throws<QException>(() => random.NextFloat(5f, 2f));
            Assert.Throws<QException>(() => random.NextDouble(5.0, 2.0));
            Assert.Throws<QException>(() => random.NextBool(2f));
            Assert.Throws<QException>(() => random.NextBool(-0.5));
            Assert.Throws<QException>(() => random.GetRandomString(-1));
        }

        [Fact]
        public void Choose_RejectsBadWeights()
        {
            var random = new QRandom(9);
            Assert.Throws<QException>(() => random.Choose(new[] { "a", "b" }, new float[] { 0f, 0f }));
            Assert.Throws<QException>(() => random.Choose(new[] { "a", "b" }, new float[] { 1f, -1f }));
            Assert.Throws<QException>(() => random.Choose(new[] { "a", "b" }, new double[] { 0.0, 0.0 }));
            Assert.Throws<QException>(() => random.Choose(new[] { "a", "b" }, new int[] { 1, -1 }));
            Assert.Throws<QException>(() => random.Choose(new[] { "a", "b" }, new int[] { 0, 0 }));
        }
    }

    public class VectorExtraTests
    {
        [Fact]
        public void Sort_OrdersLargeInput()
        {
            var random = new QRandom(42);
            var vector = new Vector<int>();
            for (var i = 0; i < 200; i++) vector.PushBack(random.Next(0, 1000));
            vector.Sort((a, b) => a.CompareTo(b));
            var array = vector.ToArray();
            for (var i = 1; i < array.Length; i++) Assert.True(array[i - 1] <= array[i]);
        }

        [Fact]
        public void Clear_Add_Count_And_Enumeration()
        {
            var vector = new Vector<int>();
            vector.Add(1);
            vector.Add(2);
            Assert.Equal(2, vector.Count);
            var sum = 0;
            foreach (var item in vector) sum += item;
            Assert.Equal(3, sum);
            vector.Clear();
            Assert.Equal(0, vector.Size);
            Assert.Empty(vector.ToArray());
        }

        [Fact]
        public void ToString_FormatsWithSpaces()
        {
            var vector = new Vector<int>();
            vector.PushBack(1);
            vector.PushBack(2);
            Assert.Equal("1 2 ", vector.ToString());
            Assert.Equal("1\n2\n", vector.ToStringLine());
        }
    }

    public class VecExtraTests
    {
        [Fact]
        public void Units_And_Distance()
        {
            Assert.Equal(new Vec2(0f, 0f), Vec2.Zero);
            Assert.Equal(new Vec2(1f, 0f), Vec2.UnitX);
            Assert.Equal(new Vec2(0f, 1f), Vec2.UnitY);
            Assert.Equal(5f, Vec2.Distance(new Vec2(0f, 0f), new Vec2(3f, 4f)), 5);
            Assert.Equal(new Vec3(0f, 0f, 1f), Vec3.UnitZ);
            Assert.Equal(5f, Vec3.Distance(Vec3.Zero, new Vec3(0f, 3f, 4f)), 5);
        }
    }

    public class QExceptionTests
    {
        [Fact]
        public void ToString_PreservesStackTrace()
        {
            QException caught = null;
            try
            {
                throw new QException("boom", 7);
            }
            catch (QException ex)
            {
                caught = ex;
            }
            Assert.NotNull(caught);
            var text = caught.ToString();
            Assert.Contains("Error code: 7", text);
            Assert.Contains("boom", text);
            Assert.Contains(nameof(ToString_PreservesStackTrace), text);
        }

        [Fact]
        public void MessageOnly_DefaultsErrorCode()
        {
            Assert.Equal(0, new QException("x").ErrorCode);
        }
    }
}
