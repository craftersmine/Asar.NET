# AsarArchivePackerDataBuilder

Namespace: craftersmine.Asar.Net

Represents a builder of data for ASAR archive packer

```csharp
public class AsarArchivePackerDataBuilder
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)

## Methods

### **CreateBuilder(String, String)**

```csharp
public static AsarArchivePackerDataBuilder CreateBuilder(string outputDir, string archiveName)
```

#### Parameters

`outputDir` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`archiveName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>

### **CreateBuilder(String, String, Boolean)**

Creates a new archive packer data builder instance

```csharp
public static AsarArchivePackerDataBuilder CreateBuilder(string outputDir, string archiveName, bool createOutputDir)
```

#### Parameters

`outputDir` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`archiveName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`createOutputDir` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
When output directory or archive name is null or empty

[DirectoryNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.directorynotfoundexception)<br>
When output directory is not found at specified location

### **AddFile(String)**

Adds file to the archive

```csharp
public AsarArchivePackerDataBuilder AddFile(string filePath)
```

#### Parameters

`filePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Path to the file

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
When specified file is not found

### **AddFile(FileInfo)**

Adds file to the archive

```csharp
public AsarArchivePackerDataBuilder AddFile(FileInfo fileInfo)
```

#### Parameters

`fileInfo` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
File info of the file

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
When specified file is not found

### **AddFile(AsarArchivePendingFile)**

Adds file to the archive

```csharp
public AsarArchivePackerDataBuilder AddFile(AsarArchivePendingFile file)
```

#### Parameters

`file` [AsarArchivePendingFile](./craftersmine.asar.net.asararchivependingfile.md)<br>
Pending file for packing

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
When specified file is not found

### **AddFile(String, Boolean)**

Adds file to the archive

```csharp
public AsarArchivePackerDataBuilder AddFile(string filePath, bool unpacked)
```

#### Parameters

`filePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Path to the file

`unpacked` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Determines whether this file needs to be packed into ASAR file or left unpacked besides archive file

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
When specified file is not found

### **AddFile(FileInfo, Boolean)**

Adds file to the archive

```csharp
public AsarArchivePackerDataBuilder AddFile(FileInfo fileInfo, bool unpacked)
```

#### Parameters

`fileInfo` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
File info of the file

`unpacked` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Determines whether this file needs to be packed into ASAR file or left unpacked besides archive file

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
When specified file is not found

### **AddFile(String, Boolean, Boolean)**

Adds file to the archive

```csharp
public AsarArchivePackerDataBuilder AddFile(string filePath, bool unpacked, bool calculateIntegrity)
```

#### Parameters

`filePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Path to the file

`unpacked` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Determines whether this file needs to be packed into ASAR file or left unpacked besides archive file

`calculateIntegrity` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Determines whether integrity needs to be calculated for this file before packing

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
When specified file is not found

### **AddFile(FileInfo, Boolean, Boolean)**

Adds file to the archive

```csharp
public AsarArchivePackerDataBuilder AddFile(FileInfo fileInfo, bool unpacked, bool calculateIntegrity)
```

#### Parameters

`fileInfo` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
File info of the file

`unpacked` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Determines whether this file needs to be packed into ASAR file or left unpacked besides archive file

`calculateIntegrity` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Determines whether integrity needs to be calculated for this file before packing

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
When specified file is not found

### **AddFiles(String[])**

Adds files to the archive

```csharp
public AsarArchivePackerDataBuilder AddFiles(String[] filePaths)
```

#### Parameters

`filePaths` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
File paths of the files

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
When specified file in array is not found

### **AddFiles(FileInfo[])**

Adds files to the archive

```csharp
public AsarArchivePackerDataBuilder AddFiles(FileInfo[] fileInfos)
```

#### Parameters

`fileInfos` [FileInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
File infos of the files

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
When specified file in array is not found

### **AddFiles(AsarArchivePendingFile[])**

Adds files to the archive

```csharp
public AsarArchivePackerDataBuilder AddFiles(AsarArchivePendingFile[] files)
```

#### Parameters

`files` [AsarArchivePendingFile[]](./craftersmine.asar.net.asararchivependingfile.md)<br>
Pending files for packing

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
When specified file in array is not found

### **AddFiles(Boolean, Boolean, String[])**

Adds files to the archive

```csharp
public AsarArchivePackerDataBuilder AddFiles(bool unpacked, bool calculateIntegrity, String[] filePaths)
```

#### Parameters

`unpacked` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Determines whether this files needs to be packed into ASAR file or left unpacked besides archive file

`calculateIntegrity` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Determines whether integrity needs to be calculated for this file before packing

`filePaths` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
File paths of the files

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
When specified file in array is not found

### **AddFiles(Boolean, Boolean, FileInfo[])**

Adds files to the archive

```csharp
public AsarArchivePackerDataBuilder AddFiles(bool unpacked, bool calculateIntegrity, FileInfo[] fileInfos)
```

#### Parameters

`unpacked` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Determines whether this files needs to be packed into ASAR file or left unpacked besides archive file

`calculateIntegrity` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Determines whether integrity needs to be calculated for this file before packing

`fileInfos` [FileInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
File infos of the files

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
When specified file in array is not found

### **AddFiles(Boolean, String[])**

Adds files to the archive

```csharp
public AsarArchivePackerDataBuilder AddFiles(bool unpacked, String[] filePaths)
```

#### Parameters

`unpacked` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Determines whether this files needs to be packed into ASAR file or left unpacked besides archive file

`filePaths` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
File paths of the files

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
When specified file in array is not found

### **AddFiles(Boolean, FileInfo[])**

Adds files to the archive

```csharp
public AsarArchivePackerDataBuilder AddFiles(bool unpacked, FileInfo[] fileInfos)
```

#### Parameters

`unpacked` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Determines whether this files needs to be packed into ASAR file or left unpacked besides archive file

`fileInfos` [FileInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
File infos of the files

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
When specified file in array is not found

### **AddDirectory(DirectoryInfo)**

Adds directory to the archive

```csharp
public AsarArchivePackerDataBuilder AddDirectory(DirectoryInfo directoryInfo)
```

#### Parameters

`directoryInfo` [DirectoryInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo)<br>
Directory info

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[DirectoryNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.directorynotfoundexception)<br>
When specified file in array is not found

### **AddDirectory(String)**

Adds directory to the archive

```csharp
public AsarArchivePackerDataBuilder AddDirectory(string directoryPath)
```

#### Parameters

`directoryPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Directory path

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[DirectoryNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.directorynotfoundexception)<br>
When specified file in array is not found

### **AddDirectory(DirectoryInfo, Boolean)**

Adds directory to the archive

```csharp
public AsarArchivePackerDataBuilder AddDirectory(DirectoryInfo directoryInfo, bool unpacked)
```

#### Parameters

`directoryInfo` [DirectoryInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo)<br>
Directory info

`unpacked` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
 if directory needs to remain unpacked

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[DirectoryNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.directorynotfoundexception)<br>
When specified file in array is not found

### **AddDirectory(String, Boolean)**

Adds directory to the archive

```csharp
public AsarArchivePackerDataBuilder AddDirectory(string directoryPath, bool unpacked)
```

#### Parameters

`directoryPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Directory path

`unpacked` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
 if directory needs to remain unpacked

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[DirectoryNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.directorynotfoundexception)<br>
When specified file in array is not found

### **AddDirectory(DirectoryInfo, Boolean, Boolean)**

Adds directory to the archive

```csharp
public AsarArchivePackerDataBuilder AddDirectory(DirectoryInfo directoryInfo, bool unpacked, bool calculateIntegrity)
```

#### Parameters

`directoryInfo` [DirectoryInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo)<br>
Directory info

`unpacked` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
 if directory needs to remain unpacked

`calculateIntegrity` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
 if integrity needs to be calculated for directory

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[DirectoryNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.directorynotfoundexception)<br>
When specified file in array is not found

### **AddDirectory(String, Boolean, Boolean)**

Adds directory to the archive

```csharp
public AsarArchivePackerDataBuilder AddDirectory(string directoryPath, bool unpacked, bool calculateIntegrity)
```

#### Parameters

`directoryPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Directory path

`unpacked` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
 if directory needs to remain unpacked

`calculateIntegrity` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
 if integrity needs to be calculated for directory

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[DirectoryNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.directorynotfoundexception)<br>
When specified file in array is not found

### **AddDirectory(AsarArchivePendingDirectory)**

Adds directory to the archive

```csharp
public AsarArchivePackerDataBuilder AddDirectory(AsarArchivePendingDirectory directory)
```

#### Parameters

`directory` [AsarArchivePendingDirectory](./craftersmine.asar.net.asararchivependingdirectory.md)<br>
Directory information to add

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

#### Exceptions

[DirectoryNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.directorynotfoundexception)<br>
When specified file in array is not found

### **RemoveFile(String)**

Removes file from pending files

```csharp
public AsarArchivePackerDataBuilder RemoveFile(string filePath)
```

#### Parameters

`filePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Path to file to remove

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

### **RemoveFile(FileInfo)**

Removes file from pending files

```csharp
public AsarArchivePackerDataBuilder RemoveFile(FileInfo fileInfo)
```

#### Parameters

`fileInfo` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
File info to remove

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

### **RemoveFile(AsarArchivePendingFile)**

Removes file from pending files

```csharp
public AsarArchivePackerDataBuilder RemoveFile(AsarArchivePendingFile file)
```

#### Parameters

`file` [AsarArchivePendingFile](./craftersmine.asar.net.asararchivependingfile.md)<br>
Pending file to remove

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

### **RemoveFiles(String[])**

Removes files from pending files

```csharp
public AsarArchivePackerDataBuilder RemoveFiles(String[] filePaths)
```

#### Parameters

`filePaths` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Path to file to remove

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

### **RemoveFiles(FileInfo[])**

Removes files from pending files

```csharp
public AsarArchivePackerDataBuilder RemoveFiles(FileInfo[] fileInfos)
```

#### Parameters

`fileInfos` [FileInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
File infos to remove

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

### **RemoveFiles(AsarArchivePendingFile[])**

Removes files from pending files

```csharp
public AsarArchivePackerDataBuilder RemoveFiles(AsarArchivePendingFile[] files)
```

#### Parameters

`files` [AsarArchivePendingFile[]](./craftersmine.asar.net.asararchivependingfile.md)<br>
Pending files to remove

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

### **RemoveDirectory(String)**

Removes directory from pending directories

```csharp
public AsarArchivePackerDataBuilder RemoveDirectory(string directoryPath)
```

#### Parameters

`directoryPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Directory path to remove

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

### **RemoveDirectory(DirectoryInfo)**

Removes directory from pending directories

```csharp
public AsarArchivePackerDataBuilder RemoveDirectory(DirectoryInfo directoryInfo)
```

#### Parameters

`directoryInfo` [DirectoryInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo)<br>
Directory info to remove

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

### **RemoveDirectory(AsarArchivePendingDirectory)**

Removes directory from pending directories

```csharp
public AsarArchivePackerDataBuilder RemoveDirectory(AsarArchivePendingDirectory directory)
```

#### Parameters

`directory` [AsarArchivePendingDirectory](./craftersmine.asar.net.asararchivependingdirectory.md)<br>
Pending directory to remove

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

### **PerformFileSort(Boolean)**

Sets if files need to be sorted before packing

```csharp
public AsarArchivePackerDataBuilder PerformFileSort(bool sort)
```

#### Parameters

`sort` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
 if sorting is needed

#### Returns

[AsarArchivePackerDataBuilder](./craftersmine.asar.net.asararchivepackerdatabuilder.md)<br>
 for creating ASAR archive

### **CreateArchiveData()**

Creates an ASAR archive data for packer

```csharp
public AsarArchivePackerData CreateArchiveData()
```

#### Returns

[AsarArchivePackerData](./craftersmine.asar.net.asararchivepackerdata.md)<br>
 that contains data for packing into ASAR archive
