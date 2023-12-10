using Soenneker.Utils.File.Types.Dtos;
using System.Collections.Generic;
using System;
using System.IO;

namespace Soenneker.Utils.File.Types.Abstract;

/// <summary>
/// A utility library for various operations on specific file types
/// </summary>
public interface IFileTypeUtil
{
    List<FileInfo> GetAllVideoFiles(string directory);

    /// <summary>
    /// Make sure lowercase. Plex supported.
    /// </summary>
    Lazy<List<string>> SubtitleTypes { get; set; }

    /// <summary>
    /// Make sure lowercase. Plex supported.
    /// </summary>
    Lazy<List<string>> VideoContainers { get; set; }

    Lazy<List<string>> AudioTypes { get; set; }

    /// <summary>
    /// Plex supported
    /// </summary>
    Lazy<List<string>> ArtworkTypes { get; set; }

    Lazy<Dictionary<string, MediaFormatSet>> ContainerMediaSets { get; set; }

    /// <summary>
    /// Make sure lowercase.
    /// </summary>
    bool IsSubtitleExtension(string extension);

    /// <summary>
    /// Make sure lowercase.
    /// </summary>
    bool IsVideoExtension(string extension);

    bool IsVideoFile(string filename);

    bool IsImageFile(string filename);
}