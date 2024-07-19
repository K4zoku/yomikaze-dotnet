using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Stripe;
using Stripe.Checkout;
using System.Data.Common;
using Yomikaze.API.Main.Configurations;
using Yomikaze.API.Main.Services;
using Yomikaze.Application.Data.Configs;
using Yomikaze.Application.Helpers;
using Yomikaze.Application.Helpers.Security;
using Yomikaze.Infrastructure;
using Yomikaze.Infrastructure.Context;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

Provider provider = Provider.FromName(configuration.GetValue("provider", Provider.Postgres.Name));
services.AddDbContext<YomikazeDbContext>(provider, configuration, "Yomikaze");
services.AddScoped<DbContext, YomikazeDbContext>();

services.AddScoped<IRepository<Comic>, ComicRepository>();
services.AddScoped<ComicRepository>();
services.AddScoped<IRepository<Chapter>, ChapterRepository>();
services.AddScoped<ChapterRepository>();
services.AddScoped<IRepository<Tag>, TagRepository>();
services.AddScoped<TagRepository>();
services.AddScoped<IRepository<TagCategory>, TagCategoryRepository>();
services.AddScoped<TagCategoryRepository>();
services.AddScoped<IRepository<HistoryRecord>, HistoryRepository>();
services.AddScoped<HistoryRepository>();
services.AddScoped<IRepository<LibraryEntry>, LibraryRepository>();
services.AddScoped<LibraryRepository>();
services.AddScoped<IRepository<LibraryCategory>, LibraryCategoryRepository>();
services.AddScoped<LibraryCategoryRepository>();
services.AddScoped<IRepository<ComicComment>, ComicCommentRepository>();
services.AddScoped<ComicCommentRepository>();
services.AddScoped<IRepository<CoinPricing>, CoinPricingRepository>();
services.AddScoped<CoinPricingRepository>();
services.AddScoped<AuthenticationService>();

// Stripe services
services.AddSingleton(new SessionService());
services.AddSingleton(new PriceService());
services.AddSingleton(new PaymentIntentService());
StripeConfig stripeConfig = configuration.GetRequiredSection("Stripe").Get<StripeConfig>() ?? throw new InvalidOperationException("Stripe configuration not found");
services.AddSingleton(stripeConfig);

services.AddRouting(options => options.LowercaseUrls = true);
services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    };
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
    JsonConvert.DefaultSettings = () => options.SerializerSettings;
});

services.AddYomikazeIdentity();
services.UpgradePasswordSecurity().UseArgon2<User>();

var googleClientId = configuration["Authentication:Google:ClientId"];
var googleClientSecret = configuration["Authentication:Google:ClientSecret"];
if (string.IsNullOrWhiteSpace(googleClientId) || string.IsNullOrWhiteSpace(googleClientSecret))
{
    throw new InvalidOperationException("Google authentication configuration not found");
}
services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
});

JwtConfiguration jwt = configuration
                           .GetRequiredSection(JwtConfiguration.SectionName)
                           .Get<JwtConfiguration>()
                       ?? throw new InvalidOperationException("Could not read JWT Configuration");
services.AddSingleton(jwt);
services.AddJwtBearerAuthentication(jwt)
    .AddGoogle(options =>
    {
        options.ClientId = googleClientId;
        options.ClientSecret = googleClientSecret;
        options.CallbackPath = "/google";
    });

services.AddTransient<IAuthorizationHandler, SidValidationAuthorizationHandler>();

services.AddEndpointsApiExplorer();
services.AddSwaggerGenWithJwt();
services.AddSwaggerGenNewtonsoftSupport();
services.AddPublicCors();
var redis = configuration.GetRequiredSection("Redis").GetConnectionString("Yomikaze") ?? throw new InvalidOperationException("Redis configuration not found");
services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redis;
    options.InstanceName = "Yomikaze:";
});
services.AddAuthorizationBuilder().SetDefaultPolicy(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
services.AddAutoMapper(typeof(YomikazeMapper));

StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"] ?? throw new InvalidOperationException("Stripe secret key not found");

services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = await GoogleCredential.GetApplicationDefaultAsync(),
    ProjectId = "yomikaze-fcm",
}));

WebApplication app = builder.Build();
IWebHostEnvironment env = app.Environment;
app.UseForwardedHeaders();
if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("Public");
}
else
{
    app.UseCors("Yomikaze");
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

IServiceScope scope = app.Services.CreateScope();
IServiceProvider serviceProvider = scope.ServiceProvider;
YomikazeDbContext dbContext = serviceProvider.GetRequiredService<YomikazeDbContext>();
ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
await dbContext.Database.MigrateAsync();
UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
if (!await dbContext.Roles.AnyAsync())
{
    logger.LogInformation("No roles found, adding default roles");
    await dbContext.Roles.AddRangeAsync(YomikazeDbContext.Default.Roles);
}
if (!await userManager.Users.AnyAsync())
{
    logger.LogInformation("No users found, adding default admin user");
    User admin = new()
    {
        UserName = app.Configuration["Admin:Username"] ?? "administrator",
        Name = "Administrator",
        Email = app.Configuration["Admin:Email"] ?? "administrator@yomikaze.org",
        EmailConfirmed = true
    };
    IdentityResult result = await userManager.CreateAsync(admin, app.Configuration["Admin:Password"] ?? "Admin@123");
    if (!result.Succeeded)
    {
        throw new InvalidOperationException("Could not create default admin user");
    }
    await userManager.AddToRoleAsync(admin, "Super");
    await userManager.AddToRoleAsync(admin, "Administrator");
}

try
{
    if (!await dbContext.TagCategories.AnyAsync())
    {
        logger.LogInformation("No tag categories found, adding default tag categories");
        await dbContext.TagCategories.AddRangeAsync(YomikazeDbContext.Default.TagCategories);
        await dbContext.SaveChangesAsync();
    }

    if (!await dbContext.Tags.AnyAsync())
    {
        logger.LogInformation("No tags found, adding default tags");
        await dbContext.Tags.AddRangeAsync(YomikazeDbContext.Default.Tags);
        await dbContext.SaveChangesAsync();
    }
}
catch (DbException)
{
    #pragma warning disable
    logger.LogWarning("Could not add default tags and tag categories, but system will continue to run.");
    #pragma warning restore
}
/* Example of using a stored function   
var f = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(30)).ToUniversalTime();
var t = DateTimeOffset.Now.ToUniversalTime();
var r = dbContext.GetComicsViewsResult(f, t)
    .Join(dbContext.Comics, result => result.Id, comic => comic.Id,
        (result, comic) => new { Id = comic.Id, Name = comic.Name, result.Views, FromDate = f, ToDate = t })
    .OrderByDescending(result => result.Views);
foreach (var result in r)
{
    logger.LogInformation("Comic {Name} ({Id}) has {Views} views", result.Name, result.Id, result.Views);
    logger.LogInformation("From {FromDate} to {ToDate}", result.FromDate.ToLocalTime(), result.ToDate.ToLocalTime());
}
*/
    

await app.RunAsync();