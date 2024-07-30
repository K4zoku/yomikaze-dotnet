using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class FixWithdrawal2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_withdrawal_requests_users_profile_id",
                table: "withdrawal_requests");

            migrationBuilder.RenameColumn(
                name: "profile_id",
                table: "withdrawal_requests",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_withdrawal_requests_profile_id",
                table: "withdrawal_requests",
                newName: "ix_withdrawal_requests_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_withdrawal_requests_users_user_id",
                table: "withdrawal_requests",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_withdrawal_requests_users_user_id",
                table: "withdrawal_requests");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "withdrawal_requests",
                newName: "profile_id");

            migrationBuilder.RenameIndex(
                name: "ix_withdrawal_requests_user_id",
                table: "withdrawal_requests",
                newName: "ix_withdrawal_requests_profile_id");

            migrationBuilder.AddForeignKey(
                name: "fk_withdrawal_requests_users_profile_id",
                table: "withdrawal_requests",
                column: "profile_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
