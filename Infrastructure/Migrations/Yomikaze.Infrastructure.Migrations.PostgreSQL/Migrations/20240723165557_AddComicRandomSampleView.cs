using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddComicRandomSampleView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string view = """
                                  CREATE OR REPLACE VIEW get_random_comic AS
                                  SELECT * FROM
                                    (SELECT * FROM comics TABLESAMPLE system_rows(10)) as c
                                  ORDER BY RANDOM() LIMIT 1;
                          """;
            migrationBuilder.Sql(view);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS get_random_comic");
        }
    }
}
