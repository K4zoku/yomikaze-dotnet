using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_comment_comment_reply_to_id",
                table: "comment");

            migrationBuilder.AddForeignKey(
                name: "fk_comment_comment_reply_to_id",
                table: "comment",
                column: "reply_to_id",
                principalTable: "comment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_comment_comment_reply_to_id",
                table: "comment");

            migrationBuilder.AddForeignKey(
                name: "fk_comment_comment_reply_to_id",
                table: "comment",
                column: "reply_to_id",
                principalTable: "comment",
                principalColumn: "id");
        }
    }
}
