# AsarArchiveUnpacker

Namespace: craftersmine.Asar.Net

Represents ASAR archive unpacker. This class cannot be inherited

```csharp
public sealed class AsarArchiveUnpacker
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AsarArchiveUnpacker](./craftersmine.asar.net.asararchiveunpacker.md)

## Properties

### **Archive**

Gets an ASAR archive that associated with this unpacker

```csharp
public AsarArchive Archive { get; private set; }
```

#### Property Value

[AsarArchive](./craftersmine.asar.net.asararchive.md)<br>

## Constructors

### **AsarArchiveUnpacker(AsarArchive)**

Creates new instance of ASAR archive unpacker

```csharp
public AsarArchiveUnpacker(AsarArchive archive)
```

#### Parameters

`archive` [AsarArchive](./craftersmine.asar.net.asararchive.md)<br>
 that needs to be unpacked

## Methods

### **UnpackAsync(String)**

Unpacks specified ASAR [AsarArchiveUnpacker.Archive](./craftersmine.asar.net.asararchiveunpacker.md#archive) into specified output directory

```csharp
public Task UnpackAsync(string outputDir)
```

#### Parameters

`outputDir` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Path to the output directory

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **UnpackFileAsync(String, String)**

Unpacks file with specified path in archive to output file path

```csharp
public Task UnpackFileAsync(string pathInArchive, string outputFilePath)
```

#### Parameters

`pathInArchive` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
File path within ASAR archive

`outputFilePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Path to the output file

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
When file path in archive or output file path is null or empty

### **UnpackFileAsync(String, Stream)**

Unpacks file with specified path in archive to specified stream

```csharp
public Task UnpackFileAsync(string pathInArchive, Stream outputStream)
```

#### Parameters

`pathInArchive` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
File path within ASAR archive

`outputStream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
Data stream to which unpack specified file

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
When file path in archive is null or empty or output stream is null

### **UnpackFileAsync(AsarArchiveFile, String)**

Unpacks specified file in archive to output file path

```csharp
public Task UnpackFileAsync(AsarArchiveFile file, string outputFilePath)
```

#### Parameters

`file` [AsarArchiveFile](./craftersmine.asar.net.asararchivefile.md)<br>
File within ASAR archive

`outputFilePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Path to the output file

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
When specified file within ASAR archive is null or output file path is null or empty

### **UnpackFileAsync(AsarArchiveFile, Stream)**

Unpacks specified file in archive to output stream

```csharp
public Task UnpackFileAsync(AsarArchiveFile file, Stream outputStream)
```

#### Parameters

`file` [AsarArchiveFile](./craftersmine.asar.net.asararchivefile.md)<br>
File within ASAR archive

`outputStream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
Data stream to which unpack specified file

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
When specified file withing ASAR archive is null or output stream is null

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
When output stream for unpacked file is read-only

## Events

### **StatusChanged**

Occurs when unpacking using [AsarArchiveUnpacker.UnpackAsync(String)](./craftersmine.asar.net.asararchiveunpacker.md#unpackasyncstring) status changed

```csharp
public event EventHandler<AsarUnpackingStatusChangedEventArgs> StatusChanged;
```

### **AsarArchiveUnpacked**

Occurs when ASAR archive fully unpacked by using [AsarArchiveUnpacker.UnpackAsync(String)](./craftersmine.asar.net.asararchiveunpacker.md#unpackasyncstring)

```csharp
public event EventHandler<AsarUnpackingCompletedEventArgs> AsarArchiveUnpacked;
```
