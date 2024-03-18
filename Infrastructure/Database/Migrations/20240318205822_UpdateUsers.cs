using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yomikaze.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028219153693278208");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028219153697472512");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028219153697472513");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028219153697472514");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028219153697472515");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028219153697472516");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028219153697472517");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028219153697472518");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028219153697472519");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028219153697472520");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028219153697472521");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028219153697472522");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028219153697472523");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028219153697472524");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "00028219153697513485");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "00028219153701707776");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "00028219153701707777");

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { "00028220542767071232", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 58, 21, 709, DateTimeKind.Unspecified).AddTicks(714), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on physical action, such as fighting, war, sports, or physical challenges.", "Action" },
                    { "00028220542771265536", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 58, 21, 709, DateTimeKind.Unspecified).AddTicks(1380), new TimeSpan(0, 0, 0, 0, 0)), "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown.", "Adventure" },
                    { "00028220542771265537", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 58, 21, 709, DateTimeKind.Unspecified).AddTicks(1388), new TimeSpan(0, 0, 0, 0, 0)), "A story with humorous narration or dialogue, intended to amuse the audience.", "Comedy" },
                    { "00028220542771265538", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 58, 21, 709, DateTimeKind.Unspecified).AddTicks(1391), new TimeSpan(0, 0, 0, 0, 0)), "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist.", "Drama" },
                    { "00028220542771265539", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 58, 21, 709, DateTimeKind.Unspecified).AddTicks(1394), new TimeSpan(0, 0, 0, 0, 0)), "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements.", "Fantasy" },
                    { "00028220542771265540", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 58, 21, 709, DateTimeKind.Unspecified).AddTicks(1396), new TimeSpan(0, 0, 0, 0, 0)), "A story that evokes fear in both the characters and the audience.", "Horror" },
                    { "00028220542771265541", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 58, 21, 709, DateTimeKind.Unspecified).AddTicks(1399), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around solving a puzzle or a crime.", "Mystery" },
                    { "00028220542771265542", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 58, 21, 709, DateTimeKind.Unspecified).AddTicks(1402), new TimeSpan(0, 0, 0, 0, 0)), "A story that emphasizes the psychology of its characters and their unstable emotional states.", "Psychological" },
                    { "00028220542771265543", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 58, 21, 709, DateTimeKind.Unspecified).AddTicks(1404), new TimeSpan(0, 0, 0, 0, 0)), "A story about love.", "Romance" },
                    { "00028220542771265544", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 58, 21, 709, DateTimeKind.Unspecified).AddTicks(1406), new TimeSpan(0, 0, 0, 0, 0)), "A story that portrays a \"cut-out\" sequence of events in a character's life.", "Slice of Life" },
                    { "00028220542771265545", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 58, 21, 709, DateTimeKind.Unspecified).AddTicks(1409), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around sports, such as baseball or basketball.", "Sports" },
                    { "00028220542771265546", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 58, 21, 709, DateTimeKind.Unspecified).AddTicks(1411), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves supernatural elements, such as ghosts or demons.", "Supernatural" },
                    { "00028220542771265547", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 58, 21, 709, DateTimeKind.Unspecified).AddTicks(1437), new TimeSpan(0, 0, 0, 0, 0)), "A story that is fast-paced and suspenseful, often involving a crime.", "Thriller" },
                    { "00028220542771265548", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 58, 21, 709, DateTimeKind.Unspecified).AddTicks(1440), new TimeSpan(0, 0, 0, 0, 0)), "A story that ends in a tragic or unhappy way.", "Tragedy" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00028220542771306509", null, "Administrator", "ADMINISTRATOR" },
                    { "00028220542771306510", null, "Publisher", "PUBLISHER" },
                    { "00028220542771306511", null, "Reader", "READER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028220542767071232");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028220542771265536");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028220542771265537");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028220542771265538");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028220542771265539");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028220542771265540");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028220542771265541");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028220542771265542");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028220542771265543");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028220542771265544");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028220542771265545");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028220542771265546");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028220542771265547");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: "00028220542771265548");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "00028220542771306509");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "00028220542771306510");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "00028220542771306511");

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { "00028219153693278208", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 52, 50, 528, DateTimeKind.Unspecified).AddTicks(2519), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on physical action, such as fighting, war, sports, or physical challenges.", "Action" },
                    { "00028219153697472512", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 52, 50, 528, DateTimeKind.Unspecified).AddTicks(3218), new TimeSpan(0, 0, 0, 0, 0)), "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown.", "Adventure" },
                    { "00028219153697472513", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 52, 50, 528, DateTimeKind.Unspecified).AddTicks(3225), new TimeSpan(0, 0, 0, 0, 0)), "A story with humorous narration or dialogue, intended to amuse the audience.", "Comedy" },
                    { "00028219153697472514", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 52, 50, 528, DateTimeKind.Unspecified).AddTicks(3228), new TimeSpan(0, 0, 0, 0, 0)), "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist.", "Drama" },
                    { "00028219153697472515", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 52, 50, 528, DateTimeKind.Unspecified).AddTicks(3231), new TimeSpan(0, 0, 0, 0, 0)), "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements.", "Fantasy" },
                    { "00028219153697472516", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 52, 50, 528, DateTimeKind.Unspecified).AddTicks(3234), new TimeSpan(0, 0, 0, 0, 0)), "A story that evokes fear in both the characters and the audience.", "Horror" },
                    { "00028219153697472517", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 52, 50, 528, DateTimeKind.Unspecified).AddTicks(3236), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around solving a puzzle or a crime.", "Mystery" },
                    { "00028219153697472518", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 52, 50, 528, DateTimeKind.Unspecified).AddTicks(3238), new TimeSpan(0, 0, 0, 0, 0)), "A story that emphasizes the psychology of its characters and their unstable emotional states.", "Psychological" },
                    { "00028219153697472519", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 52, 50, 528, DateTimeKind.Unspecified).AddTicks(3241), new TimeSpan(0, 0, 0, 0, 0)), "A story about love.", "Romance" },
                    { "00028219153697472520", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 52, 50, 528, DateTimeKind.Unspecified).AddTicks(3243), new TimeSpan(0, 0, 0, 0, 0)), "A story that portrays a \"cut-out\" sequence of events in a character's life.", "Slice of Life" },
                    { "00028219153697472521", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 52, 50, 528, DateTimeKind.Unspecified).AddTicks(3246), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around sports, such as baseball or basketball.", "Sports" },
                    { "00028219153697472522", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 52, 50, 528, DateTimeKind.Unspecified).AddTicks(3248), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves supernatural elements, such as ghosts or demons.", "Supernatural" },
                    { "00028219153697472523", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 52, 50, 528, DateTimeKind.Unspecified).AddTicks(3250), new TimeSpan(0, 0, 0, 0, 0)), "A story that is fast-paced and suspenseful, often involving a crime.", "Thriller" },
                    { "00028219153697472524", new DateTimeOffset(new DateTime(2024, 3, 18, 20, 52, 50, 528, DateTimeKind.Unspecified).AddTicks(3253), new TimeSpan(0, 0, 0, 0, 0)), "A story that ends in a tragic or unhappy way.", "Tragedy" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00028219153697513485", null, "Administrator", "ADMINISTRATOR" },
                    { "00028219153701707776", null, "Publisher", "PUBLISHER" },
                    { "00028219153701707777", null, "Reader", "READER" }
                });
        }
    }
}
