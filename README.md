[![](https://img.shields.io/nuget/v/Soenneker.Utils.File.Types.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Utils.File.Types/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.file.types/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.utils.file.types/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Utils.File.Types.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Utils.File.Types/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.file.types/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.utils.file.types/actions/workflows/codeql.yml)

# Soenneker.Utils.File.Types

Case-insensitive extension classification for common video, audio, subtitle, and artwork files, plus recursive video discovery.

## Installation

```bash
dotnet add package Soenneker.Utils.File.Types
```

## Registration

```csharp
builder.Services.AddFileTypeUtilAsSingleton();
```

Scoped registration is also available with `AddFileTypeUtilAsScoped()`.

## Classify extensions and paths

```csharp
bool videoExtension = fileTypes.IsVideoExtension(".mkv");
bool audioExtension = fileTypes.IsAudioExtension(".flac");
bool subtitleExtension = fileTypes.IsSubtitleExtension(".vtt");
bool artworkExtension = fileTypes.IsImageExtension(".jpg");

bool videoFile = fileTypes.IsVideoFile(@"C:\media\movie.MP4");
bool imageFile = fileTypes.IsImageFile("poster.png");
```

Extension methods expect the leading period; pass `.mp4`, not `mp4`. File methods extract the extension from the supplied path or name. Matching is ordinal and case-insensitive.

Classification is based only on the name. It does not inspect file signatures, MIME types, codecs, or file contents and must not be used as a security validation boundary for uploads.

## Discover video files

```csharp
List<FileInfo> videos = await fileTypes.GetAllVideoFiles(mediaDirectory);

await foreach (FileInfo video in fileTypes.EnumerateVideoFiles(mediaDirectory))
{
    Console.WriteLine(video.FullName);
}
```

Both APIs first materialize a recursive file snapshot, skipping inaccessible directories and reparse points, and then filter it by extension. `EnumerateVideoFiles()` exposes async iteration but does not provide true filesystem streaming or cancellation.

## Container codecs

```csharp
if (fileTypes.TryGetContainerMediaSet(".mkv", out MediaFormatSet? formats) == true)
{
    IReadOnlyList<string> videoCodecs = formats.VideoCodecs;
    IReadOnlyList<string> audioCodecs = formats.AudioCodecs;
}
```

Container lookup requires the leading period and only contains explicitly defined mappings. The returned codec lists are defensive copies and can be modified without changing later lookups. They describe the library's known mapping, not the codecs actually present in a particular file.
