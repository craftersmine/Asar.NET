# AsarUnpackingStatusChangedEventArgs

Namespace: craftersmine.Asar.Net

Contains [AsarArchiveUnpacker.StatusChanged](./craftersmine.asar.net.asararchiveunpacker.md#statuschanged) event arguments

```csharp
public class AsarUnpackingStatusChangedEventArgs : System.EventArgs
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/system.eventargs) → [AsarUnpackingStatusChangedEventArgs](./craftersmine.asar.net.asarunpackingstatuschangedeventargs.md)

## Properties

### **TotalFiles**

Gets total amount of files to be unpacked

```csharp
public int TotalFiles { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **CurrentFile**

Gets current file index that is unpacking

```csharp
public int CurrentFile { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **OutputFilePath**

Gets current file output path

```csharp
public string OutputFilePath { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CurrentFileData**

Gets current file data within ASAR archive

```csharp
public AsarArchiveFile CurrentFileData { get; private set; }
```

#### Property Value

[AsarArchiveFile](./craftersmine.asar.net.asararchivefile.md)<br>
