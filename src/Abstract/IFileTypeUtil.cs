using System.Collections.Generic;
using System.IO;
using Soenneker.Utils.File.Types.Dtos;

namespace Soenneker.Utils.File.Types.Abstract;

/// <summary>
/// High-performance utilities for determining file types and enumerating media files.
/// </summary>
/// <remarks>
/// Implementations should avoid unnecessary allocations and favor fast lookups.
/// See <see cref="Soenneker.Utils.File.Types.FileTypeUtil"/> for a default implementation.
/// </remarks>
public interface IFileTypeUtil
{
    /// <summary>
    /// Returns all video files under the specified directory (recursively).
    /// </summary>
    /// <param name="directory">The root directory to scan.</param>
    /// <returns>
    /// A materialized <see cref="List{T}"/> of <see cref="FileInfo"/> entries whose extensions are recognized as video.
    /// </returns>
    /// <remarks>
    /// Allocates a single list; for lower allocation and streaming semantics, prefer <see cref="EnumerateVideoFiles(string)"/>.
    /// </remarks>
    List<FileInfo> GetAllVideoFiles(string directory);

    /// <summary>
    /// Enumerates video files under the specified directory (recursively) without allocating a result list.
    /// </summary>
    /// <param name="directory">The root directory to scan.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> sequence of <see cref="FileInfo"/> entries whose extensions are recognized as video.
    /// </returns>
    IEnumerable<FileInfo> EnumerateVideoFiles(string directory);

    /// <summary>
    /// Determines whether an extension represents a known subtitle type.
    /// </summary>
    /// <param name="extension">The file extension (e.g., ".srt"). Case-insensitive.</param>
    /// <returns><c>true</c> if the extension is a known subtitle type; otherwise, <c>false</c>.</returns>
    bool IsSubtitleExtension(string extension);

    /// <summary>
    /// Determines whether an extension represents a known video type.
    /// </summary>
    /// <param name="extension">The file extension (e.g., ".mp4"). Case-insensitive.</param>
    /// <returns><c>true</c> if the extension is a known video type; otherwise, <c>false</c>.</returns>
    bool IsVideoExtension(string extension);

    /// <summary>
    /// Determines whether an extension represents a known image (artwork) type.
    /// </summary>
    /// <param name="extension">The file extension (e.g., ".png"). Case-insensitive.</param>
    /// <returns><c>true</c> if the extension is a known image type; otherwise, <c>false</c>.</returns>
    bool IsImageExtension(string extension);

    /// <summary>
    /// Determines whether an extension represents a known audio type.
    /// </summary>
    /// <param name="extension">The file extension (e.g., ".flac"). Case-insensitive.</param>
    /// <returns><c>true</c> if the extension is a known audio type; otherwise, <c>false</c>.</returns>
    bool IsAudioExtension(string extension);

    /// <summary>
    /// Determines whether the specified path or file name refers to a video file (by extension).
    /// </summary>
    /// <param name="pathOrFileName">A full or relative path, or just a file name.</param>
    /// <returns><c>true</c> if the file extension is a known video type; otherwise, <c>false</c>.</returns>
    bool IsVideoFile(string pathOrFileName);

    /// <summary>
    /// Determines whether the specified path or file name refers to an image (artwork) file (by extension).
    /// </summary>
    /// <param name="pathOrFileName">A full or relative path, or just a file name.</param>
    /// <returns><c>true</c> if the file extension is a known image type; otherwise, <c>false</c>.</returns>
    bool IsImageFile(string pathOrFileName);

    /// <summary>
    /// Attempts to get the container media format set (supported audio/video codecs) for a given container extension.
    /// </summary>
    /// <param name="extension">The container file extension (e.g., ".mkv"). Case-insensitive.</param>
    /// <param name="set">When this method returns, contains the <see cref="MediaFormatSet"/> if found; otherwise, the default value.</param>
    /// <returns><c>true</c> if a known container mapping exists; otherwise, <c>false</c>.</returns>
    bool TryGetContainerMediaSet(string extension, out MediaFormatSet set);
}