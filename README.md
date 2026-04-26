# SB.Tar

A simple C# console application for exploring and parsing TAR files. This project provides functionality to read TAR archives, extract file headers, and display information about the contents. It's a giant WIP. At the moment it's a console project, but it should really be a 
console project for testing and a class-library (DLL).

## Features

- Supports USTAR (Unix Standard TAR) and GNU TAR formats
- Parses TAR headers including file names, sizes, permissions, and timestamps
- Lightweight library for TAR file manipulation

## TAR Format Standards

This project implements parsing for the standard TAR archive formats as documented in the following specifications:

### BSD tar Format (FreeBSD)
The BSD tar format is based on the original Unix tar implementation and includes several variants:
- **Old-Style Archive Format**: The original format from Version 7 AT&T UNIX, with 512-byte header records containing basic file metadata.
- **POSIX ustar Archives**: Standardized format (IEEE Std 1003.1-1988) with extended fields for user/group names and long pathnames.
- **GNU Tar Archives**: Extensions for sparse files, multi-volume archives, and additional metadata.
- **Pax Interchange Format**: Modern format (IEEE Std 1003.1-2001) using extended attributes for portable metadata storage.

For detailed specifications, see the [FreeBSD tar(5) manual page](https://man.freebsd.org/cgi/man.cgi?query=tar&apropos=0&sektion=5&manpath=FreeBSD+16.0-CURRENT&format=html).

### GNU tar Standard Format
The GNU tar implementation follows the POSIX standard with additional extensions:
- **Basic Tar Format**: Consists of 512-byte blocks with header and data sections.
- **POSIX Header Structure**: Defines fields like name, mode, uid, gid, size, mtime, checksum, typeflag, etc.
- **Type Flags**: Support for regular files, directories, symbolic links, hard links, device nodes, FIFOs, and extended headers.
- **Extensions**: Sparse file support, multi-volume archives, and pax interchange format.

For the complete GNU tar format specification, refer to the [GNU tar manual: Basic Tar Format](https://www.gnu.org/software/tar/manual/html_node/Standard.html).

## Requirements

- .NET 10.0 or later

## Building and Running

1. Clone or download the repository
2. Navigate to the project directory
3. Build the project:
   ```
   dotnet build
   ```
4. Run the application:
   ```
   dotnet run
   ```

## Usage

The current implementation demonstrates reading a TAR file and listing its contents:

```csharp
using SB.Tar.Lib;

var tf = Lib.SbTarFile.FromFile(@"path/to/your/tar/file.tar");

foreach (var h in tf.Headers)
{
    Console.WriteLine($"=> {h.Name} ({h.Size.ToInt32FromOctalString().ToFriendlySize()})");
}
```

Replace `"path/to/your/tar/file.tar"` with the actual path to your TAR file.

## Project Structure

- `Program.cs`: Main entry point demonstrating TAR file reading
- `Lib/`: Core library containing TAR parsing logic
  - `SbTarFile.cs`: Main TAR file reader
  - `SbTarHeader.cs`: Base header class
  - `SbTarUstarHeader.cs`: USTAR format header implementation
  - `SbTarGnuHeader.cs`: GNU format header implementation
  - `Extensions.cs`: Utility extensions for streams and size formatting

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
