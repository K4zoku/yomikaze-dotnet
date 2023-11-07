using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yomikaze.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updatecascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comics_Users_UserId",
                table: "Comics");

            migrationBuilder.DropIndex(
                name: "IX_Comics_UserId",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "PageIndex",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comics");

            migrationBuilder.CreateTable(
                name: "LibraryEntries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComicId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Added = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryEntries_Comics_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryEntries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibraryEntries_ComicId",
                table: "LibraryEntries",
                column: "ComicId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryEntries_UserId",
                table: "LibraryEntries",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibraryEntries");

            migrationBuilder.AddColumn<int>(
                name: "PageIndex",
                table: "Histories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Comics",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comics_UserId",
                table: "Comics",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comics_Users_UserId",
                table: "Comics",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
