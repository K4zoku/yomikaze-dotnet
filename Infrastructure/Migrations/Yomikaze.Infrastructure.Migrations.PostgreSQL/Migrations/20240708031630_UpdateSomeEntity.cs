using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSomeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_library_entries_library_categories_category_id",
                table: "library_entries");

            migrationBuilder.DropIndex(
                name: "ix_library_entries_category_id",
                table: "library_entries");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "library_entries");

            migrationBuilder.AddColumn<decimal>(
                name: "library_entry_id",
                table: "library_categories",
                type: "numeric(20,0)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "stripe_price_id",
                table: "coin_pricings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "chapters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
                keyValue: 67464207515254787m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 360, DateTimeKind.Unspecified).AddTicks(9160), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254788m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 360, DateTimeKind.Unspecified).AddTicks(9165), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "tag_categories",
                columns: new[] { "id", "creation_time", "name" },
                values: new object[,]
                {
                    { 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 360, DateTimeKind.Unspecified).AddTicks(8762), new TimeSpan(0, 0, 0, 0, 0)), "Format" },
                    { 67464207515254789m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 360, DateTimeKind.Unspecified).AddTicks(9167), new TimeSpan(0, 0, 0, 0, 0)), "Content" }
                });

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
                columns: new[] { "category_id", "creation_time" },
                values: new object[] { 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(793), new TimeSpan(0, 0, 0, 0, 0)) });

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

            migrationBuilder.InsertData(
                table: "tags",
                columns: new[] { "id", "category_id", "creation_time", "description", "name" },
                values: new object[,]
                {
                    { 67464207515254803m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(678), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on romantic relationships between \"male\" characters.", "Boys' Love" },
                    { 67464207515254804m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(682), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around a crime, such as a thief", "Crime" },
                    { 67464207515254805m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(686), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on romantic relationships between \"female\" characters.", "Girls' Love" },
                    { 67464207515254806m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(688), new TimeSpan(0, 0, 0, 0, 0)), "A story that takes place in the past.", "Historical" },
                    { 67464207515254807m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(691), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves characters being transported to another world.", "Isekai" },
                    { 67464207515254808m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(693), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around spell-casting and transforming heroines.", "Magical Girls" },
                    { 67464207515254809m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(694), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around giant robots.", "Mecha" },
                    { 67464207515254810m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(696), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around medical procedures and practices.", "Medical" },
                    { 67464207515254811m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(701), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around science fiction elements, such as space travel or hi-tech gadgets.", "Sci-Fi" },
                    { 67464207515254812m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(707), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around characters with superhuman abilities.", "Superhero" },
                    { 67464207515254813m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(711), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around martial arts and chivalry in ancient China.", "Wuxia" },
                    { 67464207515254814m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(712), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves extraterrestrial beings.", "Aliens" },
                    { 67464207515254815m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(714), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves animals.", "Animals" },
                    { 67464207515254816m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(757), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around cooking.", "Cooking" },
                    { 67464207515254817m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(759), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves characters dressing as the opposite gender.", "Crossdressing" },
                    { 67464207515254818m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(761), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves delinquent characters.", "Delinquents" },
                    { 67464207515254819m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(762), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves demonic beings.", "Demons" },
                    { 67464207515254820m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(764), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves characters swapping their gender.", "Genderswap" },
                    { 67464207515254821m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(765), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves ghostly beings.", "Ghosts" },
                    { 67464207515254822m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(766), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves gyaru characters.", "Gyaru" },
                    { 67464207515254823m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(768), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves a man that is surrounded by multiple girls.", "Harem" },
                    { 67464207515254824m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(769), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves incestuous relationships.", "Incest" },
                    { 67464207515254825m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(771), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves little girl", "Loli" },
                    { 67464207515254826m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(772), new TimeSpan(0, 0, 0, 0, 0)), "", "Mafia" },
                    { 67464207515254827m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(773), new TimeSpan(0, 0, 0, 0, 0)), "", "Magic" },
                    { 67464207515254828m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(775), new TimeSpan(0, 0, 0, 0, 0)), "", "Martial Arts" },
                    { 67464207515254829m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(776), new TimeSpan(0, 0, 0, 0, 0)), "", "Military" },
                    { 67464207515254830m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(777), new TimeSpan(0, 0, 0, 0, 0)), "", "Monster Girls" },
                    { 67464207515254831m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(779), new TimeSpan(0, 0, 0, 0, 0)), "", "Monsters" },
                    { 67464207515254832m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(780), new TimeSpan(0, 0, 0, 0, 0)), "", "Music" },
                    { 67464207515254833m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(781), new TimeSpan(0, 0, 0, 0, 0)), "", "Ninja" },
                    { 67464207515254834m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(783), new TimeSpan(0, 0, 0, 0, 0)), "", "Office Workers" },
                    { 67464207515254835m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(784), new TimeSpan(0, 0, 0, 0, 0)), "", "Police" },
                    { 67464207515254836m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(785), new TimeSpan(0, 0, 0, 0, 0)), "", "Post-Apocalyptic" },
                    { 67464207515254837m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(787), new TimeSpan(0, 0, 0, 0, 0)), "", "Reincarnation" },
                    { 67464207515254838m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(788), new TimeSpan(0, 0, 0, 0, 0)), "", "Reversed Harem" },
                    { 67464207515254839m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(789), new TimeSpan(0, 0, 0, 0, 0)), "", "Samurai" },
                    { 67464207515254840m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(791), new TimeSpan(0, 0, 0, 0, 0)), "", "School Life" },
                    { 67464207515254841m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(792), new TimeSpan(0, 0, 0, 0, 0)), "", "Shota" },
                    { 67464207515254842m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(795), new TimeSpan(0, 0, 0, 0, 0)), "", "Survival" },
                    { 67464207515254843m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(796), new TimeSpan(0, 0, 0, 0, 0)), "", "Time Travel" },
                    { 67464207515254844m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(797), new TimeSpan(0, 0, 0, 0, 0)), "", "Traditional Games" },
                    { 67464207515254845m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(799), new TimeSpan(0, 0, 0, 0, 0)), "", "Vampires" },
                    { 67464207515254846m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(800), new TimeSpan(0, 0, 0, 0, 0)), "", "Video Games" },
                    { 67464207515254847m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(801), new TimeSpan(0, 0, 0, 0, 0)), "", "Villainess" },
                    { 67464207515254848m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(803), new TimeSpan(0, 0, 0, 0, 0)), "", "Virtual Reality" },
                    { 67464207515254849m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(804), new TimeSpan(0, 0, 0, 0, 0)), "", "Zombies" },
                    { 67464207515254850m, 67464207515254789m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(805), new TimeSpan(0, 0, 0, 0, 0)), "A story that contains graphic violence.", "Gore" },
                    { 67464207515254851m, 67464207515254789m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(806), new TimeSpan(0, 0, 0, 0, 0)), "A story that contains sexual violence.", "Sexual Violence" },
                    { 67464207515254877m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(49), new TimeSpan(0, 0, 0, 0, 0)), "A comic strip format that consists of four panels.", "4-Koma" },
                    { 67464207515254878m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(655), new TimeSpan(0, 0, 0, 0, 0)), "A comic that is based on a previously existing work.", "Adaption" },
                    { 67464207515254879m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(659), new TimeSpan(0, 0, 0, 0, 0)), "A collection of stories or poems by different authors.", "Anthology" },
                    { 67464207515254880m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(661), new TimeSpan(0, 0, 0, 0, 0)), "A comic that has won an award.", "Award Winning" },
                    { 67464207515254881m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(662), new TimeSpan(0, 0, 0, 0, 0)), "A comic that based on a previously existing work, created by a fan.", "Doujinshi" },
                    { 67464207515254882m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(664), new TimeSpan(0, 0, 0, 0, 0)), "A comic that has been colored by a fan.", "Fan colored" },
                    { 67464207515254883m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(666), new TimeSpan(0, 0, 0, 0, 0)), "A comic that is fully colored.", "Full Color" },
                    { 67464207515254884m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(667), new TimeSpan(0, 0, 0, 0, 0)), "A comic that consists of a long strip of panels.", "Long Strip" },
                    { 67464207515254885m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(669), new TimeSpan(0, 0, 0, 0, 0)), "A comic that has been officially colored.", "Official Colored" },
                    { 67464207515254886m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(670), new TimeSpan(0, 0, 0, 0, 0)), "A comic that is a single, standalone story.", "Oneshot" },
                    { 67464207515254887m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(672), new TimeSpan(0, 0, 0, 0, 0)), "A comic that has been published by the creator.", "Self-Published" },
                    { 67464207515254888m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 3, 16, 29, 361, DateTimeKind.Unspecified).AddTicks(673), new TimeSpan(0, 0, 0, 0, 0)), "A comic that is published on the internet.", "Web Comic" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_library_categories_library_entries_library_entry_id",
                table: "library_categories");

            migrationBuilder.DropIndex(
                name: "ix_library_categories_library_entry_id",
                table: "library_categories");

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254803m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254804m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254805m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254806m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254807m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254808m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254809m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254810m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254811m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254812m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254813m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254814m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254815m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254816m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254817m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254818m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254819m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254820m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254821m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254822m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254823m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254824m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254825m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254826m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254827m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254828m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254829m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254830m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254831m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254832m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254833m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254834m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254835m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254836m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254837m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254838m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254839m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254840m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254841m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254842m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254843m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254844m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254845m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254846m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254847m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254848m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254849m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254850m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254851m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254877m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254878m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254879m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254880m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254881m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254882m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254883m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254884m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254885m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254886m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254887m);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254888m);

            migrationBuilder.DeleteData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254786m);

            migrationBuilder.DeleteData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254789m);

            migrationBuilder.DropColumn(
                name: "library_entry_id",
                table: "library_categories");

            migrationBuilder.DropColumn(
                name: "stripe_price_id",
                table: "coin_pricings");

            migrationBuilder.DropColumn(
                name: "price",
                table: "chapters");

            migrationBuilder.AddColumn<decimal>(
                name: "category_id",
                table: "library_entries",
                type: "numeric(20,0)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207511101440m,
                column: "concurrency_stamp",
                value: "fb1e935d-799f-418e-8032-882d9604b819");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295744m,
                column: "concurrency_stamp",
                value: "0087db43-71b7-4528-94e1-cec49d4ac944");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295745m,
                column: "concurrency_stamp",
                value: "bbd047ee-5ddf-4db0-b2f0-3e4563548da6");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 67464207515295746m,
                column: "concurrency_stamp",
                value: "5372e28a-4a82-46ee-9e40-9a02a41426e3");

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254787m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(5166), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tag_categories",
                keyColumn: "id",
                keyValue: 67464207515254788m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(5470), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254789m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6218), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254790m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6762), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254791m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6766), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254792m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6769), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254793m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6772), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254794m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6775), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254795m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6780), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254796m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6782), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254797m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6785), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254798m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6787), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254799m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6791), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254800m,
                columns: new[] { "category_id", "creation_time" },
                values: new object[] { 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6793), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254801m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6796), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "tags",
                keyColumn: "id",
                keyValue: 67464207515254802m,
                column: "creation_time",
                value: new DateTimeOffset(new DateTime(2024, 7, 5, 3, 58, 40, 496, DateTimeKind.Unspecified).AddTicks(6800), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "ix_library_entries_category_id",
                table: "library_entries",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "fk_library_entries_library_categories_category_id",
                table: "library_entries",
                column: "category_id",
                principalTable: "library_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
