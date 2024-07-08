using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLibraryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_library_categories_library_entries_library_entry_id",
                table: "library_categories");

            migrationBuilder.DropIndex(
                name: "ix_library_categories_library_entry_id",
                table: "library_categories");

            migrationBuilder.DropColumn(
                name: "library_entry_id",
                table: "library_categories");

            migrationBuilder.CreateTable(
                name: "library_entry_category",
                columns: table => new
                {
                    entry_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    category_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_library_entry_category", x => new { x.entry_id, x.category_id });
                    table.ForeignKey(
                        name: "fk_library_entry_category_library_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "library_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_library_entry_category_library_entries_entry_id",
                        column: x => x.entry_id,
                        principalTable: "library_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207511101440m,
                column: "concurrency_stamp",
                value: "7fcd7855-4def-4c66-a09e-f02688f61334");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295744m,
                column: "concurrency_stamp",
                value: "9afb8433-1d9d-4bfc-b90a-6dffc47e7996");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295745m,
                column: "concurrency_stamp",
                value: "87bb1d97-6685-4191-86cd-2a0e0e185906");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295746m,
                column: "concurrency_stamp",
                value: "3dd6dbd1-fa23-4a09-8f9b-04ec3e1a0f68");

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254786m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(3285), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254787m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(3577), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254788m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(3581), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254789m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(3582), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254789m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5175), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254790m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5177), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254791m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5180), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254792m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5183), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254793m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5185), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254794m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5189), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254795m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5196), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254796m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5197), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254797m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5199), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254798m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5201), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254799m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5203), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254800m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5344), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254801m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5206), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254802m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5207), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254803m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5178), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254804m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5182), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254805m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5186), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254806m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5187), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254807m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5190), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254808m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5192), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254809m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5193), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254810m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5194), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254811m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5200), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254812m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5204), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254813m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5208), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254814m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5211), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254815m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5212), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254816m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5213), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254817m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5215), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254818m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5216), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254819m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5218), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254820m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5219), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254821m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5220), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254822m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5222), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254823m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5223), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254824m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5225), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254825m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5228), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254826m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5230), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254827m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5319), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254828m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5320), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254829m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5322), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254830m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5324), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254831m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5325), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254832m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5327), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254833m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5330), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254834m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5332), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254835m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5333), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254836m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5335), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254837m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5336), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254838m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5337), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254839m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5339), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254840m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5340), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254841m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5342), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254842m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5345), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254843m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5347), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254844m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5348), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254845m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5349), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254846m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5351), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254847m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5352), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254848m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5354), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254849m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5355), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254850m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5356), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254851m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5358), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254877m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(4408), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254878m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5091), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254879m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5097), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254880m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5099), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254881m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5101), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254882m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5102), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254883m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5104), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254884m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5106), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254885m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5108), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254886m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5109), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254887m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5111), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254888m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 8, 56, 37, 806, DateTimeKind.Unspecified).AddTicks(5174), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "ix_library_entry_category_category_id",
                table: "library_entry_category",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "library_entry_category");

            migrationBuilder.AddColumn<decimal>(
                name: "library_entry_id",
                table: "library_categories",
                type: "numeric(20,0)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207511101440m,
                column: "concurrency_stamp",
                value: "bf72375e-094f-4839-b963-7359cec38ff3");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295744m,
                column: "concurrency_stamp",
                value: "5c370476-dd39-4d67-aee8-d4d413e4592f");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295745m,
                column: "concurrency_stamp",
                value: "57b7d003-922b-4f63-81c4-fe8b1ac61fc6");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295746m,
                column: "concurrency_stamp",
                value: "24009479-7379-4317-88c7-dc441f0d3289");

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254786m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 360, DateTimeKind.Unspecified).AddTicks(8762), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254787m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 360, DateTimeKind.Unspecified).AddTicks(9160), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254788m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 360, DateTimeKind.Unspecified).AddTicks(9165), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254789m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 360, DateTimeKind.Unspecified).AddTicks(9167), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254789m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(675), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254790m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(677), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254791m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(680), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254792m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(683), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254793m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(685), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254794m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(690), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254795m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(697), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254796m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(699), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254797m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(700), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254798m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(703), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254799m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(705), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254800m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(793), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254801m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(708), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254802m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(710), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254803m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(678), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254804m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(682), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254805m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(686), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254806m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(688), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254807m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(691), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254808m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(693), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254809m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(694), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254810m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(696), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254811m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(701), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254812m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(707), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254813m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(711), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254814m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(712), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254815m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(714), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254816m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(757), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254817m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(759), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254818m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(761), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254819m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(762), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254820m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(764), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254821m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(765), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254822m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(766), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254823m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(768), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254824m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(769), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254825m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(771), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254826m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(772), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254827m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(773), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254828m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(775), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254829m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(776), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254830m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(777), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254831m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(779), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254832m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(780), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254833m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(781), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254834m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(783), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254835m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(784), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254836m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(785), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254837m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(787), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254838m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(788), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254839m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(789), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254840m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(791), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254841m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(792), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254842m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(795), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254843m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(796), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254844m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(797), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254845m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(799), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254846m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(800), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254847m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(801), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254848m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(803), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254849m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(804), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254850m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(805), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254851m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(806), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254877m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(49), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254878m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(655), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254879m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(659), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254880m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(661), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254881m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(662), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254882m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(664), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254883m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(666), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254884m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(667), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254885m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(669), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254886m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(670), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254887m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(672), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254888m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(673), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "ix_library_categories_library_entry_id",
                table: "library_categories",
                column: "library_entry_id");

            migrationBuilder.AddForeignKey(
                name: "fk_library_categories_library_entries_library_entry_id",
                table: "library_categories",
                column: "library_entry_id",
                principalTable: "library_entries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
