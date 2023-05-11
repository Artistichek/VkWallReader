﻿// <auto-generated />
using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VkWallReader.DAL.EF;

#nullable disable

namespace VkWallReader.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DAL.Entities.CountedWall", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<JsonDocument>("CountedLetters")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("Domain")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Hash")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("CountedWalls");
                });
#pragma warning restore 612, 618
        }
    }
}
