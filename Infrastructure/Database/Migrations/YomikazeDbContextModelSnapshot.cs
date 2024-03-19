﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Yomikaze.Infrastructure.Database;

#nullable disable

namespace Yomikaze.Infrastructure.Database.Migrations
{
    [DbContext(typeof(YomikazeDbContext))]
    partial class YomikazeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Chapter", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTimeOffset?>("Available")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ComicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ComicId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Comic", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Aliases")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Authors")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Banner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cover")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("Ended")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("Published")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Comics");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.ComicGenre", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ComicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("GenreId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTimeOffset?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("ComicId");

                    b.HasIndex("GenreId");

                    b.ToTable("ComicGenres");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ComicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ReplyToId")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ComicId");

                    b.HasIndex("ReplyToId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Genre", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = "28308392250441728",
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 3, 19, 2, 47, 26, 657, DateTimeKind.Unspecified).AddTicks(7778), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "A story that focuses on physical action, such as fighting, war, sports, or physical challenges.",
                            Name = "Action"
                        },
                        new
                        {
                            Id = "28308392250441729",
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 3, 19, 2, 47, 26, 657, DateTimeKind.Unspecified).AddTicks(8381), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown.",
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = "28308392250441730",
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 3, 19, 2, 47, 26, 657, DateTimeKind.Unspecified).AddTicks(8388), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "A story with humorous narration or dialogue, intended to amuse the audience.",
                            Name = "Comedy"
                        },
                        new
                        {
                            Id = "28308392250441731",
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 3, 19, 2, 47, 26, 657, DateTimeKind.Unspecified).AddTicks(8391), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist.",
                            Name = "Drama"
                        },
                        new
                        {
                            Id = "28308392250441732",
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 3, 19, 2, 47, 26, 657, DateTimeKind.Unspecified).AddTicks(8393), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements.",
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = "28308392250441733",
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 3, 19, 2, 47, 26, 657, DateTimeKind.Unspecified).AddTicks(8395), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "A story that evokes fear in both the characters and the audience.",
                            Name = "Horror"
                        },
                        new
                        {
                            Id = "28308392250441734",
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 3, 19, 2, 47, 26, 657, DateTimeKind.Unspecified).AddTicks(8397), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "A story that revolves around solving a puzzle or a crime.",
                            Name = "Mystery"
                        },
                        new
                        {
                            Id = "28308392250441735",
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 3, 19, 2, 47, 26, 657, DateTimeKind.Unspecified).AddTicks(8399), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "A story that emphasizes the psychology of its characters and their unstable emotional states.",
                            Name = "Psychological"
                        },
                        new
                        {
                            Id = "28308392250441736",
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 3, 19, 2, 47, 26, 657, DateTimeKind.Unspecified).AddTicks(8401), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "A story about love.",
                            Name = "Romance"
                        },
                        new
                        {
                            Id = "28308392250441737",
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 3, 19, 2, 47, 26, 657, DateTimeKind.Unspecified).AddTicks(8403), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "A story that portrays a \"cut-out\" sequence of events in a character's life.",
                            Name = "Slice of Life"
                        },
                        new
                        {
                            Id = "28308392250441738",
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 3, 19, 2, 47, 26, 657, DateTimeKind.Unspecified).AddTicks(8405), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "A story that revolves around sports, such as baseball or basketball.",
                            Name = "Sports"
                        },
                        new
                        {
                            Id = "28308392250441739",
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 3, 19, 2, 47, 26, 657, DateTimeKind.Unspecified).AddTicks(8407), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "A story that involves supernatural elements, such as ghosts or demons.",
                            Name = "Supernatural"
                        },
                        new
                        {
                            Id = "28308392250441740",
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 3, 19, 2, 47, 26, 657, DateTimeKind.Unspecified).AddTicks(8471), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "A story that is fast-paced and suspenseful, often involving a crime.",
                            Name = "Thriller"
                        },
                        new
                        {
                            Id = "28308392250441741",
                            CreatedAt = new DateTimeOffset(new DateTime(2024, 3, 19, 2, 47, 26, 657, DateTimeKind.Unspecified).AddTicks(8474), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "A story that ends in a tragic or unhappy way.",
                            Name = "Tragedy"
                        });
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.HistoryRecord", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ChapterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.HasIndex("UserId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.LibraryEntry", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ComicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateAdded")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ComicId");

                    b.HasIndex("UserId");

                    b.ToTable("LibraryEntries");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Notification", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("Read")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Page", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ChapterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "28308392254676992",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "28308392254676993",
                            Name = "Publisher",
                            NormalizedName = "PUBLISHER"
                        },
                        new
                        {
                            Id = "28308392254676994",
                            Name = "Reader",
                            NormalizedName = "READER"
                        });
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Banner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("Birthday")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Fullname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Yomikaze.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Yomikaze.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Yomikaze.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Yomikaze.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yomikaze.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Yomikaze.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Chapter", b =>
                {
                    b.HasOne("Yomikaze.Domain.Entities.Comic", "Comic")
                        .WithMany("Chapters")
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comic");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.ComicGenre", b =>
                {
                    b.HasOne("Yomikaze.Domain.Entities.Comic", "Comic")
                        .WithMany("ComicGenres")
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yomikaze.Domain.Entities.Genre", "Genre")
                        .WithMany("ComicGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comic");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Comment", b =>
                {
                    b.HasOne("Yomikaze.Domain.Entities.Comic", "Comic")
                        .WithMany()
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yomikaze.Domain.Entities.Comment", "ReplyTo")
                        .WithMany("Replies")
                        .HasForeignKey("ReplyToId");

                    b.HasOne("Yomikaze.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comic");

                    b.Navigation("ReplyTo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.HistoryRecord", b =>
                {
                    b.HasOne("Yomikaze.Domain.Entities.Chapter", "Chapter")
                        .WithMany("Trackers")
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yomikaze.Domain.Entities.User", "User")
                        .WithMany("History")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chapter");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.LibraryEntry", b =>
                {
                    b.HasOne("Yomikaze.Domain.Entities.Comic", "Comic")
                        .WithMany()
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yomikaze.Domain.Entities.User", "User")
                        .WithMany("Library")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Notification", b =>
                {
                    b.HasOne("Yomikaze.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Page", b =>
                {
                    b.HasOne("Yomikaze.Domain.Entities.Chapter", "Chapter")
                        .WithMany("Pages")
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chapter");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Chapter", b =>
                {
                    b.Navigation("Pages");

                    b.Navigation("Trackers");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Comic", b =>
                {
                    b.Navigation("Chapters");

                    b.Navigation("ComicGenres");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Comment", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.Genre", b =>
                {
                    b.Navigation("ComicGenres");
                });

            modelBuilder.Entity("Yomikaze.Domain.Entities.User", b =>
                {
                    b.Navigation("History");

                    b.Navigation("Library");
                });
#pragma warning restore 612, 618
        }
    }
}
