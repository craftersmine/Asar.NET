# AsarArchiveFileIntegrity

Namespace: craftersmine.Asar.Net

Represents a ASAR archive file integrity information. This class cannot be inherited

```csharp
public sealed class AsarArchiveFileIntegrity
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AsarArchiveFileIntegrity](./craftersmine.asar.net.asararchivefileintegrity.md)

## Properties

### **BlockSize**

Gets file block size

```csharp
public int BlockSize { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Algorithm**

Gets file hashing algorithm

```csharp
public string Algorithm { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Hash**

Gets whole file hash

```csharp
public string Hash { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Blocks**

Gets hashes for file blocks of specified size

```csharp
public String[] Blocks { get; private set; }
```

#### Property Value

[String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **AsarArchiveFileIntegrity(Int32, String, String, String[])**

Creates a new instance of ASAR archive file integrity data

```csharp
public AsarArchiveFileIntegrity(int blockSize, string algorithm, string hash, String[] blocks)
```

#### Parameters

`blockSize` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Size of one block for hashing

`algorithm` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Algorithm used for hashing

`hash` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Whole file hash

`blocks` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Array of file blocks hashes

### **AsarArchiveFileIntegrity(String, String[])**

```csharp
public AsarArchiveFileIntegrity(string hash, String[] blocks)
```

#### Parameters

`hash` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`blocks` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **GetFileIntegrityAsync(String)**

Computes file hashes for specified file and returns [AsarArchiveFileIntegrity](./craftersmine.asar.net.asararchivefileintegrity.md) with hashes

```csharp
public static Task<AsarArchiveFileIntegrity> GetFileIntegrityAsync(string filePath)
```

#### Parameters

`filePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Path to file for computing hashes

#### Returns

[Task&lt;AsarArchiveFileIntegrity&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
File integrity information

### **GetStreamIntegrityAsync(Stream)**

Computes hashes for specified stream and returns [AsarArchiveFileIntegrity](./craftersmine.asar.net.asararchivefileintegrity.md) with hashes

```csharp
public static Task<AsarArchiveFileIntegrity> GetStreamIntegrityAsync(Stream stream)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
Data stream for computing hashes

#### Returns

[Task&lt;AsarArchiveFileIntegrity&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
Stream integrity information

### **ValidateFileAsync(String, AsarArchiveFileIntegrity)**

Validates file at specified path against specified integrity data and returns  if data is same, otherwise

```csharp
public static Task<bool> ValidateFileAsync(string filePath, AsarArchiveFileIntegrity integrityData)
```

#### Parameters

`filePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Path to file for checks

`integrityData` [AsarArchiveFileIntegrity](./craftersmine.asar.net.asararchivefileintegrity.md)<br>
Integrity data for checks

#### Returns

[Task&lt;Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
 if file valid, otherwise

### **ValidateStreamAsync(Stream, AsarArchiveFileIntegrity)**

Validates data in stream against specified integrity data and returns  if data is same, otherwise

```csharp
public static Task<bool> ValidateStreamAsync(Stream stream, AsarArchiveFileIntegrity integrityData)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
Data stream for checks

`integrityData` [AsarArchiveFileIntegrity](./craftersmine.asar.net.asararchivefileintegrity.md)<br>
Integrity data for checks

#### Returns

[Task&lt;Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
 if file valid, otherwise

### **Equals(Object)**

```csharp
public bool Equals(object obj)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **GetHashCode()**

```csharp
public int GetHashCode()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
