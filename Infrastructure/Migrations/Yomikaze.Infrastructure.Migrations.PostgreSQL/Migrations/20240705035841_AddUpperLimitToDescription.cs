using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddUpperLimitToDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67003811956301824m);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67003811960496128m);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67003811960496129m);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67003811960496130m);

            migrationBuilder.DeleteData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67003811960455172m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67003811960455173m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67003811960455174m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67003811960455175m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67003811960455176m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67003811960455177m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67003811960455178m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67003811960455179m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67003811960455180m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67003811960455181m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67003811960455182m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67003811960455183m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67003811960455184m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67003811960455185m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67003811960455186m);

            migrationBuilder.DeleteData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67003811960455171m);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "comics",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(512)",
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { 67464207511101440m, "fb1e935d-799f-418e-8032-882d9604b819", "Super", "SUPER" },
                    { 67464207515295744m, "0087db43-71b7-4528-94e1-cec49d4ac944", "Administrator", "ADMINISTRATOR" },
                    { 67464207515295745m, "bbd047ee-5ddf-4db0-b2f0-3e4563548da6", "Publisher", "PUBLISHER" },
                    { 67464207515295746m, "5372e28a-4a82-46ee-9e40-9a02a41426e3", "Reader", "READER" }
                });

            migrationBuilder.InsertData(
                table: "tag_categories",
                columns: new[] { "id", "creation_time", "name" },
                values: new object[,]
                {
                    { 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(5166), new TimeSpan(0, 0, 0, 0, 0)), "Genre" },
                    { 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(5470), new TimeSpan(0, 0, 0, 0, 0)), "Theme" }
                });

            migrationBuilder.InsertData(
                table: "tags",
                columns: new[] { "id", "category_id", "creation_time", "description", "name" },
                values: new object[,]
                {
                    { 67464207515254789m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6218), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on physical action, such as fighting, war, sports, or physical challenges.", "Action" },
                    { 67464207515254790m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6762), new TimeSpan(0, 0, 0, 0, 0)), "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown.", "Adventure" },
                    { 67464207515254791m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6766), new TimeSpan(0, 0, 0, 0, 0)), "A story with humorous narration or dialogue, intended to amuse the audience.", "Comedy" },
                    { 67464207515254792m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6769), new TimeSpan(0, 0, 0, 0, 0)), "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist.", "Drama" },
                    { 67464207515254793m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6772), new TimeSpan(0, 0, 0, 0, 0)), "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements.", "Fantasy" },
                    { 67464207515254794m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6775), new TimeSpan(0, 0, 0, 0, 0)), "A story that evokes fear in both the characters and the audience.", "Horror" },
                    { 67464207515254795m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6780), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around solving a puzzle or a crime.", "Mystery" },
                    { 67464207515254796m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6782), new TimeSpan(0, 0, 0, 0, 0)), "A story that emphasizes the psychology of its characters and their unstable emotional states.", "Psychological" },
                    { 67464207515254797m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6785), new TimeSpan(0, 0, 0, 0, 0)), "A story about love.", "Romance" },
                    { 67464207515254798m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6787), new TimeSpan(0, 0, 0, 0, 0)), "A story that portrays a \"cut-out\" sequence of events in a character's life.", "Slice of Life" },
                    { 67464207515254799m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6791), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around sports, such as baseball or basketball.", "Sports" },
                    { 67464207515254800m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6793), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves supernatural elements, such as ghosts or demons.", "Supernatural" },
                    { 67464207515254801m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6796), new TimeSpan(0, 0, 0, 0, 0)), "A story that is fast-paced and suspenseful, often involving a crime.", "Thriller" },
                    { 67464207515254802m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6800), new TimeSpan(0, 0, 0, 0, 0)), "A story that ends in a tragic or unhappy way.", "Tragedy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207511101440m);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295744m);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295745m);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295746m);

            migrationBuilder.DeleteData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254788m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254789m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254790m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254791m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254792m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254793m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254794m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254795m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254796m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254797m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254798m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254799m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254800m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254801m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254802m);

            migrationBuilder.DeleteData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254787m);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "comics",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { 67003811956301824m, "0de354ef-d93b-4627-92be-3b54ebefafea", "Super", "SUPER" },
                    { 67003811960496128m, "54bb6019-ba9e-4c80-98ab-7835929d0e2f", "Administrator", "ADMINISTRATOR" },
                    { 67003811960496129m, "7b00f126-c095-4c9c-8cae-daf860b956d3", "Publisher", "PUBLISHER" },
                    { 67003811960496130m, "d1168463-a18a-4851-b359-acb3bb7487c2", "Reader", "READER" }
                });

            migrationBuilder.InsertData(
                table: "tag_categories",
                columns: new[] { "id", "creation_time", "name" },
                values: new object[,]
                {
                    { 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(3423), new TimeSpan(0, 0, 0, 0, 0)), "Genre" },
                    { 67003811960455172m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(3704), new TimeSpan(0, 0, 0, 0, 0)), "Theme" }
                });

            migrationBuilder.InsertData(
                table: "tags",
                columns: new[] { "id", "category_id", "creation_time", "description", "name" },
                values: new object[,]
                {
                    { 67003811960455173m, 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(4288), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on physical action, such as fighting, war, sports, or physical challenges.", "Action" },
                    { 67003811960455174m, 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(4936), new TimeSpan(0, 0, 0, 0, 0)), "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown.", "Adventure" },
                    { 67003811960455175m, 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(4942), new TimeSpan(0, 0, 0, 0, 0)), "A story with humorous narration or dialogue, intended to amuse the audience.", "Comedy" },
                    { 67003811960455176m, 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(4945), new TimeSpan(0, 0, 0, 0, 0)), "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist.", "Drama" },
                    { 67003811960455177m, 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(4948), new TimeSpan(0, 0, 0, 0, 0)), "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements.", "Fantasy" },
                    { 67003811960455178m, 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(4951), new TimeSpan(0, 0, 0, 0, 0)), "A story that evokes fear in both the characters and the audience.", "Horror" },
                    { 67003811960455179m, 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(4953), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around solving a puzzle or a crime.", "Mystery" },
                    { 67003811960455180m, 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(4955), new TimeSpan(0, 0, 0, 0, 0)), "A story that emphasizes the psychology of its characters and their unstable emotional states.", "Psychological" },
                    { 67003811960455181m, 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(4957), new TimeSpan(0, 0, 0, 0, 0)), "A story about love.", "Romance" },
                    { 67003811960455182m, 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(4959), new TimeSpan(0, 0, 0, 0, 0)), "A story that portrays a \"cut-out\" sequence of events in a character's life.", "Slice of Life" },
                    { 67003811960455183m, 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(4961), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around sports, such as baseball or basketball.", "Sports" },
                    { 67003811960455184m, 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(4963), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves supernatural elements, such as ghosts or demons.", "Supernatural" },
                    { 67003811960455185m, 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(4965), new TimeSpan(0, 0, 0, 0, 0)), "A story that is fast-paced and suspenseful, often involving a crime.", "Thriller" },
                    { 67003811960455186m, 67003811960455171m, new DateTimeOffset(new DateTime(2024, 7, 3, 21, 29, 13, 642, DateTimeKind.Unspecified).AddTicks(4967), new TimeSpan(0, 0, 0, 0, 0)), "A story that ends in a tragic or unhappy way.", "Tragedy" }
                });
        }
    }
}
