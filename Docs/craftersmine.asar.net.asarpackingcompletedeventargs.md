# AsarPackingCompletedEventArgs

Namespace: craftersmine.Asar.Net

Represents an ASAR packing completed event args

```csharp
public class AsarPackingCompletedEventArgs : System.EventArgs
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/system.eventargs) → [AsarPackingCompletedEventArgs](./craftersmine.asar.net.asarpackingcompletedeventargs.md)

## Properties

### **AsarFilePath**

Gets a path to packed ASAR archive

```csharp
public string AsarFilePath { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **PackedArchive**

Gets an instance of a packed ASAR archive

```csharp
public AsarArchive PackedArchive { get; private set; }
```

#### Property Value

[AsarArchive](./craftersmine.asar.net.asararchive.md)<br>

## Constructors

### **AsarPackingCompletedEventArgs(String, AsarArchive)**



```csharp
public AsarPackingCompletedEventArgs(string asarFilePath, AsarArchive packedArchive)
```

#### Parameters

`asarFilePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`packedArchive` [AsarArchive](./craftersmine.asar.net.asararchive.md)<br>
