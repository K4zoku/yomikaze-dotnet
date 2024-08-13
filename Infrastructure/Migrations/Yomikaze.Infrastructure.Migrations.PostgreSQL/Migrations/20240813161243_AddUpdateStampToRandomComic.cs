using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateStampToRandomComic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var randomComicView = """
                CREATE OR REPLACE VIEW get_random_comic AS
                SELECT * FROM
                    (SELECT * FROM comics TABLESAMPLE system_rows(10)) as c
                ORDER BY RANDOM() LIMIT 1;
            """; // Basically the same as the one in 20240723165557_AddComicRandomSampleView but the asterisk is adding new column behind the scene
            migrationBuilder.Sql(randomComicView);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
