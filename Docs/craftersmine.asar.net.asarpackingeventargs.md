# AsarPackingEventArgs

Namespace: craftersmine.Asar.Net

Represents an ASAR packing event args

```csharp
public class AsarPackingEventArgs : System.EventArgs
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/system.eventargs) → [AsarPackingEventArgs](./craftersmine.asar.net.asarpackingeventargs.md)

## Properties

### **TotalFiles**

Gets total amount of files to pack

```csharp
public int TotalFiles { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **CurrentFile**

Gets current packing file index

```csharp
public int CurrentFile { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FilePath**

Gets current packing file path

```csharp
public string FilePath { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **PackingStatus**

Gets current packing status

```csharp
public AsarPackingStatus PackingStatus { get; private set; }
```

#### Property Value

[AsarPackingStatus](./craftersmine.asar.net.asarpackingstatus.md)<br>

### **CurrentFileData**

Gets current processing file data

```csharp
public AsarArchiveFile CurrentFileData { get; private set; }
```

#### Property Value

[AsarArchiveFile](./craftersmine.asar.net.asararchivefile.md)<br>

## Constructors

### **AsarPackingEventArgs(Int32, Int32, String, AsarPackingStatus, AsarArchiveFile)**



```csharp
public AsarPackingEventArgs(int totalFiles, int currentFile, string filePath, AsarPackingStatus status, AsarArchiveFile archiveFile)
```

#### Parameters

`totalFiles` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`currentFile` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`filePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`status` [AsarPackingStatus](./craftersmine.asar.net.asarpackingstatus.md)<br>

`archiveFile` [AsarArchiveFile](./craftersmine.asar.net.asararchivefile.md)<br>
