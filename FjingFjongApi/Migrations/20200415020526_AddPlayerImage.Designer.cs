﻿// <auto-generated />
using System;
using FjingFjongApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FjingFjongApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200415020526_AddPlayerImage")]
    partial class AddPlayerImage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FjingFjongApi.Models.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PlayerFourId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerOneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerThreeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerTwoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TeamOneScore")
                        .HasColumnType("int");

                    b.Property<int>("TeamTwoScore")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PlayerFourId");

                    b.HasIndex("PlayerOneId");

                    b.HasIndex("PlayerThreeId");

                    b.HasIndex("PlayerTwoId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("FjingFjongApi.Models.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(1000.0);

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("FjingFjongApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FjingFjongApi.Models.Match", b =>
                {
                    b.HasOne("FjingFjongApi.Models.Player", "PlayerFour")
                        .WithMany("MatchesFour")
                        .HasForeignKey("PlayerFourId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FjingFjongApi.Models.Player", "PlayerOne")
                        .WithMany("MatchesOne")
                        .HasForeignKey("PlayerOneId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FjingFjongApi.Models.Player", "PlayerThree")
                        .WithMany("MatchesThree")
                        .HasForeignKey("PlayerThreeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FjingFjongApi.Models.Player", "PlayerTwo")
                        .WithMany("MatchesTwo")
                        .HasForeignKey("PlayerTwoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
