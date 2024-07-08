using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Yomikaze.Infrastructure.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "coin_pricings",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false),
                    discount = table.Column<double>(type: "double precision", nullable: false),
                    stripe_price_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_coin_pricings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "report_categories",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    requires_description = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_report_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tag_categories",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    avatar = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    banner = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    bio = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    birthday = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    balance = table.Column<long>(type: "bigint", nullable: false),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_claims_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    category_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.id);
                    table.ForeignKey(
                        name: "fk_tags_tag_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "tag_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comics",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    aliases = table.Column<string[]>(type: "text[]", nullable: false),
                    description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    cover = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    banner = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    publication_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    authors = table.Column<string[]>(type: "text[]", nullable: false),
                    publisher_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comics", x => x.id);
                    table.ForeignKey(
                        name: "fk_comics_users_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "library_categories",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_library_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_library_categories_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    content = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    read = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notifications", x => x.id);
                    table.ForeignKey(
                        name: "fk_notifications_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_transactions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_claims_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_user_logins_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    role_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                columns: table => new
                {
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_user_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "withdrawal_requests",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    profile_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_withdrawal_requests", x => x.id);
                    table.ForeignKey(
                        name: "fk_withdrawal_requests_users_profile_id",
                        column: x => x.profile_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chapters",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    comic_id = table.Column<decimal>(type: "numeric(20,0)", maxLength: 20, nullable: false),
                    views = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chapters", x => x.id);
                    table.ForeignKey(
                        name: "fk_chapters_comics_comic_id",
                        column: x => x.comic_id,
                        principalTable: "comics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comic_ratings",
                columns: table => new
                {
                    comic_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comic_ratings", x => new { x.comic_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_comic_ratings_comics_comic_id",
                        column: x => x.comic_id,
                        principalTable: "comics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comic_ratings_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comic_tag",
                columns: table => new
                {
                    comic_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    tag_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comic_tag", x => new { x.comic_id, x.tag_id });
                    table.ForeignKey(
                        name: "fk_comic_tag_comics_comic_id",
                        column: x => x.comic_id,
                        principalTable: "comics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comic_tag_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "library_entries",
                columns: table => new
                {
                    comic_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_library_entries", x => x.id);
                    table.ForeignKey(
                        name: "fk_library_entries_comics_comic_id",
                        column: x => x.comic_id,
                        principalTable: "comics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_library_entries_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    content = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    author_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    reply_to_id = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    type = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    chapter_id = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    comic_id = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    profile_id = table.Column<decimal>(type: "numeric(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment", x => x.id);
                    table.ForeignKey(
                        name: "fk_comment_chapters_chapter_id",
                        column: x => x.chapter_id,
                        principalTable: "chapters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comment_comics_comic_id",
                        column: x => x.comic_id,
                        principalTable: "comics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comment_comment_reply_to_id",
                        column: x => x.reply_to_id,
                        principalTable: "comment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_comment_users_author_id",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comment_users_profile_id",
                        column: x => x.profile_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "history_records",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    chapter_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    page_number = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_history_records", x => x.id);
                    table.ForeignKey(
                        name: "fk_history_records_chapters_chapter_id",
                        column: x => x.chapter_id,
                        principalTable: "chapters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_history_records_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pages",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    image = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    chapter_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pages", x => x.id);
                    table.ForeignKey(
                        name: "fk_pages_chapters_chapter_id",
                        column: x => x.chapter_id,
                        principalTable: "chapters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "unlocked_chapters",
                columns: table => new
                {
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    chapter_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unlocked_chapters", x => new { x.user_id, x.chapter_id });
                    table.ForeignKey(
                        name: "fk_unlocked_chapters_chapters_chapter_id",
                        column: x => x.chapter_id,
                        principalTable: "chapters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_unlocked_chapters_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "comment_reactions",
                columns: table => new
                {
                    comment_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    reaction_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment_reactions", x => new { x.comment_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_comment_reactions_comment_comment_id",
                        column: x => x.comment_id,
                        principalTable: "comment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comment_reactions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "translations",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    user_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    page_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    x = table.Column<int>(type: "integer", nullable: false),
                    y = table.Column<int>(type: "integer", nullable: false),
                    width = table.Column<int>(type: "integer", nullable: false),
                    height = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    alignment = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_translations", x => x.id);
                    table.ForeignKey(
                        name: "fk_translations_pages_page_id",
                        column: x => x.page_id,
                        principalTable: "pages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_translations_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report",
                columns: table => new
                {
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", rowVersion: true, nullable: true),
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    category_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    reporter_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    images = table.Column<string[]>(type: "text[]", nullable: true),
                    dismissal_reason = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    chapter_id = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    comic_id = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    profile_id = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    translation_id = table.Column<decimal>(type: "numeric(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_report", x => x.id);
                    table.ForeignKey(
                        name: "fk_report_chapters_chapter_id",
                        column: x => x.chapter_id,
                        principalTable: "chapters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_report_comics_comic_id",
                        column: x => x.comic_id,
                        principalTable: "comics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_report_report_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "report_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_report_translations_translation_id",
                        column: x => x.translation_id,
                        principalTable: "translations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_report_users_profile_id",
                        column: x => x.profile_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_report_users_reporter_id",
                        column: x => x.reporter_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { 67464207511101440m, "a463d649-579f-4dac-9c2b-2003771edd3a", "Super", "SUPER" },
                    { 67464207515295744m, "db4d9a74-c4fd-4a04-8922-f0b76eee2c24", "Administrator", "ADMINISTRATOR" },
                    { 67464207515295745m, "ff33e1bf-a857-43db-9f16-a85ba7037148", "Publisher", "PUBLISHER" },
                    { 67464207515295746m, "7ae547f3-022c-4521-85a2-470200e12b99", "Reader", "READER" }
                });

            migrationBuilder.InsertData(
                table: "tag_categories",
                columns: new[] { "id", "creation_time", "name" },
                values: new object[,]
                {
                    { 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 428, DateTimeKind.Unspecified).AddTicks(8832), new TimeSpan(0, 0, 0, 0, 0)), "Format" },
                    { 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 428, DateTimeKind.Unspecified).AddTicks(9127), new TimeSpan(0, 0, 0, 0, 0)), "Genre" },
                    { 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 428, DateTimeKind.Unspecified).AddTicks(9131), new TimeSpan(0, 0, 0, 0, 0)), "Theme" },
                    { 67464207515254789m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 428, DateTimeKind.Unspecified).AddTicks(9133), new TimeSpan(0, 0, 0, 0, 0)), "Content" }
                });

            migrationBuilder.InsertData(
                table: "tags",
                columns: new[] { "id", "category_id", "creation_time", "description", "name" },
                values: new object[,]
                {
                    { 67464207515254789m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(704), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on physical action, such as fighting, war, sports, or physical challenges.", "Action" },
                    { 67464207515254790m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(706), new TimeSpan(0, 0, 0, 0, 0)), "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown.", "Adventure" },
                    { 67464207515254791m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(808), new TimeSpan(0, 0, 0, 0, 0)), "A story with humorous narration or dialogue, intended to amuse the audience.", "Comedy" },
                    { 67464207515254792m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(811), new TimeSpan(0, 0, 0, 0, 0)), "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist.", "Drama" },
                    { 67464207515254793m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(813), new TimeSpan(0, 0, 0, 0, 0)), "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements.", "Fantasy" },
                    { 67464207515254794m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(817), new TimeSpan(0, 0, 0, 0, 0)), "A story that evokes fear in both the characters and the audience.", "Horror" },
                    { 67464207515254795m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(825), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around solving a puzzle or a crime.", "Mystery" },
                    { 67464207515254796m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(826), new TimeSpan(0, 0, 0, 0, 0)), "A story that emphasizes the psychology of its characters and their unstable emotional states.", "Psychological" },
                    { 67464207515254797m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(828), new TimeSpan(0, 0, 0, 0, 0)), "A story about love.", "Romance" },
                    { 67464207515254798m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(831), new TimeSpan(0, 0, 0, 0, 0)), "A story that portrays a \"cut-out\" sequence of events in a character's life.", "Slice of Life" },
                    { 67464207515254799m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(832), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around sports, such as baseball or basketball.", "Sports" },
                    { 67464207515254800m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(912), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves supernatural elements, such as ghosts or demons.", "Supernatural" },
                    { 67464207515254801m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(835), new TimeSpan(0, 0, 0, 0, 0)), "A story that is fast-paced and suspenseful, often involving a crime.", "Thriller" },
                    { 67464207515254802m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(836), new TimeSpan(0, 0, 0, 0, 0)), "A story that ends in a tragic or unhappy way.", "Tragedy" },
                    { 67464207515254803m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(707), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on romantic relationships between \"male\" characters.", "Boys' Love" },
                    { 67464207515254804m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(809), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around a crime, such as a thief", "Crime" },
                    { 67464207515254805m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(814), new TimeSpan(0, 0, 0, 0, 0)), "A story that focuses on romantic relationships between \"female\" characters.", "Girls' Love" },
                    { 67464207515254806m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(816), new TimeSpan(0, 0, 0, 0, 0)), "A story that takes place in the past.", "Historical" },
                    { 67464207515254807m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(819), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves characters being transported to another world.", "Isekai" },
                    { 67464207515254808m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(820), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around spell-casting and transforming heroines.", "Magical Girls" },
                    { 67464207515254809m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(822), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around giant robots.", "Mecha" },
                    { 67464207515254810m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(823), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around medical procedures and practices.", "Medical" },
                    { 67464207515254811m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(829), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around science fiction elements, such as space travel or hi-tech gadgets.", "Sci-Fi" },
                    { 67464207515254812m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(833), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around characters with superhuman abilities.", "Superhero" },
                    { 67464207515254813m, 67464207515254787m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(838), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around martial arts and chivalry in ancient China.", "Wuxia" },
                    { 67464207515254814m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(839), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves extraterrestrial beings.", "Aliens" },
                    { 67464207515254815m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(840), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves animals.", "Animals" },
                    { 67464207515254816m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(842), new TimeSpan(0, 0, 0, 0, 0)), "A story that revolves around cooking.", "Cooking" },
                    { 67464207515254817m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(843), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves characters dressing as the opposite gender.", "Crossdressing" },
                    { 67464207515254818m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(845), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves delinquent characters.", "Delinquents" },
                    { 67464207515254819m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(846), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves demonic beings.", "Demons" },
                    { 67464207515254820m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(848), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves characters swapping their gender.", "Genderswap" },
                    { 67464207515254821m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(849), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves ghostly beings.", "Ghosts" },
                    { 67464207515254822m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(850), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves gyaru characters.", "Gyaru" },
                    { 67464207515254823m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(852), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves a man that is surrounded by multiple girls.", "Harem" },
                    { 67464207515254824m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(853), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves incestuous relationships.", "Incest" },
                    { 67464207515254825m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(855), new TimeSpan(0, 0, 0, 0, 0)), "A story that involves little girl", "Loli" },
                    { 67464207515254826m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(856), new TimeSpan(0, 0, 0, 0, 0)), "", "Mafia" },
                    { 67464207515254827m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(857), new TimeSpan(0, 0, 0, 0, 0)), "", "Magic" },
                    { 67464207515254828m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(859), new TimeSpan(0, 0, 0, 0, 0)), "", "Martial Arts" },
                    { 67464207515254829m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(860), new TimeSpan(0, 0, 0, 0, 0)), "", "Military" },
                    { 67464207515254830m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(861), new TimeSpan(0, 0, 0, 0, 0)), "", "Monster Girls" },
                    { 67464207515254831m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(863), new TimeSpan(0, 0, 0, 0, 0)), "", "Monsters" },
                    { 67464207515254832m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(896), new TimeSpan(0, 0, 0, 0, 0)), "", "Music" },
                    { 67464207515254833m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(898), new TimeSpan(0, 0, 0, 0, 0)), "", "Ninja" },
                    { 67464207515254834m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(900), new TimeSpan(0, 0, 0, 0, 0)), "", "Office Workers" },
                    { 67464207515254835m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(901), new TimeSpan(0, 0, 0, 0, 0)), "", "Police" },
                    { 67464207515254836m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(903), new TimeSpan(0, 0, 0, 0, 0)), "", "Post-Apocalyptic" },
                    { 67464207515254837m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(904), new TimeSpan(0, 0, 0, 0, 0)), "", "Reincarnation" },
                    { 67464207515254838m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(906), new TimeSpan(0, 0, 0, 0, 0)), "", "Reversed Harem" },
                    { 67464207515254839m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(908), new TimeSpan(0, 0, 0, 0, 0)), "", "Samurai" },
                    { 67464207515254840m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), "", "School Life" },
                    { 67464207515254841m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(911), new TimeSpan(0, 0, 0, 0, 0)), "", "Shota" },
                    { 67464207515254842m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(913), new TimeSpan(0, 0, 0, 0, 0)), "", "Survival" },
                    { 67464207515254843m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(915), new TimeSpan(0, 0, 0, 0, 0)), "", "Time Travel" },
                    { 67464207515254844m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(916), new TimeSpan(0, 0, 0, 0, 0)), "", "Traditional Games" },
                    { 67464207515254845m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(918), new TimeSpan(0, 0, 0, 0, 0)), "", "Vampires" },
                    { 67464207515254846m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(919), new TimeSpan(0, 0, 0, 0, 0)), "", "Video Games" },
                    { 67464207515254847m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(920), new TimeSpan(0, 0, 0, 0, 0)), "", "Villainess" },
                    { 67464207515254848m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(922), new TimeSpan(0, 0, 0, 0, 0)), "", "Virtual Reality" },
                    { 67464207515254849m, 67464207515254788m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(923), new TimeSpan(0, 0, 0, 0, 0)), "", "Zombies" },
                    { 67464207515254850m, 67464207515254789m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(925), new TimeSpan(0, 0, 0, 0, 0)), "A story that contains graphic violence.", "Gore" },
                    { 67464207515254851m, 67464207515254789m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(926), new TimeSpan(0, 0, 0, 0, 0)), "A story that contains sexual violence.", "Sexual Violence" },
                    { 67464207515254877m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 428, DateTimeKind.Unspecified).AddTicks(9965), new TimeSpan(0, 0, 0, 0, 0)), "A comic strip format that consists of four panels.", "4-Koma" },
                    { 67464207515254878m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(681), new TimeSpan(0, 0, 0, 0, 0)), "A comic that is based on a previously existing work.", "Adaption" },
                    { 67464207515254879m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(687), new TimeSpan(0, 0, 0, 0, 0)), "A collection of stories or poems by different authors.", "Anthology" },
                    { 67464207515254880m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(690), new TimeSpan(0, 0, 0, 0, 0)), "A comic that has won an award.", "Award Winning" },
                    { 67464207515254881m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(691), new TimeSpan(0, 0, 0, 0, 0)), "A comic that based on a previously existing work, created by a fan.", "Doujinshi" },
                    { 67464207515254882m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(693), new TimeSpan(0, 0, 0, 0, 0)), "A comic that has been colored by a fan.", "Fan colored" },
                    { 67464207515254883m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(695), new TimeSpan(0, 0, 0, 0, 0)), "A comic that is fully colored.", "Full Color" },
                    { 67464207515254884m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(696), new TimeSpan(0, 0, 0, 0, 0)), "A comic that consists of a long strip of panels.", "Long Strip" },
                    { 67464207515254885m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(698), new TimeSpan(0, 0, 0, 0, 0)), "A comic that has been officially colored.", "Official Colored" },
                    { 67464207515254886m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(700), new TimeSpan(0, 0, 0, 0, 0)), "A comic that is a single, standalone story.", "Oneshot" },
                    { 67464207515254887m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(701), new TimeSpan(0, 0, 0, 0, 0)), "A comic that has been published by the creator.", "Self-Published" },
                    { 67464207515254888m, 67464207515254786m, new DateTimeOffset(new DateTime(2024, 7, 8, 9, 25, 22, 429, DateTimeKind.Unspecified).AddTicks(703), new TimeSpan(0, 0, 0, 0, 0)), "A comic that is published on the internet.", "Web Comic" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_chapters_comic_id",
                table: "chapters",
                column: "comic_id");

            migrationBuilder.CreateIndex(
                name: "ix_comic_ratings_user_id",
                table: "comic_ratings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_comic_tag_comic_id_tag_id",
                table: "comic_tag",
                columns: new[] { "comic_id", "tag_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_comic_tag_tag_id",
                table: "comic_tag",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "ix_comics_publisher_id",
                table: "comics",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_author_id",
                table: "comment",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_chapter_id",
                table: "comment",
                column: "chapter_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_comic_id",
                table: "comment",
                column: "comic_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_profile_id",
                table: "comment",
                column: "profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_reply_to_id",
                table: "comment",
                column: "reply_to_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_reactions_user_id",
                table: "comment_reactions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_history_records_chapter_id",
                table: "history_records",
                column: "chapter_id");

            migrationBuilder.CreateIndex(
                name: "ix_history_records_user_id",
                table: "history_records",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_library_categories_user_id_name",
                table: "library_categories",
                columns: new[] { "user_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_library_entries_comic_id_user_id",
                table: "library_entries",
                columns: new[] { "comic_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_library_entries_user_id",
                table: "library_entries",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_library_entry_category_category_id",
                table: "library_entry_category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications_user_id",
                table: "notifications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_pages_chapter_id",
                table: "pages",
                column: "chapter_id");

            migrationBuilder.CreateIndex(
                name: "ix_report_category_id",
                table: "report",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_report_chapter_id",
                table: "report",
                column: "chapter_id");

            migrationBuilder.CreateIndex(
                name: "ix_report_comic_id",
                table: "report",
                column: "comic_id");

            migrationBuilder.CreateIndex(
                name: "ix_report_profile_id",
                table: "report",
                column: "profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_report_reporter_id",
                table: "report",
                column: "reporter_id");

            migrationBuilder.CreateIndex(
                name: "ix_report_translation_id",
                table: "report",
                column: "translation_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_claims_role_id",
                table: "role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tags_category_id",
                table: "tags",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_tags_name_category_id",
                table: "tags",
                columns: new[] { "name", "category_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_transactions_user_id",
                table: "transactions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_translations_page_id",
                table: "translations",
                column: "page_id");

            migrationBuilder.CreateIndex(
                name: "ix_translations_user_id",
                table: "translations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_unlocked_chapters_chapter_id",
                table: "unlocked_chapters",
                column: "chapter_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_claims_user_id",
                table: "user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_logins_user_id",
                table: "user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "users",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_withdrawal_requests_profile_id",
                table: "withdrawal_requests",
                column: "profile_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "coin_pricings");

            migrationBuilder.DropTable(
                name: "comic_ratings");

            migrationBuilder.DropTable(
                name: "comic_tag");

            migrationBuilder.DropTable(
                name: "comment_reactions");

            migrationBuilder.DropTable(
                name: "history_records");

            migrationBuilder.DropTable(
                name: "library_entry_category");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "report");

            migrationBuilder.DropTable(
                name: "role_claims");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "unlocked_chapters");

            migrationBuilder.DropTable(
                name: "user_claims");

            migrationBuilder.DropTable(
                name: "user_logins");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_tokens");

            migrationBuilder.DropTable(
                name: "withdrawal_requests");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "library_categories");

            migrationBuilder.DropTable(
                name: "library_entries");

            migrationBuilder.DropTable(
                name: "report_categories");

            migrationBuilder.DropTable(
                name: "translations");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "tag_categories");

            migrationBuilder.DropTable(
                name: "pages");

            migrationBuilder.DropTable(
                name: "chapters");

            migrationBuilder.DropTable(
                name: "comics");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
