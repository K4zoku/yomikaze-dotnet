using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddReportReasons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_report_report_categories_category_id",
                table: "report");

            migrationBuilder.DropTable(
                name: "report_categories");

            migrationBuilder.DropIndex(
                name: "ix_report_category_id",
                table: "report");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "report",
                newName: "reason_id");

            migrationBuilder.CreateTable(
                name: "report_reason",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    content = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    type = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_report_reason", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "report_reason");

            migrationBuilder.RenameColumn(
                name: "reason_id",
                table: "report",
                newName: "category_id");

            migrationBuilder.CreateTable(
                name: "report_categories",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    requires_description = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_report_categories", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_report_category_id",
                table: "report",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "fk_report_report_categories_category_id",
                table: "report",
                column: "category_id",
                principalTable: "report_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
