using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddSuperRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 64744714359840768m);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 64744714368229376m);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 64744714368229377m);

            migrationBuilder.DeleteData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 64744714368188419m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 64744714368188420m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 64744714368188421m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 64744714368188422m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 64744714368188423m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 64744714368188424m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 64744714368188425m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 64744714368188426m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 64744714368188427m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 64744714368188428m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 64744714368188429m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 64744714368188430m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 64744714368188431m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 64744714368188432m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 64744714368188433m);

            migrationBuilder.DeleteData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 64744714368188418m);

            migrationBuilder.DropColumn(
                name: "description",
                table: "chapters");

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

            migrationBuilder.CreateIndex(
                name: "ix_unlocked_chapters_chapter_id",
                table: "unlocked_chapters",
                column: "chapter_id");

            migrationBuilder.AddForeignKey(
                name: "fk_unlocked_chapters_chapters_chapter_id",
                table: "unlocked_chapters",
                column: "chapter_id",
                principalTable: "chapters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_unlocked_chapters_users_user_id",
                table: "unlocked_chapters",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_unlocked_chapters_chapters_chapter_id",
                table: "unlocked_chapters");

            migrationBuilder.DropForeignKey(
                name: "fk_unlocked_chapters_users_user_id",
                table: "unlocked_chapters");

            migrationBuilder.DropIndex(
                name: "ix_unlocked_chapters_chapter_id",
                table: "unlocked_chapters");

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

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "chapters",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { 64744714359840768m, "877d6ec0-dfbe-4258-8035-8025768f14e6", "Administrator", "ADMINISTRATOR" },
                    { 64744714368229376m, "764596fa-72d8-48a9-ba1c-0efb6fe33450", "Publisher", "PUBLISHER" },
                    { 64744714368229377m, "72ce889b-ed09-4632-86ef-a077da904a8c", "Reader", "READER" }
                });

            migrationBuilder.InsertData(
                table: "tag_categories",
                columns: new[] { "id", "creation_time", "name" },
                values: new object[,]
                {
                    { 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(4341), new TimeSpan(0, 0, 0, 0, 0)), "Genre" },
                    { 64744714368188419m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(4965), new TimeSpan(0, 0, 0, 0, 0)), "Theme" }
                });

            migrationBuilder.InsertData(
                table: "tags",
                columns: new[] { "id", "category_id", "creation_time", "description", "name" },
                values: new object[,]
                {
                    { 64744714368188420m, 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(6307), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on physical action, such as fighting, war, sports, or physical challenges.", "Action" },
                    { 64744714368188421m, 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(7373), new TimeSpan(0, 0, 0, 0, 0)), "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown.", "Adventure" },
                    { 64744714368188422m, 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(7383), new TimeSpan(0, 0, 0, 0, 0)), "A story with humorous narration or dialogue, intended to amuse the audience.", "Comedy" },
                    { 64744714368188423m, 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(7388), new TimeSpan(0, 0, 0, 0, 0)), "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist.", "Drama" },
                    { 64744714368188424m, 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(7393), new TimeSpan(0, 0, 0, 0, 0)), "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements.", "Fantasy" },
                    { 64744714368188425m, 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(7397), new TimeSpan(0, 0, 0, 0, 0)), "A story that evokes fear in both the characters and the audience.", "Horror" },
                    { 64744714368188426m, 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(7401), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around solving a puzzle or a crime.", "Mystery" },
                    { 64744714368188427m, 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(7405), new TimeSpan(0, 0, 0, 0, 0)), "A story that emphasizes the psychology of its characters and their unstable emotional states.", "Psychological" },
                    { 64744714368188428m, 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(7409), new TimeSpan(0, 0, 0, 0, 0)), "A story about love.", "Romance" },
                    { 64744714368188429m, 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(7413), new TimeSpan(0, 0, 0, 0, 0)), "A story that portrays a \"cut-out\" sequence of events in a character's life.", "Slice of Life" },
                    { 64744714368188430m, 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(7417), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around sports, such as baseball or basketball.", "Sports" },
                    { 64744714368188431m, 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(7830), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves supernatural elements, such as ghosts or demons.", "Supernatural" },
                    { 64744714368188432m, 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(7837), new TimeSpan(0, 0, 0, 0, 0)), "A story that is fast-paced and suspenseful, often involving a crime.", "Thriller" },
                    { 64744714368188433m, 64744714368188418m, new DateTimeOffset(new DateTime(2024, 6, 27, 15, 52, 22, 804, DateTimeKind.Unspecified).AddTicks(7845), new TimeSpan(0, 0, 0, 0, 0)), "A story that ends in a tragic or unhappy way.", "Tragedy" }
                });
        }
    }
}
