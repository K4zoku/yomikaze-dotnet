using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yomikaze.Infrastructure.Migrations.SQLServer.Yomikaze
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    avatar = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    creation_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "comics",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    aliases = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    cover = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    banner = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    published = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ended = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    creation_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comics", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    creation_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    content = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    read = table.Column<bool>(type: "bit", nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chapters",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    index = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    comic_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chapters", x => x.id);
                    table.ForeignKey(
                        name: "FK_chapters_comics_comic_id",
                        column: x => x.comic_id,
                        principalTable: "comics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comic_author",
                columns: table => new
                {
                    comic_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    author_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comic_author", x => new { x.comic_id, x.author_id });
                    table.ForeignKey(
                        name: "FK_comic_author_authors_author_id",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comic_author_comics_comic_id",
                        column: x => x.comic_id,
                        principalTable: "comics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "library_entries",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    comic_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_library_entries", x => x.id);
                    table.ForeignKey(
                        name: "FK_library_entries_comics_comic_id",
                        column: x => x.comic_id,
                        principalTable: "comics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comic_genre",
                columns: table => new
                {
                    comic_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    genre_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comic_genre", x => new { x.comic_id, x.genre_id });
                    table.ForeignKey(
                        name: "FK_comic_genre_comics_comic_id",
                        column: x => x.comic_id,
                        principalTable: "comics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comic_genre_genres_genre_id",
                        column: x => x.genre_id,
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    content = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    comic_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    chapter_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    reply_to_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    creation_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_comments_chapters_chapter_id",
                        column: x => x.chapter_id,
                        principalTable: "chapters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_comics_comic_id",
                        column: x => x.comic_id,
                        principalTable: "comics",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_comments_comments_reply_to_id",
                        column: x => x.reply_to_id,
                        principalTable: "comments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "history_records",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    chapter_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_history_records", x => x.id);
                    table.ForeignKey(
                        name: "FK_history_records_chapters_chapter_id",
                        column: x => x.chapter_id,
                        principalTable: "chapters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pages",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    index = table.Column<int>(type: "int", nullable: false),
                    server = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    image = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    chapter_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pages", x => x.id);
                    table.ForeignKey(
                        name: "FK_pages_chapters_chapter_id",
                        column: x => x.chapter_id,
                        principalTable: "chapters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "genres",
                columns: new[] { "id", "creation_time", "description", "last_modified", "name" },
                values: new object[,]
                {
                    { "30868928423002112", new DateTimeOffset(new DateTime(2024, 3, 26, 4, 22, 6, 53, DateTimeKind.Unspecified).AddTicks(7220), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on physical action, such as fighting, war, sports, or physical challenges.", null, "Action" },
                    { "30868928423002113", new DateTimeOffset(new DateTime(2024, 3, 26, 4, 22, 6, 53, DateTimeKind.Unspecified).AddTicks(7780), new TimeSpan(0, 0, 0, 0, 0)), "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown.", null, "Adventure" },
                    { "30868928423002114", new DateTimeOffset(new DateTime(2024, 3, 26, 4, 22, 6, 53, DateTimeKind.Unspecified).AddTicks(7786), new TimeSpan(0, 0, 0, 0, 0)), "A story with humorous narration or dialogue, intended to amuse the audience.", null, "Comedy" },
                    { "30868928423002115", new DateTimeOffset(new DateTime(2024, 3, 26, 4, 22, 6, 53, DateTimeKind.Unspecified).AddTicks(7789), new TimeSpan(0, 0, 0, 0, 0)), "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist.", null, "Drama" },
                    { "30868928423002116", new DateTimeOffset(new DateTime(2024, 3, 26, 4, 22, 6, 53, DateTimeKind.Unspecified).AddTicks(7791), new TimeSpan(0, 0, 0, 0, 0)), "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements.", null, "Fantasy" },
                    { "30868928423002117", new DateTimeOffset(new DateTime(2024, 3, 26, 4, 22, 6, 53, DateTimeKind.Unspecified).AddTicks(7827), new TimeSpan(0, 0, 0, 0, 0)), "A story that evokes fear in both the characters and the audience.", null, "Horror" },
                    { "30868928423002118", new DateTimeOffset(new DateTime(2024, 3, 26, 4, 22, 6, 53, DateTimeKind.Unspecified).AddTicks(7830), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around solving a puzzle or a crime.", null, "Mystery" },
                    { "30868928423002119", new DateTimeOffset(new DateTime(2024, 3, 26, 4, 22, 6, 53, DateTimeKind.Unspecified).AddTicks(7832), new TimeSpan(0, 0, 0, 0, 0)), "A story that emphasizes the psychology of its characters and their unstable emotional states.", null, "Psychological" },
                    { "30868928423002120", new DateTimeOffset(new DateTime(2024, 3, 26, 4, 22, 6, 53, DateTimeKind.Unspecified).AddTicks(7834), new TimeSpan(0, 0, 0, 0, 0)), "A story about love.", null, "Romance" },
                    { "30868928423002121", new DateTimeOffset(new DateTime(2024, 3, 26, 4, 22, 6, 53, DateTimeKind.Unspecified).AddTicks(7836), new TimeSpan(0, 0, 0, 0, 0)), "A story that portrays a \"cut-out\" sequence of events in a character's life.", null, "Slice of Life" },
                    { "30868928423002122", new DateTimeOffset(new DateTime(2024, 3, 26, 4, 22, 6, 53, DateTimeKind.Unspecified).AddTicks(7839), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around sports, such as baseball or basketball.", null, "Sports" },
                    { "30868928423002123", new DateTimeOffset(new DateTime(2024, 3, 26, 4, 22, 6, 53, DateTimeKind.Unspecified).AddTicks(7841), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves supernatural elements, such as ghosts or demons.", null, "Supernatural" },
                    { "30868928423002124", new DateTimeOffset(new DateTime(2024, 3, 26, 4, 22, 6, 53, DateTimeKind.Unspecified).AddTicks(7843), new TimeSpan(0, 0, 0, 0, 0)), "A story that is fast-paced and suspenseful, often involving a crime.", null, "Thriller" },
                    { "30868928423002125", new DateTimeOffset(new DateTime(2024, 3, 26, 4, 22, 6, 53, DateTimeKind.Unspecified).AddTicks(7845), new TimeSpan(0, 0, 0, 0, 0)), "A story that ends in a tragic or unhappy way.", null, "Tragedy" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_chapters_comic_id",
                table: "chapters",
                column: "comic_id");

            migrationBuilder.CreateIndex(
                name: "IX_comic_author_author_id",
                table: "comic_author",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_comic_genre_genre_id",
                table: "comic_genre",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments_chapter_id",
                table: "comments",
                column: "chapter_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments_comic_id",
                table: "comments",
                column: "comic_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments_reply_to_id",
                table: "comments",
                column: "reply_to_id");

            migrationBuilder.CreateIndex(
                name: "IX_genres_name",
                table: "genres",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_history_records_chapter_id",
                table: "history_records",
                column: "chapter_id");

            migrationBuilder.CreateIndex(
                name: "IX_library_entries_comic_id",
                table: "library_entries",
                column: "comic_id");

            migrationBuilder.CreateIndex(
                name: "IX_pages_chapter_id",
                table: "pages",
                column: "chapter_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comic_author");

            migrationBuilder.DropTable(
                name: "comic_genre");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "history_records");

            migrationBuilder.DropTable(
                name: "library_entries");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "pages");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropTable(
                name: "chapters");

            migrationBuilder.DropTable(
                name: "comics");
        }
    }
}
