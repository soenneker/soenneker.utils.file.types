using System.Collections.Generic;

namespace Soenneker.Utils.File.Types.Dtos;

public class MediaFormatSet
{
    public List<string> VideoCodecs { get; set; } = default!;

    public List<string> AudioCodecs { get; set; } = default!;
}