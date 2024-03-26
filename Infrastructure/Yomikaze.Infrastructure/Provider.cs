namespace Yomikaze.Infrastructure;

public record Provider(string Name, string Assembly)
{
    public static Provider Sqlite => new(nameof(Sqlite), typeof(Migrations.Sqlite.Marker).Assembly.GetName().Name!);
    public static Provider Postgres => new(nameof(Postgres), typeof(Migrations.PostgreSQL.Marker).Assembly.GetName().Name!);
    public static Provider SqlServer => new(nameof(SqlServer), typeof(Migrations.SQLServer.Marker).Assembly.GetName().Name!);
    
    public static Provider FromName(string? name) => name switch
    {
        nameof(Sqlite) => Sqlite,
        nameof(Postgres) => Postgres,
        nameof(SqlServer) => SqlServer,
        _ => throw new ArgumentException("Invalid provider")
    };

    public string GetConnectionString(IConfiguration configuration, string key)
    {
        var section = configuration.GetRequiredSection(Name);
        var connectionString = section.GetConnectionString(key);
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException($"Connection string for {Name} is required");
        }
        return connectionString;
    }
}