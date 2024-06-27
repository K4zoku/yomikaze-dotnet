using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSomethingIDK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 63853233931591680m);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 63853233935785984m);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 63853233939980288m);

            migrationBuilder.DeleteData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 63853233939939330m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 63853233939939331m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 63853233939939332m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 63853233939939333m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 63853233939939334m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 63853233939939335m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 63853233939939336m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 63853233939939337m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 63853233939939338m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 63853233939939339m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 63853233939939340m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 63853233939939341m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 63853233939939342m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 63853233939939343m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 63853233939939344m);

            migrationBuilder.DeleteData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 63853233939939329m);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "withdrawal_requests",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "translations",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "transactions",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "tags",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "tag_categories",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "report_categories",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "report",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "pages",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "notifications",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "library_entries",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "library_categories",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "history_records",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "comment",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "comics",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "coin_pricings",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "chapters",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .OldAnnotation("Relational:ColumnOrder", 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "withdrawal_requests",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "translations",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "transactions",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "tags",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "tag_categories",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "report_categories",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "report",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "pages",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "notifications",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "library_entries",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "library_categories",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "history_records",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "comment",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "comics",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "coin_pricings",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "chapters",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { 63853233931591680m, "280cde96-1153-446a-bf9c-aafab2906f37", "Administrator", "ADMINISTRATOR" },
                    { 63853233935785984m, "590f3802-0591-4407-9717-77706c8e2b2c", "Publisher", "PUBLISHER" },
                    { 63853233939980288m, "da309417-4deb-48a4-9bbf-6d4a720d6f2d", "Reader", "READER" }
                });

            migrationBuilder.InsertData(
                table: "tag_categories",
                columns: new[] { "id", "creation_time", "name" },
                values: new object[,]
                {
                    { 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(1113), new TimeSpan(0, 0, 0, 0, 0)), "Genre" },
                    { 63853233939939330m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(1532), new TimeSpan(0, 0, 0, 0, 0)), "Theme" }
                });

            migrationBuilder.InsertData(
                table: "tags",
                columns: new[] { "id", "category_id", "creation_time", "description", "name" },
                values: new object[,]
                {
                    { 63853233939939331m, 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(2240), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on physical action, such as fighting, war, sports, or physical challenges.", "Action" },
                    { 63853233939939332m, 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(2743), new TimeSpan(0, 0, 0, 0, 0)), "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown.", "Adventure" },
                    { 63853233939939333m, 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(2747), new TimeSpan(0, 0, 0, 0, 0)), "A story with humorous narration or dialogue, intended to amuse the audience.", "Comedy" },
                    { 63853233939939334m, 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(2749), new TimeSpan(0, 0, 0, 0, 0)), "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist.", "Drama" },
                    { 63853233939939335m, 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(2751), new TimeSpan(0, 0, 0, 0, 0)), "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements.", "Fantasy" },
                    { 63853233939939336m, 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(2754), new TimeSpan(0, 0, 0, 0, 0)), "A story that evokes fear in both the characters and the audience.", "Horror" },
                    { 63853233939939337m, 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(2805), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around solving a puzzle or a crime.", "Mystery" },
                    { 63853233939939338m, 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(2807), new TimeSpan(0, 0, 0, 0, 0)), "A story that emphasizes the psychology of its characters and their unstable emotional states.", "Psychological" },
                    { 63853233939939339m, 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(2809), new TimeSpan(0, 0, 0, 0, 0)), "A story about love.", "Romance" },
                    { 63853233939939340m, 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(2811), new TimeSpan(0, 0, 0, 0, 0)), "A story that portrays a \"cut-out\" sequence of events in a character's life.", "Slice of Life" },
                    { 63853233939939341m, 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(2814), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around sports, such as baseball or basketball.", "Sports" },
                    { 63853233939939342m, 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(2816), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves supernatural elements, such as ghosts or demons.", "Supernatural" },
                    { 63853233939939343m, 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(2818), new TimeSpan(0, 0, 0, 0, 0)), "A story that is fast-paced and suspenseful, often involving a crime.", "Thriller" },
                    { 63853233939939344m, 63853233939939329m, new DateTimeOffset(new DateTime(2024, 6, 25, 4, 49, 57, 307, DateTimeKind.Unspecified).AddTicks(2820), new TimeSpan(0, 0, 0, 0, 0)), "A story that ends in a tragic or unhappy way.", "Tragedy" }
                });
        }
    }
}
