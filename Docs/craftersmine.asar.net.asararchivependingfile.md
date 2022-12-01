# AsarArchivePendingFile

Namespace: craftersmine.Asar.Net

Represents an pending file to be packed in ASAR archive

```csharp
public class AsarArchivePendingFile
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AsarArchivePendingFile](./craftersmine.asar.net.asararchivependingfile.md)

## Properties

### **CalculateIntegrity**

Gets or sets  if packer must generate integrity data for this file

```csharp
public bool CalculateIntegrity { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsUnpacked**

Gets or sets  if packer should pack this file into ASAR archive file, instead of leave it in unpacked folder

```csharp
public bool IsUnpacked { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **FilePath**

Gets full path to file which needs to be archived

```csharp
public string FilePath { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FileInfo**

Gets or sets file which needs to be archived

```csharp
public FileInfo FileInfo { get; set; }
```

#### Property Value

[FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>

## Constructors

### **AsarArchivePendingFile(FileInfo)**

Creates a new instance of [AsarArchivePendingFile](./craftersmine.asar.net.asararchivependingfile.md)

```csharp
public AsarArchivePendingFile(FileInfo fileInfo)
```

#### Parameters

`fileInfo` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
File information about packing file
