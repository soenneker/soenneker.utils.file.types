using System.Collections.Generic;

namespace Soenneker.Utils.File.Types.Dtos;

/// <summary>
/// Represents the media format set.
/// </summary>
public sealed class MediaFormatSet
{
    /// <summary>
    /// Gets or sets video codecs.
    /// </summary>
    public List<string> VideoCodecs { get; set; } = null!;

    /// <summary>
    /// Gets or sets audio codecs.
    /// </summary>
    public List<string> AudioCodecs { get; set; } = null!;
}