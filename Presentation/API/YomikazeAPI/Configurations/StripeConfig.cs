namespace Yomikaze.API.Main.Configurations;

public class StripeConfig
{
    public string WebhookSecret { get; set; } = default!;

    public string PublishableKey { get; set; } = default!;
}