using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace Yomikaze.API.Main;

public class ReplacedGoogleHandler : GoogleHandler
{
    [Obsolete("Obsolete")]
    public ReplacedGoogleHandler(IOptionsMonitor<GoogleOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    public ReplacedGoogleHandler(IOptionsMonitor<GoogleOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
    {
    }

    protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
    {
        UriBuilder builder = new(redirectUri) { Scheme = "https", Host = "yomikaze.org", Port = 443 };
        return base.BuildChallengeUrl(properties, builder.Uri.ToString());
    }
}