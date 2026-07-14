# QuantumNetLib

QuantumNetLib is a C# utility library with a dynamic `Vector<T>`, 2D/3D math vectors, a Park–Miller PRNG, math helpers, and LINQ-style array helpers.

## Features

- `Vector<T>`: Dynamic array with push/pop/insert/erase/sort (C++ `std::vector`-style API)
- `Vec2` & `Vec3`: Value-type vectors with operators, length, normalize, dot, and cross
- `QRandom`: Park–Miller LCG with shuffle, choose, and weighted selection
- `QMath`: Math helpers backed by `System.Math` for accuracy
- `QLinq`: LINQ-style query helpers for arrays
- `QException`: Exception type with an error code

## Prerequisites

- .NET SDK 8.0+ (library targets `netstandard2.0`)

## Getting Started

```bash
git clone https://github.com/ataberkus/QuantumNetLib.git
cd QuantumNetLib
dotnet build
dotnet test
```

Add a project reference to `QuantumNetLib/QuantumNetLib.csproj`, or reference the built DLL.

## Usage

### Vector<T>

```csharp
var vector = new QuantumNetLib.Vector<int>();
vector.PushBack(1);
vector.PushBack(2);
var copy = vector.ToArray(); // { 1, 2 }
```

### QRandom

```csharp
var random = new QuantumNetLib.QRandom(42);
int value = random.Next(0, 100); // [0, 100)
random.Shuffle(copy);
```

### QMath

```csharp
float value = QuantumNetLib.QMath.Sin(QuantumNetLib.QMath.PI / 2f);
float mapped = QuantumNetLib.QMath.Map(5f, 0f, 10f, 0f, 1f);
```

### Vec2 & Vec3

```csharp
var a = new QuantumNetLib.Vec2(3, 4);
var unit = a.Normalized;          // length ≈ 1
float dot = QuantumNetLib.Vec2.Dot(a, new QuantumNetLib.Vec2(1, 0));

var n = QuantumNetLib.Vec3.Cross(
    new QuantumNetLib.Vec3(1, 0, 0),
    new QuantumNetLib.Vec3(0, 1, 0)); // (0, 0, 1)
```

### QLinq

```csharp
int[] numbers = { 1, 2, 3, 4 };
var evens = QuantumNetLib.QLinq.Where(numbers, x => x % 2 == 0);
var labels = QuantumNetLib.QLinq.Select(numbers, x => x.ToString());
```

## Contributing

Contributions are welcome — open a pull request with focused changes and tests where practical.

## License

Distributed under the MIT License. See `LICENSE.txt` for details.
