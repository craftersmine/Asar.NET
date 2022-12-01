# AsarArchivePackerData

Namespace: craftersmine.Asar.Net

Represents an ASAR archive builder

```csharp
public class AsarArchivePackerData
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AsarArchivePackerData](./craftersmine.asar.net.asararchivepackerdata.md)

## Properties

### **PendingFiles**

Gets currently pending files for adding into new archive

```csharp
public AsarArchivePendingFile[] PendingFiles { get; }
```

#### Property Value

[AsarArchivePendingFile[]](./craftersmine.asar.net.asararchivependingfile.md)<br>

### **PendingDirectories**

```csharp
public AsarArchivePendingDirectory[] PendingDirectories { get; }
```

#### Property Value

[AsarArchivePendingDirectory[]](./craftersmine.asar.net.asararchivependingdirectory.md)<br>

### **OutputDirectoryPath**

Gets an output directory path for archive

```csharp
public string OutputDirectoryPath { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ArchiveName**

Gets an ASAR archive name

```csharp
public string ArchiveName { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **PerformSort**

```csharp
public bool PerformSort { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **AsarArchivePackerData(String, String)**

Creates a new instance of ASAR archive data

```csharp
public AsarArchivePackerData(string outputDir, string archiveName)
```

#### Parameters

`outputDir` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`archiveName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
