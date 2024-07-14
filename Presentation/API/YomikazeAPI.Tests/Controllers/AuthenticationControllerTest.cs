using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Threading.Tasks;
using Yomikaze.API.Main.Controllers;
using Yomikaze.API.Main.Services;
using Yomikaze.Application.Helpers.Security;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models;
using Yomikaze.Infrastructure;
using Yomikaze.Infrastructure.Context;

namespace YomikazeAPI.Tests.Controllers;

[TestFixture]
[TestOf(typeof(AuthenticationController))]
public class AuthenticationControllerTest
{
    
    private readonly IServiceCollection _services;
    private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddJsonFile("appsettings.json", false, true).Build();
    public AuthenticationControllerTest()
    {
        _services = new ServiceCollection();
        _configuration["Postgres:ConnectionStrings:YomikazeTest"] =
            "Host=192.168.1.8;Port=5432;Database=yomikaze_test;Username=yomikaze;Password=Yomikaze@SU24;";
        _services.AddDbContext<YomikazeDbContext>(Provider.Postgres, _configuration, "YomikazeTest");
        _services.AddIdentity<User, Role>().AddEntityFrameworkStores<YomikazeDbContext>();
        _services.AddScoped<DbContext, YomikazeDbContext>();
        _services.AddScoped<AuthenticationService>();
        _services.AddSingleton(new JwtConfiguration() { Secret = "The encryption algorithm 'HS256' requires a key size of at least '128' bits", });
        
        _services.AddLogging();
    }
    
    private readonly string defaultUsername = "administrator";
    private readonly string defaultPassword = "Admin@123";
    
    // on startup, migrations are applied to the database
    [SetUp]
    public async Task Setup()
    {
        await using var scope = _services.BuildServiceProvider().CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<YomikazeDbContext>();
        await context.Database.MigrateAsync();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        
        var user = new User
        {
            UserName = defaultUsername,
            Name = defaultUsername,
            Email = defaultUsername + "@yomikaze.com",
        };
        await userManager.CreateAsync(user, defaultPassword);
        await userManager.AddToRoleAsync(user, YomikazeDbContext.Default.Super.Name!);
        await userManager.AddToRoleAsync(user, YomikazeDbContext.Default.Administrator.Name!);
        await userManager.AddToRoleAsync(user, YomikazeDbContext.Default.DefaulRole.Name!);
    }   
    
    [TearDown]
    public async Task TearDown()
    {
        await using var scope = _services.BuildServiceProvider().CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<YomikazeDbContext>();
        await context.Database.EnsureDeletedAsync();
    }

    [Test]
    public async Task TestLogin_ShouldReturnToken()
    {
        // Arrange
        var service = _services.BuildServiceProvider();
        var authService = service.GetRequiredService<AuthenticationService>();
        var signInManager = service.GetRequiredService<SignInManager<User>>();
        var logger = service.GetRequiredService<ILogger<AuthenticationController>>();
        var controller = new AuthenticationController(signInManager, authService, logger);
        
        // Act
        var result = await controller.Login(new LoginModel { Username = defaultUsername, Password = defaultPassword });
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Value, Is.Not.Null);
        Assert.That(result.Value.Token, Is.Not.Null);
    }
    
    [Test]
    public async Task TestLogin_ShouldReturnValidationProblem()
    {
        // Arrange
        var service = _services.BuildServiceProvider();
        var authService = service.GetRequiredService<AuthenticationService>();
        var signInManager = service.GetRequiredService<SignInManager<User>>();
        var logger = service.GetRequiredService<ILogger<AuthenticationController>>();
        var controller = new AuthenticationController(signInManager, authService, logger);
        
        // Act
        var result = await controller.Login(new LoginModel { Username = "notfounduser", Password = "Admin@1234" });
        
        // Assert
        Assert.That(result.Result, Is.Not.Null);
        Assert.That(result.Result, Is.TypeOf<ObjectResult>());
        Assert.That((result.Result as ObjectResult)!.Value, Is.TypeOf<ValidationProblemDetails>());
    }
}