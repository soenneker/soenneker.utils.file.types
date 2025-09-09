using System.Collections.Generic;

namespace Soenneker.Utils.File.Types.Dtos;

public sealed class MediaFormatSet
{
    public List<string> VideoCodecs { get; set; } = null!;

    public List<string> AudioCodecs { get; set; } = null!;
}