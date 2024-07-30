using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWithdrawalAndAddRoleRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_approved",
                table: "role_requests");

            migrationBuilder.AddColumn<string>(
                name: "payment_information",
                table: "withdrawal_requests",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "creation_time",
                table: "role_requests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "role_requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "payment_information",
                table: "withdrawal_requests");

            migrationBuilder.DropColumn(
                name: "status",
                table: "role_requests");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "creation_time",
                table: "role_requests",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<bool>(
                name: "is_approved",
                table: "role_requests",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
