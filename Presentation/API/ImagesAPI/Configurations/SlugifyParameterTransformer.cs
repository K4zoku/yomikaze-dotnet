using System.Text.RegularExpressions;

namespace Yomikaze.API.CDN.Images.Configurations;

public partial class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value == null ? null : KebabRegex.Replace(value.ToString() ?? string.Empty, "$1-$2").ToLower();
    }

    private static Regex KebabRegex { get; } = MyRegex();

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex MyRegex();
}