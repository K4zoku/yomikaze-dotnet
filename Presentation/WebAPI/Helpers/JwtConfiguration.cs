namespace Yomikaze.WebAPI.Helpers
{
    public class JwtConfiguration
    {
        public const string SectionName = "JWT";
        public string Audience { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Secret { get; set; } = default!;
    }
}
