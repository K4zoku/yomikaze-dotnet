using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddStripeCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "stripe_customer_id",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "stripe_ephemeral_key",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "comment",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207511101440m,
                column: "concurrency_stamp",
                value: "4f2762b0-6f7d-4363-babd-ecc08bc0c832");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295744m,
                column: "concurrency_stamp",
                value: "49e172c9-8db0-4fda-810c-f7981a53815b");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295745m,
                column: "concurrency_stamp",
                value: "a09af224-ebec-4cb0-9fda-57af9f9ff554");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295746m,
                column: "concurrency_stamp",
                value: "47e0fa72-de5e-4d66-a665-14fd39ff4ec7");

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254786m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(1102), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254787m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(1409), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254788m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(1413), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254789m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(1414), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254789m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3037), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254790m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3038), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254791m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3041), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254792m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3106), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254793m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3108), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254794m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3113), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254795m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3120), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254796m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3121), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254797m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3123), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254798m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3126), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254799m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3127), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254800m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3202), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254801m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3131), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254802m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3133), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254803m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3040), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254804m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3043), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254805m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3109), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254806m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3111), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254807m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3114), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254808m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3116), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254809m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3117), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254810m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3119), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254811m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3124), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254812m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3129), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254813m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3134), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254814m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3135), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254815m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3137), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254816m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3138), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254817m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3140), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254818m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3141), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254819m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3143), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254820m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3144), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254821m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3145), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254822m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3147), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254823m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3148), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254824m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3150), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254825m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3151), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254826m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3153), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254827m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3154), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254828m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3155), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254829m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3157), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254830m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3158), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254831m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3159), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254832m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3161), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254833m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3162), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254834m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3164), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254835m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3192), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254836m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3193), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254837m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3195), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254838m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3196), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254839m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3198), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254840m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3199), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254841m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3201), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254842m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3203), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254843m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3205), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254844m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3206), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254845m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3208), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254846m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3209), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254847m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3211), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254848m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3212), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254849m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3213), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254850m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3215), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254851m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3216), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254877m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(2443), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254878m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3015), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254879m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254880m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3022), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254881m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3024), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254882m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3025), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254883m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3027), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254884m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3029), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254885m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3031), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254886m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3033), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254887m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3034), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254888m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 19, 6, 22, 58, 640, DateTimeKind.Unspecified).AddTicks(3035), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stripe_customer_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "stripe_ephemeral_key",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "comment",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1024)",
                oldMaxLength: 1024);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207511101440m,
                column: "concurrency_stamp",
                value: "a463d649-579f-4dac-9c2b-2003771edd3a");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295744m,
                column: "concurrency_stamp",
                value: "db4d9a74-c4fd-4a04-8922-f0b76eee2c24");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295745m,
                column: "concurrency_stamp",
                value: "ff33e1bf-a857-43db-9f16-a85ba7037148");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295746m,
                column: "concurrency_stamp",
                value: "7ae547f3-022c-4521-85a2-470200e12b99");

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254786m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 428, DateTimeKind.Unspecified).AddTicks(8832), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254787m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 428, DateTimeKind.Unspecified).AddTicks(9127), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254788m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 428, DateTimeKind.Unspecified).AddTicks(9131), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254789m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 428, DateTimeKind.Unspecified).AddTicks(9133), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254789m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(704), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254790m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(706), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254791m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(808), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254792m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(811), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254793m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(813), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254794m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(817), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254795m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(825), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254796m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(826), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254797m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(828), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254798m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(831), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254799m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(832), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254800m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(912), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254801m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(835), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254802m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(836), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254803m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(707), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254804m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(809), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254805m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(814), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254806m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(816), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254807m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(819), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254808m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(820), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254809m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(822), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254810m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(823), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254811m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(829), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254812m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(833), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254813m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(838), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254814m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(839), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254815m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(840), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254816m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(842), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254817m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(843), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254818m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(845), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254819m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(846), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254820m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(848), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254821m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(849), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254822m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(850), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254823m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(852), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254824m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(853), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254825m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(855), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254826m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(856), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254827m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(857), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254828m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(859), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254829m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(860), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254830m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(861), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254831m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(863), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254832m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(896), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254833m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(898), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254834m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(900), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254835m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(901), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254836m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(903), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254837m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(904), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254838m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(906), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254839m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(908), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254840m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254841m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(911), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254842m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(913), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254843m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(915), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254844m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(916), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254845m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(918), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254846m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(919), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254847m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(920), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254848m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(922), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254849m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(923), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254850m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(925), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254851m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(926), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254877m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 428, DateTimeKind.Unspecified).AddTicks(9965), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254878m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(681), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254879m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(687), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254880m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(690), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254881m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(691), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254882m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(693), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254883m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(695), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254884m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(696), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254885m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(698), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254886m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(700), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254887m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(701), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254888m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(703), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
