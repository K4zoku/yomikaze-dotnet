using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class Report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "report",
                type: "character varying(34)",
                maxLength: 34,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(21)",
                oldMaxLength: 21);

            migrationBuilder.AddColumn<decimal>(
                name: "comment_id",
                table: "report",
                type: "numeric(20,0)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_report_comment_id",
                table: "report",
                column: "comment_id");

            migrationBuilder.CreateIndex(
                name: "ix_report_reason_id",
                table: "report",
                column: "reason_id");

            migrationBuilder.AddForeignKey(
                name: "fk_report_chapter_comments_comment_id",
                table: "report",
                column: "comment_id",
                principalTable: "comment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_report_comic_comments_comment_id",
                table: "report",
                column: "comment_id",
                principalTable: "comment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_report_report_reason_reason_id",
                table: "report",
                column: "reason_id",
                principalTable: "report_reason",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_report_chapter_comments_comment_id",
                table: "report");

            migrationBuilder.DropForeignKey(
                name: "fk_report_comic_comments_comment_id",
                table: "report");

            migrationBuilder.DropForeignKey(
                name: "fk_report_report_reason_reason_id",
                table: "report");

            migrationBuilder.DropIndex(
                name: "ix_report_comment_id",
                table: "report");

            migrationBuilder.DropIndex(
                name: "ix_report_reason_id",
                table: "report");

            migrationBuilder.DropColumn(
                name: "comment_id",
                table: "report");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "report",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(34)",
                oldMaxLength: 34);
        }
    }
}
