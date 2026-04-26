# QuantumNetLib

QuantumNetLib is a custom C# library that provides a set of foundational utilities and mathematical constructs for .NET applications. This library includes a vector implementation resembling C++'s `std::vector`, a pseudo-random number generator, and various mathematical and LINQ-like operations.

## Features

- `Vector<T>`: A dynamic array class with functionality similar to C++'s `std::vector`.
- `Vec2` & `Vec3`: Classes representing two and three-dimensional vectors with related operations.
- `Random`: A pseudo-random number generator class for generating various random data types.
- `Math`: Provides a collection of mathematical functions and utilities.
- `LINQ`: A set of methods that offer LINQ-like query operations for arrays.
- `Exception`: Custom exception handling utilities.

## Getting Started

To use QuantumNetLib in your project, clone this repository and include the .cs files in your project or compile it into a DLL.

```bash
git clone https://github.com/DeQuex/QuantumNetLib.git

```
## Prerequisites
.NET Framework 4.8
Visual Studio 2022 (recommended)

## Installing
After cloning the repository, add the QuantumNetLib project to your solution or reference the compiled DLL in your C# project.

# Usage
Below are quick usage examples for each major component of the library:

# Vector<T>
```c#
var vector = new QuantumNetLib.Vector<int>();
vector.PushBack(1);
vector.PushBack(2);
// Continue using the vector as needed
```

# Random
```c#
var random = new QuantumNetLib.Random();
int randomNumber = random.Next(0, 100);
```

# Math
```c#
float randomValue = QuantumNetLib.Math.Random(0f, 10f);
```

# Vec2 & Vec3
```c#
var vec2 = new QuantumNetLib.Vec2(1, 2);
var vec3 = new QuantumNetLib.Vec3(1, 2, 3);
```

# LINQ
```c#
int[] numbers = { 1, 2, 3, 4 };
var evenNumbers = QuantumNetLib.LINQ.Where(numbers, x => x % 2 == 0);
```

# Contributing
Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are greatly appreciated.

# License
Distributed under the MIT License. See LICENSE for more information.
