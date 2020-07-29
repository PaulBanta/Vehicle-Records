﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VehicleRecords.Data;

namespace VehicleRecords.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200729181229_Registration")]
    partial class Registration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VehicleRecords.Models.Fillup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Date")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<int>("DaysSinceLastFillup")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<decimal>("Gallons")
                        .HasColumnType("decimal(7, 3)");

                    b.Property<decimal>("MilesPerDay")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(5, 1)")
                        .HasComputedColumnSql("iif( [DaysSinceLastFillup] > 1, [TripOdometer] / [DaysSinceLastFillup], [TripOdometer] )");

                    b.Property<decimal>("MilesPerGallon")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(5, 2)")
                        .HasComputedColumnSql("iif( [Gallons] > 0, [TripOdometer] / [Gallons], 999.9 )");

                    b.Property<int>("Odometer")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePerDay")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(6, 2)")
                        .HasComputedColumnSql("iif( [DaysSinceLastFillup] > 1, [TotalCost] / [DaysSinceLastFillup], [TotalCost] )");

                    b.Property<decimal>("PricePerGallon")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(5, 3)")
                        .HasComputedColumnSql("iif( [Gallons] > 0, [TotalCost] / [Gallons], 999.9 )");

                    b.Property<decimal>("PricePerMile")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(4, 2)")
                        .HasComputedColumnSql("iif( [TripOdometer] > 0, [TotalCost] / [TripOdometer], 999.9 )");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(7, 2)");

                    b.Property<decimal>("TripOdometer")
                        .HasColumnType("decimal(6, 1)");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Fillup","VehRec");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2020, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DaysSinceLastFillup = 0,
                            Gallons = 10.001m,
                            MilesPerDay = 0m,
                            MilesPerGallon = 0m,
                            Odometer = 300,
                            PricePerDay = 0m,
                            PricePerGallon = 0m,
                            PricePerMile = 0m,
                            TotalCost = 24.99m,
                            TripOdometer = 300.1m,
                            VehicleId = 1
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2020, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DaysSinceLastFillup = 6,
                            Gallons = 9.001m,
                            MilesPerDay = 0m,
                            MilesPerGallon = 0m,
                            Odometer = 600,
                            PricePerDay = 0m,
                            PricePerGallon = 0m,
                            PricePerMile = 0m,
                            TotalCost = 22m,
                            TripOdometer = 300.1m,
                            VehicleId = 1
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2020, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DaysSinceLastFillup = 9,
                            Gallons = 12.001m,
                            MilesPerDay = 0m,
                            MilesPerGallon = 0m,
                            Odometer = 900,
                            PricePerDay = 0m,
                            PricePerGallon = 0m,
                            PricePerMile = 0m,
                            TotalCost = 22m,
                            TripOdometer = 300.1m,
                            VehicleId = 1
                        },
                        new
                        {
                            Id = 4,
                            Date = new DateTime(2020, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DaysSinceLastFillup = 6,
                            Gallons = 12.001m,
                            MilesPerDay = 0m,
                            MilesPerGallon = 0m,
                            Odometer = 1350,
                            PricePerDay = 0m,
                            PricePerGallon = 0m,
                            PricePerMile = 0m,
                            TotalCost = 22m,
                            TripOdometer = 450.1m,
                            VehicleId = 1
                        });
                });

            modelBuilder.Entity("VehicleRecords.Models.Insurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<decimal>("CostPerMonth")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(7, 2)")
                        .HasComputedColumnSql("iif( [NumberOfMonths] > 0, [TotalCost] / [NumberOfMonths], 999.9 )");

                    b.Property<string>("Coverage")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<DateTime?>("Date")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<int>("NumberOfMonths")
                        .HasColumnType("int");

                    b.Property<string>("PolicyNumber")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(7, 2)");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Insurance","VehRec");
                });

            modelBuilder.Entity("VehicleRecords.Models.Maintenance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BriefDescriptionOfWork")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<DateTime?>("Date")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<string>("FullDescriptionOfWork")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<int?>("Odometer")
                        .HasColumnType("int");

                    b.Property<string>("PerformedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(7, 2)");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Maintenance","VehRec");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BriefDescriptionOfWork = "Oil Change",
                            Date = new DateTime(2020, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Odometer = 200000,
                            PerformedBy = "Self",
                            TotalCost = 22.49m,
                            VehicleId = 1
                        },
                        new
                        {
                            Id = 2,
                            BriefDescriptionOfWork = "Replace Clutch",
                            Date = new DateTime(2020, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FullDescriptionOfWork = @"Parts: $150
Labor: $245
Warranty: 6 Months / 6,000 Miles",
                            Odometer = 200150,
                            PerformedBy = "Woodmoor Conoco",
                            TotalCost = 395m,
                            VehicleId = 1
                        });
                });

            modelBuilder.Entity("VehicleRecords.Models.Registration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Date")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(6, 2)");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Registration","VehRec");
                });

            modelBuilder.Entity("VehicleRecords.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.ToTable("User","Account");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmailAddress = "85FDFD0FB6DFE3AFED031983A1EAEC69ADB8E91CFCEB9FA3EBFAA6984C1E564541CCA57A965FD4C6ACF6632EB0130F42F70E4E52EA038B111B6E16461F2165CD",
                            Password = "87D77AE90DAA4EC8F1FB1C144D0D851785720688A4C52D9D6EC223A631BA5578AA2F18977BB40A018DCE92A95810BBEDB33E94E528EB99EC5A61348485C86853"
                        },
                        new
                        {
                            Id = 2,
                            EmailAddress = "4F988DDAFEE8760D1B56532E2C1C356EAC82DF48EA37F47E86F93230032236C64A0183C9C8670DC9D68D1F5F52E1A6F474FB88ACCA637DD0592894AB442EAA12",
                            Password = "A7F059EAF5955D7BDDDE7E4F5C245A62E66FFFF88C2001A511A6D7E7B6F4F30F94EFF44902FC217E7B27A38D4A9594FBEBA09760DBD659F97830E5251FCC4914"
                        });
                });

            modelBuilder.Entity("VehicleRecords.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("DatePurchased")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DateSold")
                        .HasColumnType("date");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<int?>("OdometerAtPurchase")
                        .HasColumnType("int");

                    b.Property<int?>("OdometerAtSale")
                        .HasColumnType("int");

                    b.Property<decimal?>("PurchasePrice")
                        .HasColumnType("money");

                    b.Property<decimal?>("SalePrice")
                        .HasColumnType("money");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Vin")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Vehicle","VehRec");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "Gold",
                            DatePurchased = new DateTime(1995, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateSold = new DateTime(2010, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Make = "Saturn",
                            Model = "SW2",
                            OdometerAtPurchase = 10,
                            OdometerAtSale = 234000,
                            PurchasePrice = 12500m,
                            SalePrice = 1000m,
                            UserId = 1,
                            Year = 1995
                        },
                        new
                        {
                            Id = 2,
                            Color = "Black",
                            Make = "Honda",
                            Model = "CR-V",
                            UserId = 1,
                            Year = 2001
                        },
                        new
                        {
                            Id = 3,
                            Color = "Gold",
                            Make = "Chevrolet",
                            Model = "Malibu",
                            UserId = 1,
                            Year = 2007
                        },
                        new
                        {
                            Id = 4,
                            Color = "Gold",
                            Make = "Subaru",
                            Model = "Forester",
                            UserId = 1,
                            Year = 2008
                        },
                        new
                        {
                            Id = 5,
                            Color = "Blue",
                            Make = "Mazda",
                            Model = "3",
                            UserId = 1,
                            Year = 2012
                        });
                });

            modelBuilder.Entity("VehicleRecords.Models.Fillup", b =>
                {
                    b.HasOne("VehicleRecords.Models.Vehicle", "Vehicle")
                        .WithMany("Fillups")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VehicleRecords.Models.Insurance", b =>
                {
                    b.HasOne("VehicleRecords.Models.Vehicle", "Vehicle")
                        .WithMany("Insurance")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VehicleRecords.Models.Maintenance", b =>
                {
                    b.HasOne("VehicleRecords.Models.Vehicle", "Vehicle")
                        .WithMany("Maintenance")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VehicleRecords.Models.Registration", b =>
                {
                    b.HasOne("VehicleRecords.Models.Vehicle", "Vehicle")
                        .WithMany("Registrations")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VehicleRecords.Models.Vehicle", b =>
                {
                    b.HasOne("VehicleRecords.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
