# AsarArchivePacker

Namespace: craftersmine.Asar.Net

Represents an ASAR archive packer. This class cannot be inherited

```csharp
public sealed class AsarArchivePacker
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AsarArchivePacker](./craftersmine.asar.net.asararchivepacker.md)

## Properties

### **PackerData**

Gets current ASAR archive data for packer

```csharp
public AsarArchivePackerData PackerData { get; private set; }
```

#### Property Value

[AsarArchivePackerData](./craftersmine.asar.net.asararchivepackerdata.md)<br>

## Constructors

### **AsarArchivePacker(AsarArchivePackerData)**

Creates ASAR archive packer from specified data

```csharp
public AsarArchivePacker(AsarArchivePackerData packerData)
```

#### Parameters

`packerData` [AsarArchivePackerData](./craftersmine.asar.net.asararchivepackerdata.md)<br>
ASAR archive data for packer

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
When packer data is null

## Methods

### **PackAsync()**

Packs ASAR archive with specified info

```csharp
public Task PackAsync()
```

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

## Events

### **StatusChanged**

Occurs when packing status is changed

```csharp
public event EventHandler<AsarPackingEventArgs> StatusChanged;
```

### **AsarArchivePacked**

Occurs when ASAR archive is packed successfully

```csharp
public event EventHandler<AsarPackingCompletedEventArgs> AsarArchivePacked;
```
