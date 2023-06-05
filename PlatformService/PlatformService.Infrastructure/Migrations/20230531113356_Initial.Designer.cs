﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlatformService.Infrastructure.Persistence;

#nullable disable

namespace PlatformService.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230531113356_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PlatformService.Domain.Platform", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cost")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Platforms", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("cfa55f0d-3014-4f3b-968b-425f25559971"),
                            Cost = "Free",
                            Name = "Dot Net",
                            Publisher = "Microsoft"
                        },
                        new
                        {
                            Id = new Guid("2739f4c5-9421-4572-a1ee-2f3e855e6a41"),
                            Cost = "Free",
                            Name = "SQL Server Express",
                            Publisher = "Microsoft"
                        },
                        new
                        {
                            Id = new Guid("4738b7c9-05ba-4a58-b54f-a40fba7a578f"),
                            Cost = "Free",
                            Name = "Kubernetes",
                            Publisher = "Cloud Native Computing Foundation"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
