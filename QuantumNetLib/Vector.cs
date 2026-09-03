namespace QuantumNetLib
{
    public class Vector<T> : System.Collections.Generic.IEnumerable<T>
    {
        public delegate int Comparison(T x, T y);

        private T[] _data;

        public Vector()
        {
            _data = new T[4];
            Size = 0;
        }

        public Vector(int capacity)
        {
            if (capacity < 0)
                throw new QException("Capacity must be non-negative", 3);

            _data = new T[capacity == 0 ? 4 : capacity];
            Size = 0;
        }

        public int Size { get; private set; }

        public int Count => Size;

        public int Capacity => _data.Length;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Size) throw new QException("Index out of range", 1);
                return _data[index];
            }
            set
            {
                if (index < 0 || index >= Size) throw new QException("Index out of range", 1);
                _data[index] = value;
            }
        }

        private void EnsureCapacity(int minCapacity)
        {
            if (minCapacity <= _data.Length) return;

            var newCapacity = _data.Length == 0 ? 4 : _data.Length * 2;
            if (newCapacity < minCapacity) newCapacity = minCapacity;

            var temp = new T[newCapacity];
            System.Array.Copy(_data, temp, Size);
            _data = temp;
        }

        public void PushBack(T item)
        {
            EnsureCapacity(Size + 1);
            _data[Size] = item;
            Size++;
        }

        public void Add(T item)
        {
            PushBack(item);
        }

        public void PopBack()
        {
            if (Size == 0) throw new QException("Vector is empty", 2);
            Size--;
            _data[Size] = default;
        }

        public void RemoveLast()
        {
            PopBack();
        }

        public void Clear()
        {
            System.Array.Clear(_data, 0, Size);
            Size = 0;
        }

        public void Erase(int index)
        {
            if (index < 0 || index >= Size) throw new QException("Index out of range", 1);

            if (index < Size - 1)
                System.Array.Copy(_data, index + 1, _data, index, Size - index - 1);
            Size--;
            _data[Size] = default;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > Size) throw new QException("Index out of range", 1);

            EnsureCapacity(Size + 1);

            if (index < Size)
                System.Array.Copy(_data, index, _data, index + 1, Size - index);
            _data[index] = item;
            Size++;
        }

        public void Sort(Comparison comparison)
        {
            if (comparison == null) throw new QException("Comparison cannot be null", 4);
            System.Array.Sort(_data, 0, Size, System.Collections.Generic.Comparer<T>.Create((x, y) => comparison(x, y)));
        }

        public void Sort()
        {
            System.Array.Sort(_data, 0, Size);
        }

        public Vector<T> Clone()
        {
            var newVector = new Vector<T>(_data.Length);
            System.Array.Copy(_data, newVector._data, Size);
            newVector.Size = Size;
            return newVector;
        }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            for (var i = 0; i < Size; i++)
            {
                sb.Append(_data[i]);
                sb.Append(' ');
            }
            return sb.ToString();
        }

        public string ToStringLine()
        {
            var sb = new System.Text.StringBuilder();
            for (var i = 0; i < Size; i++)
            {
                sb.Append(_data[i]);
                sb.Append('\n');
            }
            return sb.ToString();
        }

        public T[] ToArray()
        {
            var result = new T[Size];
            System.Array.Copy(_data, result, Size);
            return result;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            System.Array.Copy(_data, 0, array, arrayIndex, Size);
        }

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Size; i++) yield return _data[i];
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
