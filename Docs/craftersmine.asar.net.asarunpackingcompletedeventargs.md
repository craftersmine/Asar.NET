# AsarUnpackingCompletedEventArgs

Namespace: craftersmine.Asar.Net

Contains [AsarArchiveUnpacker.AsarArchiveUnpacked](./craftersmine.asar.net.asararchiveunpacker.md#asararchiveunpacked) event arguments

```csharp
public class AsarUnpackingCompletedEventArgs : System.EventArgs
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/system.eventargs) → [AsarUnpackingCompletedEventArgs](./craftersmine.asar.net.asarunpackingcompletedeventargs.md)

## Properties

### **OutputDirectoryPath**

Gets output directory of unpacked archive

```csharp
public string OutputDirectoryPath { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **OutputDirectory**

Gets information about output directory of unpacked archive

```csharp
public DirectoryInfo OutputDirectory { get; private set; }
```

#### Property Value

[DirectoryInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo)<br>
