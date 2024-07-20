using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddGetComicViewsDbFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "fk_comic_views_comics_comic_id",
                table: "comic_views",
                column: "comic_id",
                principalTable: "comics",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            string getComicViews = """
                        CREATE OR REPLACE FUNCTION get_comic_views(id numberic, start_time timestamp with time zone, end_time timestamp with time zone)
                    RETURNS TABLE (views bigint) LANGUAGE SQL AS $$
                        SELECT sum(views) as views
                        FROM comic_views
                        WHERE comic_id = id
                        AND creation_time >= start_time
                        AND creation_time <= end_time
                        GROUP BY comic_id
                $$;
            """;
            migrationBuilder.Sql(getComicViews);
            string getComicsViews = """
                CREATE OR REPLACE FUNCTION get_comics_views(start_time timestamp with time zone, end_time timestamp with time zone)
                    RETURNS TABLE (id bigint, views bigint) LANGUAGE SQL AS $$
                        SELECT comic_id as id, sum(views) as views
                        FROM comic_views
                        WHERE creation_time >= start_time
                        AND creation_time <= end_time
                        GROUP BY comic_id
                $$;
            """;
            migrationBuilder.Sql(getComicsViews);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_comic_views_comics_comic_id",
                table: "comic_views");
            
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS get_comic_views(bigint, timestamp with time zone, timestamp with time zone)");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS get_comics_views(timestamp with time zone, timestamp with time zone)");
        }
    }
}
