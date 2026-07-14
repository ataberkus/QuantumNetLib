namespace QuantumNetLib
{
    public static class QLinq
    {
        public static T[] Where<T>(T[] array, System.Func<T, bool> predicate)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (predicate == null) throw new QException("Predicate cannot be null", 11);

            var result = new Vector<T>();
            foreach (var item in array)
                if (predicate(item))
                    result.PushBack(item);
            return result.ToArray();
        }

        public static TResult[] Select<T, TResult>(T[] array, System.Func<T, TResult> selector)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (selector == null) throw new QException("Selector cannot be null", 11);

            var result = new Vector<TResult>();
            foreach (var item in array) result.PushBack(selector(item));
            return result.ToArray();
        }

        public static TResult[] Select<T, TResult>(T[] array, System.Func<T, int, TResult> selector)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (selector == null) throw new QException("Selector cannot be null", 11);

            var result = new Vector<TResult>();
            for (var i = 0; i < array.Length; i++) result.PushBack(selector(array[i], i));
            return result.ToArray();
        }

        public static TResult[] SelectMany<T, TResult>(T[] array, System.Func<T, TResult[]> selector)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (selector == null) throw new QException("Selector cannot be null", 11);

            var result = new Vector<TResult>();
            foreach (var item in array)
            foreach (var subItem in selector(item))
                result.PushBack(subItem);
            return result.ToArray();
        }

        public static TResult[] SelectMany<T, TResult>(T[] array, System.Func<T, int, TResult[]> selector)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (selector == null) throw new QException("Selector cannot be null", 11);

            var result = new Vector<TResult>();
            for (var i = 0; i < array.Length; i++)
                foreach (var subItem in selector(array[i], i))
                    result.PushBack(subItem);
            return result.ToArray();
        }

        public static TResult[] SelectMany<T, TCollection, TResult>(
            T[] array,
            System.Func<T, TCollection[]> selector,
            System.Func<T, TCollection, TResult> resultSelector)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (selector == null) throw new QException("Selector cannot be null", 11);
            if (resultSelector == null) throw new QException("Result selector cannot be null", 11);

            var result = new Vector<TResult>();
            foreach (var item in array)
            foreach (var subItem in selector(item))
                result.PushBack(resultSelector(item, subItem));
            return result.ToArray();
        }

        public static T First<T>(T[] array, System.Func<T, bool> predicate)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (predicate == null) throw new QException("Predicate cannot be null", 11);

            foreach (var item in array)
                if (predicate(item))
                    return item;

            throw new QException("Sequence contains no matching element", 2);
        }

        public static T FirstOrDefault<T>(T[] array, System.Func<T, bool> predicate)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (predicate == null) throw new QException("Predicate cannot be null", 11);

            foreach (var item in array)
                if (predicate(item))
                    return item;
            return default;
        }

        public static T First<T>(T[] array)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (array.Length == 0) throw new QException("Sequence contains no elements", 2);
            return array[0];
        }

        public static T FirstOrDefault<T>(T[] array)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            return array.Length > 0 ? array[0] : default;
        }

        public static T Last<T>(T[] array, System.Func<T, bool> predicate)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (predicate == null) throw new QException("Predicate cannot be null", 11);

            for (var i = array.Length - 1; i >= 0; i--)
                if (predicate(array[i]))
                    return array[i];

            throw new QException("Sequence contains no matching element", 2);
        }

        public static T LastOrDefault<T>(T[] array, System.Func<T, bool> predicate)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (predicate == null) throw new QException("Predicate cannot be null", 11);

            for (var i = array.Length - 1; i >= 0; i--)
                if (predicate(array[i]))
                    return array[i];
            return default;
        }

        public static T Last<T>(T[] array)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (array.Length == 0) throw new QException("Sequence contains no elements", 2);
            return array[array.Length - 1];
        }

        public static T LastOrDefault<T>(T[] array)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            return array.Length > 0 ? array[array.Length - 1] : default;
        }

        public static T Single<T>(T[] array, System.Func<T, bool> predicate)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (predicate == null) throw new QException("Predicate cannot be null", 11);

            var result = default(T);
            var found = false;
            foreach (var item in array)
                if (predicate(item))
                {
                    if (found) throw new QException("Sequence contains more than one element", 1);
                    result = item;
                    found = true;
                }

            if (!found) throw new QException("Sequence contains no elements", 2);
            return result;
        }

        public static T SingleOrDefault<T>(T[] array, System.Func<T, bool> predicate)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (predicate == null) throw new QException("Predicate cannot be null", 11);

            var result = default(T);
            var found = false;
            foreach (var item in array)
                if (predicate(item))
                {
                    if (found) throw new QException("Sequence contains more than one element", 1);
                    result = item;
                    found = true;
                }

            return result;
        }

        public static T Single<T>(T[] array)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (array.Length == 0) throw new QException("Sequence contains no elements", 2);
            if (array.Length > 1) throw new QException("Sequence contains more than one element", 1);
            return array[0];
        }

        public static T SingleOrDefault<T>(T[] array)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (array.Length > 1) throw new QException("Sequence contains more than one element", 1);
            return array.Length > 0 ? array[0] : default;
        }

        public static T ElementAt<T>(T[] array, int index)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (index < 0 || index >= array.Length) throw new QException("Index out of range", 1);
            return array[index];
        }

        public static T ElementAtOrDefault<T>(T[] array, int index)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            if (index < 0 || index >= array.Length) return default;
            return array[index];
        }

        public static T[] Concat<T>(params T[][] arrays)
        {
            if (arrays == null) throw new QException("Arrays cannot be null", 10);

            var result = new Vector<T>();
            foreach (var array in arrays)
            {
                if (array == null) throw new QException("Array cannot be null", 10);
                foreach (var item in array) result.PushBack(item);
            }

            return result.ToArray();
        }

        public static int Sum(int[] array)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            var result = 0;
            foreach (var item in array) result += item;
            return result;
        }

        public static float Sum(float[] array)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            float result = 0;
            foreach (var item in array) result += item;
            return result;
        }

        public static double Sum(double[] array)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            double result = 0;
            foreach (var item in array) result += item;
            return result;
        }

        public static long Sum(long[] array)
        {
            if (array == null) throw new QException("Array cannot be null", 10);
            long result = 0;
            foreach (var item in array) result += item;
            return result;
        }
    }
}
