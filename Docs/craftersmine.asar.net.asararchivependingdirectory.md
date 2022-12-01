# AsarArchivePendingDirectory

Namespace: craftersmine.Asar.Net

Represents a pending directory to be packed in ASAR archive

```csharp
public class AsarArchivePendingDirectory
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AsarArchivePendingDirectory](./craftersmine.asar.net.asararchivependingdirectory.md)

## Properties

### **DirectoryPath**

Gets full path to directory which needs to be archived

```csharp
public string DirectoryPath { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **DirectoryInfo**

Gets or sets directory info which needs to be archived

```csharp
public DirectoryInfo DirectoryInfo { get; set; }
```

#### Property Value

[DirectoryInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo)<br>

### **PendingFiles**

```csharp
public AsarArchivePendingFile[] PendingFiles { get; private set; }
```

#### Property Value

[AsarArchivePendingFile[]](./craftersmine.asar.net.asararchivependingfile.md)<br>

### **PendingDirectories**

```csharp
public AsarArchivePendingDirectory[] PendingDirectories { get; private set; }
```

#### Property Value

[AsarArchivePendingDirectory[]](./craftersmine.asar.net.asararchivependingdirectory.md)<br>

## Constructors

### **AsarArchivePendingDirectory(DirectoryInfo, Boolean, Boolean)**

Creates a new instance of [AsarArchivePendingFile](./craftersmine.asar.net.asararchivependingfile.md)

```csharp
public AsarArchivePendingDirectory(DirectoryInfo dirInfo, bool unpacked, bool calculateIntegrity)
```

#### Parameters

`dirInfo` [DirectoryInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo)<br>
File information about packing file

`unpacked` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`calculateIntegrity` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
