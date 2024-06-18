using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comic_genre_genres_genre_id",
                table: "comic_genre");

            migrationBuilder.DropForeignKey(
                name: "FK_comics_profiles_publisher_id",
                table: "comics");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_profiles_UserProfileId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_history_records_profiles_user_id",
                table: "history_records");

            migrationBuilder.DropForeignKey(
                name: "FK_library_entries_profiles_user_id",
                table: "library_entries");

            migrationBuilder.DropForeignKey(
                name: "FK_notifications_profiles_user_id",
                table: "notifications");

            migrationBuilder.DropTable(
                name: "comic_author");

            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropIndex(
                name: "IX_comic_genre_genre_id",
                table: "comic_genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_profiles",
                table: "profiles");

            migrationBuilder.RenameTable(
                name: "profiles",
                newName: "users");

            migrationBuilder.AddColumn<string[]>(
                name: "Authors",
                table: "comics",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<decimal>(
                name: "GenresId",
                table: "comic_genre",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Tags",
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
                    table.PrimaryKey("PK_Tags", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "id", "creation_time", "description", "name" },
                values: new object[,]
                {
                    { 61289721216630784m, new DateTimeOffset(new DateTime(2024, 6, 18, 3, 3, 28, 244, DateTimeKind.Unspecified).AddTicks(2578), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on physical action, such as fighting, war, sports, or physical challenges.", "Action" },
                    { 61289721216630785m, new DateTimeOffset(new DateTime(2024, 6, 18, 3, 3, 28, 246, DateTimeKind.Unspecified).AddTicks(9725), new TimeSpan(0, 0, 0, 0, 0)), "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown.", "Adventure" },
                    { 61289721216630786m, new DateTimeOffset(new DateTime(2024, 6, 18, 3, 3, 28, 246, DateTimeKind.Unspecified).AddTicks(9757), new TimeSpan(0, 0, 0, 0, 0)), "A story with humorous narration or dialogue, intended to amuse the audience.", "Comedy" },
                    { 61289721216630787m, new DateTimeOffset(new DateTime(2024, 6, 18, 3, 3, 28, 246, DateTimeKind.Unspecified).AddTicks(9761), new TimeSpan(0, 0, 0, 0, 0)), "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist.", "Drama" },
                    { 61289721216630788m, new DateTimeOffset(new DateTime(2024, 6, 18, 3, 3, 28, 246, DateTimeKind.Unspecified).AddTicks(9765), new TimeSpan(0, 0, 0, 0, 0)), "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements.", "Fantasy" },
                    { 61289721216630789m, new DateTimeOffset(new DateTime(2024, 6, 18, 3, 3, 28, 246, DateTimeKind.Unspecified).AddTicks(9769), new TimeSpan(0, 0, 0, 0, 0)), "A story that evokes fear in both the characters and the audience.", "Horror" },
                    { 61289721216630790m, new DateTimeOffset(new DateTime(2024, 6, 18, 3, 3, 28, 246, DateTimeKind.Unspecified).AddTicks(9772), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around solving a puzzle or a crime.", "Mystery" },
                    { 61289721216630791m, new DateTimeOffset(new DateTime(2024, 6, 18, 3, 3, 28, 246, DateTimeKind.Unspecified).AddTicks(9775), new TimeSpan(0, 0, 0, 0, 0)), "A story that emphasizes the psychology of its characters and their unstable emotional states.", "Psychological" },
                    { 61289721216630792m, new DateTimeOffset(new DateTime(2024, 6, 18, 3, 3, 28, 246, DateTimeKind.Unspecified).AddTicks(9778), new TimeSpan(0, 0, 0, 0, 0)), "A story about love.", "Romance" },
                    { 61289721216630793m, new DateTimeOffset(new DateTime(2024, 6, 18, 3, 3, 28, 246, DateTimeKind.Unspecified).AddTicks(9781), new TimeSpan(0, 0, 0, 0, 0)), "A story that portrays a \"cut-out\" sequence of events in a character's life.", "Slice of Life" },
                    { 61289721216630794m, new DateTimeOffset(new DateTime(2024, 6, 18, 3, 3, 28, 246, DateTimeKind.Unspecified).AddTicks(9784), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around sports, such as baseball or basketball.", "Sports" },
                    { 61289721216630795m, new DateTimeOffset(new DateTime(2024, 6, 18, 3, 3, 28, 246, DateTimeKind.Unspecified).AddTicks(9787), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves supernatural elements, such as ghosts or demons.", "Supernatural" },
                    { 61289721216630796m, new DateTimeOffset(new DateTime(2024, 6, 18, 3, 3, 28, 246, DateTimeKind.Unspecified).AddTicks(9790), new TimeSpan(0, 0, 0, 0, 0)), "A story that is fast-paced and suspenseful, often involving a crime.", "Thriller" },
                    { 61289721216630797m, new DateTimeOffset(new DateTime(2024, 6, 18, 3, 3, 28, 246, DateTimeKind.Unspecified).AddTicks(9793), new TimeSpan(0, 0, 0, 0, 0)), "A story that ends in a tragic or unhappy way.", "Tragedy" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_comic_genre_GenresId",
                table: "comic_genre",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_name",
                table: "Tags",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_comic_genre_Tags_GenresId",
                table: "comic_genre",
                column: "GenresId",
                principalTable: "Tags",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comics_users_publisher_id",
                table: "comics",
                column: "publisher_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_users_UserProfileId",
                table: "comments",
                column: "UserProfileId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_history_records_users_user_id",
                table: "history_records",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_library_entries_users_user_id",
                table: "library_entries",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_users_user_id",
                table: "notifications",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comic_genre_Tags_GenresId",
                table: "comic_genre");

            migrationBuilder.DropForeignKey(
                name: "FK_comics_users_publisher_id",
                table: "comics");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_users_UserProfileId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_history_records_users_user_id",
                table: "history_records");

            migrationBuilder.DropForeignKey(
                name: "FK_library_entries_users_user_id",
                table: "library_entries");

            migrationBuilder.DropForeignKey(
                name: "FK_notifications_users_user_id",
                table: "notifications");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_comic_genre_GenresId",
                table: "comic_genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Authors",
                table: "comics");

            migrationBuilder.DropColumn(
                name: "GenresId",
                table: "comic_genre");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "profiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_profiles",
                table: "profiles",
                column: "id");

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
                name: "IX_comic_genre_genre_id",
                table: "comic_genre",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_comic_author_author_id",
                table: "comic_author",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_genres_name",
                table: "genres",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_comic_genre_genres_genre_id",
                table: "comic_genre",
                column: "genre_id",
                principalTable: "genres",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comics_profiles_publisher_id",
                table: "comics",
                column: "publisher_id",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_profiles_UserProfileId",
                table: "comments",
                column: "UserProfileId",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_history_records_profiles_user_id",
                table: "history_records",
                column: "user_id",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_library_entries_profiles_user_id",
                table: "library_entries",
                column: "user_id",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_profiles_user_id",
                table: "notifications",
                column: "user_id",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
