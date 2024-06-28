using System.Text.RegularExpressions;

namespace Yomikaze.API.Main.Configurations;

public partial class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value == null ? null : KebabRegex.Replace(value.ToString() ?? string.Empty, "$1-$2").ToLower();
    }

    private static Regex KebabRegex { get; } = new("([a-z])([A-Z])");
}