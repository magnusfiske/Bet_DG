﻿// <auto-generated />
using Bet.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bet.Data.Migrations
{
    [DbContext(typeof(BetContext))]
    [Migration("20231024164754_SeedDataAdded")]
    partial class SeedDataAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bet.Data.Entities.Bet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("Bet.Data.Entities.BetRow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BetId")
                        .HasColumnType("int");

                    b.Property<int>("Placing")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BetId");

                    b.HasIndex("TeamId")
                        .IsUnique();

                    b.ToTable("BetRows");
                });

            modelBuilder.Entity("Bet.Data.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Hammarby IF"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Malmö FF"
                        },
                        new
                        {
                            Id = 3,
                            Name = "IF Elfsborg"
                        },
                        new
                        {
                            Id = 4,
                            Name = "BK Häcken"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Djurgården"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Kalmar FF"
                        },
                        new
                        {
                            Id = 7,
                            Name = "IFK Norrköping"
                        },
                        new
                        {
                            Id = 8,
                            Name = "IFK Värnamo"
                        },
                        new
                        {
                            Id = 9,
                            Name = "IK Sirius"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Mjällby AIF"
                        },
                        new
                        {
                            Id = 11,
                            Name = "AIK"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Hamlstad BK"
                        },
                        new
                        {
                            Id = 13,
                            Name = "IFK Göteborg"
                        },
                        new
                        {
                            Id = 14,
                            Name = "IF Brommapojkarna"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Degerfors IF"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Varberg BoIS"
                        });
                });

            modelBuilder.Entity("Bet.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<bool>("Submited")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Bet.Data.Entities.Bet", b =>
                {
                    b.HasOne("Bet.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bet.Data.Entities.BetRow", b =>
                {
                    b.HasOne("Bet.Data.Entities.Bet", "Bet")
                        .WithMany("BetRows")
                        .HasForeignKey("BetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bet.Data.Entities.Team", "Team")
                        .WithOne("BetRow")
                        .HasForeignKey("Bet.Data.Entities.BetRow", "TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bet");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Bet.Data.Entities.Bet", b =>
                {
                    b.Navigation("BetRows");
                });

            modelBuilder.Entity("Bet.Data.Entities.Team", b =>
                {
                    b.Navigation("BetRow");
                });
#pragma warning restore 612, 618
        }
    }
}
