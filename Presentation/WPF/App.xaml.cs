using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Windows;
using Yomikaze.WPF;

namespace WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public const string API_SERVER = "http://localhost:5179";

    public static IServiceProvider Services { get; private set; }

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        Services = services.BuildServiceProvider();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<Login>();
        services.AddTransient<ManageComics>();
        services.AddTransient<Upload>();
        services.AddTransient<ComicForm>();
        HttpClient httpClient = new()
        {
            BaseAddress = new Uri(API_SERVER)
        };
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        services.AddSingleton(httpClient);
        services.AddSingleton<YomikazeClient>();
    }

    private void OnStart(object sender, StartupEventArgs e)
    {
        var loginWindow = Services.GetRequiredService<Login>();
        loginWindow.Show();
    }
}
