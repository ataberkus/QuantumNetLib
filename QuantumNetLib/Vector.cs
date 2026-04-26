namespace QuantumNetLib
{
    public class Vector<T>
    {
        // Delegate for custom comparison
        public delegate int Comparison(T x, T y);

        public Vector()
        {
            Data = new T[4]; // Initial capacity
            Size = 0;
        }

        public T[] Data { get; set; }
        public int Size { get; private set; }

        // Indexer
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Size) throw new Exception("Index out of range", 1);
                return Data[index];
            }
            set
            {
                if (index < 0 || index >= Size) throw new Exception("Index out of range", 1);
                Data[index] = value;
            }
        }

        // Resize array
        private void Resize()
        {
            var newCapacity = Data.Length == 0 ? 4 : Data.Length * 2;
            var temp = new T[newCapacity];
            Data.CopyTo(temp, 0);
            Data = temp;
        }

        // Add element at the end
        public void PushBack(T item)
        {
            if (Size == Data.Length) Resize();
            Data[Size] = item;
            Size++;
        }

        // Remove element at the end
        public void PopBack()
        {
            if (Size == 0) throw new Exception("Vector is empty", 2);
            Size--;
            Data[Size] = default;
        }

        // Get element at index
        public void Erase(int index)
        {
            if (index < 0 || index >= Size) throw new Exception("Index out of range", 1);

            for (var i = index; i < Size - 1; i++) Data[i] = Data[i + 1];
            Size--;
            Data[Size] = default;
        }


        // Insert element at index
        public void Insert(int index, T item)
        {
            if (index < 0 || index > Size) throw new Exception("Index out of range", 1);

            if (Size == Data.Length) Resize();

            for (var i = Size; i > index; i--) Data[i] = Data[i - 1];
            Data[index] = item;
            Size++;
        }

        // Sort using a custom comparison function
        public void Sort(Comparison comparison)
        {
            // Implement a sorting algorithm here, using 'comparison' to compare elements
            for (var i = 0; i < Size - 1; i++)
            {
                for (var j = 0; j < Size - i - 1; j++)
                {
                    if (comparison(Data[j], Data[j + 1]) > 0)
                    {
                        // Swap elements
                        (Data[j], Data[j + 1]) = (Data[j + 1], Data[j]);
                    }
                }
            }
        }

        public Vector<T> Clone() // Deep copy
        {
            var newVector = new Vector<T>();
            newVector.Data = new T[Data.Length];
            Data.CopyTo(newVector.Data, 0);
            newVector.Size = Size;
            return newVector;
        }

        // Get string
        public override string ToString()
        {
            var result = "";
            for (var i = 0; i < Size; i++) result += Data[i] + " ";
            return result;
        }

        // Get string with new line
        public string ToStringLine()
        {
            var result = "";
            for (var i = 0; i < Size; i++) result += Data[i] + "\n";
            return result;
        }

        public T[] ToArray()
        {
            var result = new T[Size];
            for (var i = 0; i < Size; i++) result[i] = Data[i];
            return result;
        }
    }
}
