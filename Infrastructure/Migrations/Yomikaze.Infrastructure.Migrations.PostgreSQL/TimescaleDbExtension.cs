using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL;

public static class TimescaleDbExtension
{
    public static OperationBuilder<SqlOperation> CreateHyperTable(this MigrationBuilder migrationBuilder, string tableName, string timeColumn)
    {
        return migrationBuilder.Sql($"SELECT create_hypertable('{tableName}', '{timeColumn}');");
    }
}