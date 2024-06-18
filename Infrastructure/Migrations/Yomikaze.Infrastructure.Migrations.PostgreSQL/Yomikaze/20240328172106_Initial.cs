using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Yomikaze
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
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    avatar = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    Avatar = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Banner = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Bio = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Birthday = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "comics",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    aliases = table.Column<string[]>(type: "text[]", nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    cover = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    banner = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    published = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ended = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    publisher_id = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comics", x => x.id);
                    table.ForeignKey(
                        name: "FK_comics_profiles_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    content = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    read = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_notifications_profiles_user_id",
                        column: x => x.user_id,
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chapters",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    comic_id = table.Column<decimal>(type: "numeric(20,0)", maxLength: 20, nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true)
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
                    comic_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    author_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
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
                name: "comic_genre",
                columns: table => new
                {
                    comic_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    genre_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
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
                name: "library_entries",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    comic_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true)
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
                    table.ForeignKey(
                        name: "FK_library_entries_profiles_user_id",
                        column: x => x.user_id,
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    content = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    comic_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    chapter_id = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    reply_to_id = table.Column<decimal>(type: "numeric(20,0)", maxLength: 20, nullable: true),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    UserProfileId = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_comments_profiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "history_records",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    chapter_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    views = table.Column<long>(type: "bigint", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true)
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
                    table.ForeignKey(
                        name: "FK_history_records_profiles_user_id",
                        column: x => x.user_id,
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pages",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false),
                    server = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    image = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    chapter_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true)
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
                columns: new[] { "id", "creation_time", "description", "name" },
                values: new object[,]
                {
                    { 31789746191597568m, new DateTimeOffset(new DateTime(2024, 3, 28, 17, 21, 6, 116, DateTimeKind.Unspecified).AddTicks(3710), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on physical action, such as fighting, war, sports, or physical challenges.", "Action" },
                    { 31789746195791872m, new DateTimeOffset(new DateTime(2024, 3, 28, 17, 21, 6, 118, DateTimeKind.Unspecified).AddTicks(1671), new TimeSpan(0, 0, 0, 0, 0)), "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown.", "Adventure" },
                    { 31789746195791873m, new DateTimeOffset(new DateTime(2024, 3, 28, 17, 21, 6, 118, DateTimeKind.Unspecified).AddTicks(1710), new TimeSpan(0, 0, 0, 0, 0)), "A story with humorous narration or dialogue, intended to amuse the audience.", "Comedy" },
                    { 31789746195791874m, new DateTimeOffset(new DateTime(2024, 3, 28, 17, 21, 6, 118, DateTimeKind.Unspecified).AddTicks(1714), new TimeSpan(0, 0, 0, 0, 0)), "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist.", "Drama" },
                    { 31789746195791875m, new DateTimeOffset(new DateTime(2024, 3, 28, 17, 21, 6, 118, DateTimeKind.Unspecified).AddTicks(1717), new TimeSpan(0, 0, 0, 0, 0)), "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements.", "Fantasy" },
                    { 31789746195791876m, new DateTimeOffset(new DateTime(2024, 3, 28, 17, 21, 6, 118, DateTimeKind.Unspecified).AddTicks(1719), new TimeSpan(0, 0, 0, 0, 0)), "A story that evokes fear in both the characters and the audience.", "Horror" },
                    { 31789746195791877m, new DateTimeOffset(new DateTime(2024, 3, 28, 17, 21, 6, 118, DateTimeKind.Unspecified).AddTicks(1722), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around solving a puzzle or a crime.", "Mystery" },
                    { 31789746195791878m, new DateTimeOffset(new DateTime(2024, 3, 28, 17, 21, 6, 118, DateTimeKind.Unspecified).AddTicks(1725), new TimeSpan(0, 0, 0, 0, 0)), "A story that emphasizes the psychology of its characters and their unstable emotional states.", "Psychological" },
                    { 31789746195791879m, new DateTimeOffset(new DateTime(2024, 3, 28, 17, 21, 6, 118, DateTimeKind.Unspecified).AddTicks(1727), new TimeSpan(0, 0, 0, 0, 0)), "A story about love.", "Romance" },
                    { 31789746195791880m, new DateTimeOffset(new DateTime(2024, 3, 28, 17, 21, 6, 118, DateTimeKind.Unspecified).AddTicks(1729), new TimeSpan(0, 0, 0, 0, 0)), "A story that portrays a \"cut-out\" sequence of events in a character's life.", "Slice of Life" },
                    { 31789746195791881m, new DateTimeOffset(new DateTime(2024, 3, 28, 17, 21, 6, 118, DateTimeKind.Unspecified).AddTicks(1731), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around sports, such as baseball or basketball.", "Sports" },
                    { 31789746195791882m, new DateTimeOffset(new DateTime(2024, 3, 28, 17, 21, 6, 118, DateTimeKind.Unspecified).AddTicks(1733), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves supernatural elements, such as ghosts or demons.", "Supernatural" },
                    { 31789746195791883m, new DateTimeOffset(new DateTime(2024, 3, 28, 17, 21, 6, 118, DateTimeKind.Unspecified).AddTicks(1736), new TimeSpan(0, 0, 0, 0, 0)), "A story that is fast-paced and suspenseful, often involving a crime.", "Thriller" },
                    { 31789746195791884m, new DateTimeOffset(new DateTime(2024, 3, 28, 17, 21, 6, 118, DateTimeKind.Unspecified).AddTicks(1738), new TimeSpan(0, 0, 0, 0, 0)), "A story that ends in a tragic or unhappy way.", "Tragedy" }
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
                name: "IX_comics_publisher_id",
                table: "comics",
                column: "publisher_id");

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
                name: "IX_comments_UserProfileId",
                table: "comments",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_genres_name",
                table: "genres",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_history_records_chapter_id_user_id",
                table: "history_records",
                columns: new[] { "chapter_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_history_records_user_id",
                table: "history_records",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_library_entries_comic_id_user_id",
                table: "library_entries",
                columns: new[] { "comic_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_library_entries_user_id",
                table: "library_entries",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_user_id",
                table: "notifications",
                column: "user_id");

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

            migrationBuilder.DropTable(
                name: "profiles");
        }
    }
}
