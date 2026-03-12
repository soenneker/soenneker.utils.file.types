using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.File.Abstract;
using Soenneker.Utils.File.Types.Abstract;
using Soenneker.Utils.File.Types.Dtos;

namespace Soenneker.Utils.File.Types;

///<inheritdoc cref="IFileTypeUtil"/>
public sealed class FileTypeUtil : IFileTypeUtil
{
    // Fast, process-wide lookup tables (no per-instance cost)
    private static readonly HashSet<string> _videoExts = new(StringComparer.OrdinalIgnoreCase)
    {
        ".asf", ".avi", ".mov", ".mkv", ".mp4", ".wmv", ".m2ts", ".ts", ".mpegts", ".3gpp",
        ".flv", ".wtv", ".mpeg", ".mpg", ".m4v", ".3gp", ".webm", ".divx"
    };

    private static readonly HashSet<string> _subtitleExts = new(StringComparer.OrdinalIgnoreCase) { ".srt", ".smi", ".ssa", ".ass", ".vtt" };

    private static readonly HashSet<string> _artworkExts = new(StringComparer.OrdinalIgnoreCase) { ".png", ".jpeg", ".jpg", ".tbn", ".ext" };

    private static readonly HashSet<string> _audioExts = new(StringComparer.OrdinalIgnoreCase) { ".aac", ".alac", ".e-ac3", ".flac", ".mp3", ".m4a", ".wav" };

    private static readonly Dictionary<string, MediaFormatSet?> _containerMediaSets = new(StringComparer.OrdinalIgnoreCase)
    {
        [".mkv"] = new MediaFormatSet
        {
            AudioCodecs =
            [
                "aac",
                "ac3",
                "flac",
                "e-ac3",
                "flac",
                "mp2",
                "mp3"
            ],

            VideoCodecs =
            [
                "h264",
                "hevc",
                "h265",
                "mpeg4",
                "msmpeg4v2",
                "msmpeg4v3",
                "vc1",
                "vp9",
                "wmv3"
            ]
        }
    };

    private readonly ILogger<FileTypeUtil> _logger;
    private readonly IFileUtil _fileUtil;

    public FileTypeUtil(ILogger<FileTypeUtil> logger, IFileUtil fileUtil)
    {
        _logger = logger;
        _fileUtil = fileUtil;
    }

    public async ValueTask<List<FileInfo>> GetAllVideoFiles(string directory)
    {
        List<FileInfo> files = await _fileUtil.GetAllFileInfoInDirectoryRecursivelySafe(directory).NoSync();

        _logger.LogDebug("Filtering for video files in {dir}. Total files: {count}", directory, files.Count);

        // Upper bound: at most `files.Count` videos
        var result = new List<FileInfo>(files.Count);

        // Manual loop for speed and to avoid iterator allocs
        for (int i = 0; i < files.Count; i++)
        {
            FileInfo fi = files[i];
            // Use Name + GetExtension to minimize substring size vs FullName
            if (IsVideoExtension(Path.GetExtension(fi.Name)))
                result.Add(fi);
        }

        // If a lot were filtered out, trim backing array
        if (result.Capacity > result.Count + 32)
            result.TrimExcess();

        _logger.LogDebug("Finished filtering. Video count: {count}", result.Count);
        return result;
    }

    public async IAsyncEnumerable<FileInfo> EnumerateVideoFiles(string directory)
    {
        List<FileInfo> files = await _fileUtil.GetAllFileInfoInDirectoryRecursivelySafe(directory).NoSync();

        for (int i = 0; i < files.Count; i++)
        {
            FileInfo fi = files[i];

            if (IsVideoExtension(Path.GetExtension(fi.Name)))
                yield return fi;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSubtitleExtension(string extension) => _subtitleExts.Contains(extension);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsVideoExtension(string extension) => _videoExts.Contains(extension);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsImageExtension(string extension) => _artworkExts.Contains(extension);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsAudioExtension(string extension) => _audioExts.Contains(extension);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsVideoFile(string pathOrFileName) => _videoExts.Contains(Path.GetExtension(pathOrFileName));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsImageFile(string pathOrFileName) => _artworkExts.Contains(Path.GetExtension(pathOrFileName));

    public bool? TryGetContainerMediaSet(string extension, out MediaFormatSet? set) => _containerMediaSets.TryGetValue(extension, out set);
}