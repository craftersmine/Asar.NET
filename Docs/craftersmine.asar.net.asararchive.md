# AsarArchive

Namespace: craftersmine.Asar.Net

Represents an Electron ASAR archive

```csharp
public class AsarArchive : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AsarArchive](./craftersmine.asar.net.asararchive.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Properties

### **HeaderSize**

Gets a size of the header metadata

```csharp
public long HeaderSize { get; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **FilesDataOffset**

Gets an offset value where in the archive actual archived files data starts

```csharp
public int FilesDataOffset { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **IsFile**

Gets a  if archive loaded from file, otherwise

```csharp
public bool IsFile { get; private set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **FilePath**

Gets a path to archive file or empty string if archive was loaded from file

```csharp
public string FilePath { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **UnpackedFilesPath**

Gets a path to unpacked files of this ASAR archive

```csharp
public string UnpackedFilesPath { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Files**

Gets packed files metadata, such as file sizes, offsets, integrity data, etc.

```csharp
public AsarArchiveFile Files { get; private set; }
```

#### Property Value

[AsarArchiveFile](./craftersmine.asar.net.asararchivefile.md)<br>

## Constructors

### **AsarArchive(Stream)**

Opens an ASAR archive from data stream

```csharp
public AsarArchive(Stream stream)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
Data stream with ASAR archive data

#### Exceptions

[AsarException](./craftersmine.asar.net.asarexception.md)<br>
When data in the stream is not ASAR archive

### **AsarArchive(String)**

Opens an ASAR archive from file

```csharp
public AsarArchive(string filePath)
```

#### Parameters

`filePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Path to ASAR archive

#### Exceptions

[AsarException](./craftersmine.asar.net.asarexception.md)<br>
When data in the file is not ASAR archive

## Methods

### **Dispose()**

Closes an ASAR archive, closes file stream and disposes all used resources

```csharp
public void Dispose()
```

### **ReadBytesAsync(String)**

Reads all bytes of ASAR archived file byt specified path within archive into [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte) array

```csharp
public Task<Byte[]> ReadBytesAsync(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Path to file within ASAR archive

#### Returns

[Task&lt;Byte[]&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
An array of  from file

### **ReadBytesAsync(AsarArchiveFile)**

Reads all bytes of ASAR archived file into [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte) array

```csharp
public Task<Byte[]> ReadBytesAsync(AsarArchiveFile file)
```

#### Parameters

`file` [AsarArchiveFile](./craftersmine.asar.net.asararchivefile.md)<br>

#### Returns

[Task&lt;Byte[]&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
An array of  from file

### **OpenFileAsStream(String)**

Opens a file with specified location within archive as stream

```csharp
public Stream OpenFileAsStream(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Path to file within archive

#### Returns

[Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
 if file is packed or  if file is unpacked

### **OpenFileAsStream(AsarArchiveFile)**

Opens a file that located in archive as stream

```csharp
public Stream OpenFileAsStream(AsarArchiveFile file)
```

#### Parameters

`file` [AsarArchiveFile](./craftersmine.asar.net.asararchivefile.md)<br>
File to read as stream

#### Returns

[Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
 if file is packed or  if file is unpacked

### **FindFile(String)**

Gets an ASAR archived file by specified path in archive

```csharp
public AsarArchiveFile FindFile(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[AsarArchiveFile](./craftersmine.asar.net.asararchivefile.md)<br>
