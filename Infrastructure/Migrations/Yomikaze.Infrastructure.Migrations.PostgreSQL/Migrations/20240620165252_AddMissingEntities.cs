using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 61877523687972864m);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 61877523696361472m);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 61877523696361473m);

            migrationBuilder.DeleteData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 61877523696320515m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 61877523696320516m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 61877523696320517m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 61877523696320518m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 61877523696320519m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 61877523696320520m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 61877523696320521m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 61877523696320522m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 61877523696320523m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 61877523696320524m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 61877523696320525m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 61877523696320526m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 61877523696320527m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 61877523696320528m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 61877523696320529m);

            migrationBuilder.DeleteData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 61877523696320514m);

            migrationBuilder.AddColumn<int>(
                name: "views",
                table: "chapters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "comic_ratings",
                columns: table => new
                {
                    comic_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comic_ratings", x => new { x.comic_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_comic_ratings_comics_comic_id",
                        column: x => x.comic_id,
                        principalTable: "comics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comic_ratings_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment_reactions",
                columns: table => new
                {
                    comment_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    reaction_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment_reactions", x => new { x.comment_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_comment_reactions_comment_comment_id",
                        column: x => x.comment_id,
                        principalTable: "comment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comment_reactions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "unlocked_chapters",
                columns: table => new
                {
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    chapter_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unlocked_chapters", x => new { x.user_id, x.chapter_id });
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { 62223221000282112m, "3b667941-f476-49ce-bfd6-fb8d7398c17a", "Administrator", "ADMINISTRATOR" },
                    { 62223221004476416m, "e6e047a6-5624-45a1-9512-a6b7f8045906", "Publisher", "PUBLISHER" },
                    { 62223221004476417m, "df4f6dab-874d-427d-a5e9-b9d34368bbb7", "Reader", "READER" }
                });

            migrationBuilder.InsertData(
                table: "tag_categories",
                columns: new[] { "id", "creation_time", "name" },
                values: new object[,]
                {
                    { 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(6808), new TimeSpan(0, 0, 0, 0, 0)), "Genre" },
                    { 62223221004435459m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(7281), new TimeSpan(0, 0, 0, 0, 0)), "Theme" }
                });

            migrationBuilder.InsertData(
                table: "tags",
                columns: new[] { "id", "category_id", "creation_time", "description", "name" },
                values: new object[,]
                {
                    { 62223221004435460m, 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(7905), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on physical action, such as fighting, war, sports, or physical challenges.", "Action" },
                    { 62223221004435461m, 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(8390), new TimeSpan(0, 0, 0, 0, 0)), "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown.", "Adventure" },
                    { 62223221004435462m, 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(8394), new TimeSpan(0, 0, 0, 0, 0)), "A story with humorous narration or dialogue, intended to amuse the audience.", "Comedy" },
                    { 62223221004435463m, 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(8398), new TimeSpan(0, 0, 0, 0, 0)), "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist.", "Drama" },
                    { 62223221004435464m, 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(8400), new TimeSpan(0, 0, 0, 0, 0)), "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements.", "Fantasy" },
                    { 62223221004435465m, 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(8402), new TimeSpan(0, 0, 0, 0, 0)), "A story that evokes fear in both the characters and the audience.", "Horror" },
                    { 62223221004435466m, 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(8469), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around solving a puzzle or a crime.", "Mystery" },
                    { 62223221004435467m, 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(8472), new TimeSpan(0, 0, 0, 0, 0)), "A story that emphasizes the psychology of its characters and their unstable emotional states.", "Psychological" },
                    { 62223221004435468m, 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(8474), new TimeSpan(0, 0, 0, 0, 0)), "A story about love.", "Romance" },
                    { 62223221004435469m, 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(8477), new TimeSpan(0, 0, 0, 0, 0)), "A story that portrays a \"cut-out\" sequence of events in a character's life.", "Slice of Life" },
                    { 62223221004435470m, 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(8479), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around sports, such as baseball or basketball.", "Sports" },
                    { 62223221004435471m, 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(8481), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves supernatural elements, such as ghosts or demons.", "Supernatural" },
                    { 62223221004435472m, 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(8484), new TimeSpan(0, 0, 0, 0, 0)), "A story that is fast-paced and suspenseful, often involving a crime.", "Thriller" },
                    { 62223221004435473m, 62223221004435458m, new DateTimeOffset(new DateTime(2024, 6, 20, 16, 52, 51, 939, DateTimeKind.Unspecified).AddTicks(8486), new TimeSpan(0, 0, 0, 0, 0)), "A story that ends in a tragic or unhappy way.", "Tragedy" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_comic_ratings_user_id",
                table: "comic_ratings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_reactions_user_id",
                table: "comment_reactions",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comic_ratings");

            migrationBuilder.DropTable(
                name: "comment_reactions");

            migrationBuilder.DropTable(
                name: "unlocked_chapters");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 62223221000282112m);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 62223221004476416m);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 62223221004476417m);

            migrationBuilder.DeleteData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 62223221004435459m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 62223221004435460m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 62223221004435461m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 62223221004435462m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 62223221004435463m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 62223221004435464m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 62223221004435465m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 62223221004435466m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 62223221004435467m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 62223221004435468m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 62223221004435469m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 62223221004435470m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 62223221004435471m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 62223221004435472m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 62223221004435473m);

            migrationBuilder.DeleteData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 62223221004435458m);

            migrationBuilder.DropColumn(
                name: "views",
                table: "chapters");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { 61877523687972864m, "e7eed162-5fea-4356-b40f-38d567e21369", "Administrator", "ADMINISTRATOR" },
                    { 61877523696361472m, "3811b64b-f8ab-4c5b-8eac-fe47caf50440", "Publisher", "PUBLISHER" },
                    { 61877523696361473m, "cda6e8dc-b574-4710-b20f-35fe4a682d9d", "Reader", "READER" }
                });

            migrationBuilder.InsertData(
                table: "tag_categories",
                columns: new[] { "id", "creation_time", "name" },
                values: new object[,]
                {
                    { 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(1831), new TimeSpan(0, 0, 0, 0, 0)), "Genre" },
                    { 61877523696320515m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(2271), new TimeSpan(0, 0, 0, 0, 0)), "Theme" }
                });

            migrationBuilder.InsertData(
                table: "tags",
                columns: new[] { "id", "category_id", "creation_time", "description", "name" },
                values: new object[,]
                {
                    { 61877523696320516m, 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(2947), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on physical action, such as fighting, war, sports, or physical challenges.", "Action" },
                    { 61877523696320517m, 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(3483), new TimeSpan(0, 0, 0, 0, 0)), "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown.", "Adventure" },
                    { 61877523696320518m, 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(3487), new TimeSpan(0, 0, 0, 0, 0)), "A story with humorous narration or dialogue, intended to amuse the audience.", "Comedy" },
                    { 61877523696320519m, 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(3489), new TimeSpan(0, 0, 0, 0, 0)), "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist.", "Drama" },
                    { 61877523696320520m, 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(3491), new TimeSpan(0, 0, 0, 0, 0)), "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements.", "Fantasy" },
                    { 61877523696320521m, 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(3494), new TimeSpan(0, 0, 0, 0, 0)), "A story that evokes fear in both the characters and the audience.", "Horror" },
                    { 61877523696320522m, 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(3496), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around solving a puzzle or a crime.", "Mystery" },
                    { 61877523696320523m, 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(3498), new TimeSpan(0, 0, 0, 0, 0)), "A story that emphasizes the psychology of its characters and their unstable emotional states.", "Psychological" },
                    { 61877523696320524m, 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(3501), new TimeSpan(0, 0, 0, 0, 0)), "A story about love.", "Romance" },
                    { 61877523696320525m, 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(3503), new TimeSpan(0, 0, 0, 0, 0)), "A story that portrays a \"cut-out\" sequence of events in a character's life.", "Slice of Life" },
                    { 61877523696320526m, 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(3505), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around sports, such as baseball or basketball.", "Sports" },
                    { 61877523696320527m, 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(3507), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves supernatural elements, such as ghosts or demons.", "Supernatural" },
                    { 61877523696320528m, 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(3509), new TimeSpan(0, 0, 0, 0, 0)), "A story that is fast-paced and suspenseful, often involving a crime.", "Thriller" },
                    { 61877523696320529m, 61877523696320514m, new DateTimeOffset(new DateTime(2024, 6, 19, 17, 59, 11, 278, DateTimeKind.Unspecified).AddTicks(3511), new TimeSpan(0, 0, 0, 0, 0)), "A story that ends in a tragic or unhappy way.", "Tragedy" }
                });
        }
    }
}
