# AsarArchiveFile

Namespace: craftersmine.Asar.Net

Represents an ASAR archive file

```csharp
public class AsarArchiveFile
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AsarArchiveFile](./craftersmine.asar.net.asararchivefile.md)

## Properties

### **Offset**

Gets offset of file in ASAR archive after header

```csharp
public long Offset { get; private set; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **Size**

Gets size of file

```csharp
public long Size { get; private set; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **IsUnpacked**

Gets  if file is unpacked and located in *.asar.unpacked directory, otherwise

```csharp
public bool IsUnpacked { get; private set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsExecutable**

Gets  if file is executable

```csharp
public bool IsExecutable { get; private set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsLink**

Gets  if file is a link

```csharp
public bool IsLink { get; private set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsRoot**

Gets  if file is an ASAR archive root

```csharp
public bool IsRoot { get; internal set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Name**

Gets name of file in ASAR archive

```csharp
public string Name { get; internal set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Integrity**

Gets file integrity information

```csharp
public AsarArchiveFileIntegrity Integrity { get; private set; }
```

#### Property Value

[AsarArchiveFileIntegrity](./craftersmine.asar.net.asararchivefileintegrity.md)<br>

### **Files**

Gets child files if it is a directory

```csharp
public AsarArchiveFileCollection Files { get; internal set; }
```

#### Property Value

[AsarArchiveFileCollection](./craftersmine.asar.net.asararchivefilecollection.md)<br>

### **Parent**

Gets file parent file (directory)

```csharp
public AsarArchiveFile Parent { get; internal set; }
```

#### Property Value

[AsarArchiveFile](./craftersmine.asar.net.asararchivefile.md)<br>

## Methods

### **GetFileCount()**

Gets total count of child files in this file (directory)

```csharp
public int GetFileCount()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **GetPathInArchive()**

Gets file path within ASAR archive

```csharp
public string GetPathInArchive()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ShouldSerializeOffset()**



```csharp
public bool ShouldSerializeOffset()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ShouldSerializeSize()**



```csharp
public bool ShouldSerializeSize()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

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
