namespace Yomikaze.Infrastructure;

public record Provider(string Name, string Assembly)
{
    public static Provider Postgres => new(nameof(Postgres), Migrations.PostgreSQL.Marker.AssemblyName);
    public static Provider SqlServer => new(nameof(SqlServer), Migrations.SQLServer.Marker.AssemblyName);
    
    public static Provider FromName(string? name) => name switch
    {
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