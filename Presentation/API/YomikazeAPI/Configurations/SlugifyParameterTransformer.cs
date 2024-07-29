using System.Text.RegularExpressions;

namespace Yomikaze.API.Main.Configurations;

public partial class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    private static Regex KebabRegex { get; } = MyRegex();

    public string? TransformOutbound(object? value)
    {
        return value == null ? null : KebabRegex.Replace(value.ToString() ?? string.Empty, "$1-$2").ToLower();
    }

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex MyRegex();
}