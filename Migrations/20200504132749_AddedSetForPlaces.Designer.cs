﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NGK_Assignment_3.Areas.Database;

namespace NGK_Assignment_3.Migrations
{
    [DbContext(typeof(NGKDbContext))]
    [Migration("20200504132749_AddedSetForPlaces")]
    partial class AddedSetForPlaces
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NGK_Assignment_3.Areas.Database.Models.Measurement", b =>
                {
                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<int>("Humidity")
                        .HasColumnType("int");

                    b.Property<double>("PlaceLat")
                        .HasColumnType("float");

                    b.Property<double>("PlaceLon")
                        .HasColumnType("float");

                    b.Property<float>("Pressure")
                        .HasColumnType("real");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.HasKey("Time");

                    b.HasIndex("PlaceLat", "PlaceLon");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("NGK_Assignment_3.Areas.Database.Models.Place", b =>
                {
                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lon")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Lat", "Lon");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("NGK_Assignment_3.Areas.Database.Models.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NGK_Assignment_3.Areas.Database.Models.Measurement", b =>
                {
                    b.HasOne("NGK_Assignment_3.Areas.Database.Models.Place", "Place")
                        .WithMany("Measurements")
                        .HasForeignKey("PlaceLat", "PlaceLon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
