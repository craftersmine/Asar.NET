# AsarFileStream

Namespace: craftersmine.Asar.Net

Represents a stream of data from file within ASAR archive

```csharp
public class AsarFileStream : System.IO.Stream, System.IDisposable, System.IAsyncDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MarshalByRefObject](https://docs.microsoft.com/en-us/dotnet/api/system.marshalbyrefobject) → [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream) → [AsarFileStream](./craftersmine.asar.net.asarfilestream.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable), [IAsyncDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncdisposable)

## Properties

### **CanRead**

Gets  if you can read from this stream

```csharp
public bool CanRead { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **CanSeek**

Gets  if you can seek this stream

```csharp
public bool CanSeek { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **CanWrite**

Gets  if you can write to this stream

```csharp
public bool CanWrite { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Length**

Gets total length of stream

```csharp
public long Length { get; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **Position**

Gets current position within stream

```csharp
public long Position { get; set; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **CanTimeout**

```csharp
public bool CanTimeout { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ReadTimeout**

```csharp
public int ReadTimeout { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **WriteTimeout**

```csharp
public int WriteTimeout { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Methods

### **Flush()**

```csharp
public void Flush()
```

### **Read(Byte[], Int32, Int32)**

```csharp
public int Read(Byte[] buffer, int offset, int count)
```

#### Parameters

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`offset` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`count` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Seek(Int64, SeekOrigin)**

```csharp
public long Seek(long offset, SeekOrigin origin)
```

#### Parameters

`offset` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

`origin` [SeekOrigin](https://docs.microsoft.com/en-us/dotnet/api/system.io.seekorigin)<br>

#### Returns

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **SetLength(Int64)**

```csharp
public void SetLength(long value)
```

#### Parameters

`value` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **Write(Byte[], Int32, Int32)**

```csharp
public void Write(Byte[] buffer, int offset, int count)
```

#### Parameters

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`offset` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`count` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **CopyTo(Stream)**

```csharp
public void CopyTo(Stream destination)
```

#### Parameters

`destination` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

### **CopyTo(Stream, Int32)**

```csharp
public void CopyTo(Stream destination, int bufferSize)
```

#### Parameters

`destination` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`bufferSize` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **CopyToAsync(Stream)**

```csharp
public Task CopyToAsync(Stream destination)
```

#### Parameters

`destination` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **CopyToAsync(Stream, Int32)**

```csharp
public Task CopyToAsync(Stream destination, int bufferSize)
```

#### Parameters

`destination` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`bufferSize` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **CopyToAsync(Stream, Int32, CancellationToken)**

```csharp
public Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
```

#### Parameters

`destination` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`bufferSize` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`cancellationToken` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
