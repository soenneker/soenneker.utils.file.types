using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using Soenneker.Utils.File.Types.Abstract;
using Soenneker.Utils.File.Types.Dtos;
using Soenneker.Utils.FileSync.Abstract;

namespace Soenneker.Utils.File.Types;

///<inheritdoc cref="IFileTypeUtil"/>
public class FileTypeUtil : IFileTypeUtil
{
    public Lazy<List<string>> VideoContainers { get; set; }
    public Lazy<Dictionary<string, MediaFormatSet>> ContainerMediaSets { get; set; }
    public Lazy<List<string>> SubtitleTypes { get; set; }
    public Lazy<List<string>> ArtworkTypes { get; set; }
    public Lazy<List<string>> AudioTypes { get; set; }

    private readonly ILogger<FileTypeUtil> _logger;
    private readonly IFileUtilSync _fileUtilSync;

    public FileTypeUtil(ILogger<FileTypeUtil> logger, IFileUtilSync fileUtilSync)
    {
        _logger = logger;
        _fileUtilSync = fileUtilSync;

        SubtitleTypes = new Lazy<List<string>>(() => new List<string>
        {
            ".srt",
            ".smi",
            ".ssa",
            ".ass",
            ".vtt"
        });

        VideoContainers = new Lazy<List<string>>(() => new List<string>
        {
            ".asf",
            ".avi",
            ".mov",
            ".mkv",
            ".mp4",
            ".wmv",
            ".m2ts",
            ".ts",
            ".mpegts",
            ".3gpp",
            ".flv",
            ".wtv",
            ".mpeg",
            ".mpg",
            ".m4v",
            ".3gp",
            ".webm",
            ".divx"
        });

        ArtworkTypes = new Lazy<List<string>>(() => new List<string>
        {
            ".png",
            ".jpeg",
            ".jpg",
            ".tbn",
            ".ext"
        });

        AudioTypes = new Lazy<List<string>>(() => new List<string>
        {
            ".aac",
            ".alac",
            ".e-ac3",
            ".flac",
            ".mp3",
            ".m4a",
            ".wav"
        });

        ContainerMediaSets = new Lazy<Dictionary<string, MediaFormatSet>>(() => new Dictionary<string, MediaFormatSet>()
        {
            {
                ".mkv", new MediaFormatSet
                {
                    AudioCodecs = new List<string> {"aac", "ac3", "flac", "e-ac3", "flac", "mp2", "mp3"},
                    VideoCodecs = new List<string>
                    {
                        "h264", "hevc", "h265", "mpeg4", "msmpeg4v2", "msmpeg4v3", "vc1", "vp9", "wmv3"
                    }
                }
            }
        });
    }

    public List<FileInfo> GetAllVideoFiles(string directory)
    {
        List<FileInfo> files = _fileUtilSync.GetAllFileInfoInDirectoryRecursivelySafe(directory);

        _logger.LogDebug("Beginning to filter for video files...");

        var result = new List<FileInfo>();

        foreach (FileInfo file in files)
        {
            if (IsVideoFile(file.FullName))
                result.Add(file);
        }

        _logger.LogDebug("Finished filtering for video files, number: {number}", result.Count);

        return result;
    }

    public bool IsSubtitleExtension(string extension)
    {
        return SubtitleTypes.Value.Contains(extension);
    }

    public bool IsVideoExtension(string extension)
    {
        return VideoContainers.Value.Contains(extension);
    }

    public bool IsVideoFile(string filename)
    {
        string extension = Path.GetExtension(filename).ToLower();

        return VideoContainers.Value.Contains(extension);
    }

    public bool IsImageFile(string filename)
    {
        string extension = Path.GetExtension(filename).ToLower();

        return ArtworkTypes.Value.Contains(extension);
    }
}