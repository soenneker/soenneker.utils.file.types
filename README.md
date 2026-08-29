[![](https://img.shields.io/nuget/v/Soenneker.Utils.File.Types.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Utils.File.Types/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.file.types/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.utils.file.types/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Utils.File.Types.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Utils.File.Types/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.file.types/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.utils.file.types/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Utils.File.Types
A utility library for various operations on specific file types.

## Installation

```bash
dotnet add package Soenneker.Utils.File.Types
```

## Quick start

```csharp
using Soenneker.Utils.File.Types.Registrars;

services.AddFileTypeUtilAsSingleton();
```

Then inject `IFileTypeUtil` wherever you need it.

## Common operations

- `GetAllVideoFiles()` - Returns all video files under the specified directory (recursively). Allocates a single list; for lower allocation and streaming semantics, prefer `EnumerateVideoFiles(string)`.
- `EnumerateVideoFiles()` - Enumerates video files under the specified directory (recursively) without allocating a result list.
- `IsSubtitleExtension()` - Determines whether an extension represents a known subtitle type.
- `IsVideoExtension()` - Determines whether an extension represents a known video type.
- `IsImageExtension()` - Determines whether an extension represents a known image (artwork) type.
- `IsAudioExtension()` - Determines whether an extension represents a known audio type.
- `IsVideoFile()` - Determines whether the specified path or file name refers to a video file (by extension).
- `IsImageFile()` - Determines whether the specified path or file name refers to an image (artwork) file (by extension).
- `TryGetContainerMediaSet()` - Attempts to get the container media format set (supported audio/video codecs) for a given container extension. Returns `true` if a known container mapping exists; otherwise, `false`.
