using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleRecords.Migrations
{
    public partial class FullCrud_UserVehicleFillupMaintenanceInsuranceRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "VehRec");

            migrationBuilder.EnsureSchema(
                name: "Account");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(maxLength: 128, nullable: false),
                    Password = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                schema: "VehRec",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(nullable: false),
                    Make = table.Column<string>(maxLength: 40, nullable: false),
                    Model = table.Column<string>(maxLength: 40, nullable: false),
                    Color = table.Column<string>(maxLength: 20, nullable: true),
                    Vin = table.Column<string>(maxLength: 20, nullable: true),
                    DatePurchased = table.Column<DateTime>(type: "date", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "money", nullable: true),
                    OdometerAtPurchase = table.Column<int>(nullable: true),
                    DateSold = table.Column<DateTime>(type: "date", nullable: true),
                    SalePrice = table.Column<decimal>(type: "money", nullable: true),
                    OdometerAtSale = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicle_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Account",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fillup",
                schema: "VehRec",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Gallons = table.Column<decimal>(type: "decimal(7, 3)", nullable: false),
                    Odometer = table.Column<int>(nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(7, 2)", nullable: false),
                    TripOdometer = table.Column<decimal>(type: "decimal(6, 1)", nullable: false),
                    DaysSinceLastFillup = table.Column<int>(nullable: false, defaultValue: 1),
                    MilesPerDay = table.Column<decimal>(type: "decimal(5, 1)", nullable: false, computedColumnSql: "iif( [DaysSinceLastFillup] > 1, [TripOdometer] / [DaysSinceLastFillup], [TripOdometer] )"),
                    MilesPerGallon = table.Column<decimal>(type: "decimal(5, 2)", nullable: false, computedColumnSql: "iif( [Gallons] > 0, [TripOdometer] / [Gallons], 999.9 )"),
                    PricePerDay = table.Column<decimal>(type: "decimal(6, 2)", nullable: false, computedColumnSql: "iif( [DaysSinceLastFillup] > 1, [TotalCost] / [DaysSinceLastFillup], [TotalCost] )"),
                    PricePerGallon = table.Column<decimal>(type: "decimal(5, 3)", nullable: false, computedColumnSql: "iif( [Gallons] > 0, [TotalCost] / [Gallons], 999.9 )"),
                    PricePerMile = table.Column<decimal>(type: "decimal(4, 2)", nullable: false, computedColumnSql: "iif( [TripOdometer] > 0, [TotalCost] / [TripOdometer], 999.9 )"),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fillup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fillup_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalSchema: "VehRec",
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Insurance",
                schema: "VehRec",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    NumberOfMonths = table.Column<int>(nullable: false),
                    Company = table.Column<string>(maxLength: 40, nullable: false),
                    PolicyNumber = table.Column<string>(maxLength: 40, nullable: true),
                    Coverage = table.Column<string>(maxLength: 40, nullable: true),
                    TotalCost = table.Column<decimal>(type: "decimal(7, 2)", nullable: false),
                    CostPerMonth = table.Column<decimal>(type: "decimal(7, 2)", nullable: false, computedColumnSql: "iif( [NumberOfMonths] > 0, [TotalCost] / [NumberOfMonths], 999.9 )"),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insurance_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalSchema: "VehRec",
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maintenance",
                schema: "VehRec",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    PerformedBy = table.Column<string>(maxLength: 40, nullable: false),
                    BriefDescriptionOfWork = table.Column<string>(maxLength: 40, nullable: false),
                    FullDescriptionOfWork = table.Column<string>(maxLength: 1000, nullable: true),
                    Odometer = table.Column<int>(nullable: true),
                    TotalCost = table.Column<decimal>(type: "decimal(7, 2)", nullable: false),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintenance_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalSchema: "VehRec",
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                schema: "VehRec",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    State = table.Column<string>(maxLength: 20, nullable: true),
                    TotalCost = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registration_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalSchema: "VehRec",
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Account",
                table: "User",
                columns: new[] { "Id", "EmailAddress", "Password" },
                values: new object[] { 1, "85FDFD0FB6DFE3AFED031983A1EAEC69ADB8E91CFCEB9FA3EBFAA6984C1E564541CCA57A965FD4C6ACF6632EB0130F42F70E4E52EA038B111B6E16461F2165CD", "87D77AE90DAA4EC8F1FB1C144D0D851785720688A4C52D9D6EC223A631BA5578AA2F18977BB40A018DCE92A95810BBEDB33E94E528EB99EC5A61348485C86853" });

            migrationBuilder.InsertData(
                schema: "Account",
                table: "User",
                columns: new[] { "Id", "EmailAddress", "Password" },
                values: new object[] { 2, "4F988DDAFEE8760D1B56532E2C1C356EAC82DF48EA37F47E86F93230032236C64A0183C9C8670DC9D68D1F5F52E1A6F474FB88ACCA637DD0592894AB442EAA12", "A7F059EAF5955D7BDDDE7E4F5C245A62E66FFFF88C2001A511A6D7E7B6F4F30F94EFF44902FC217E7B27A38D4A9594FBEBA09760DBD659F97830E5251FCC4914" });

            migrationBuilder.CreateIndex(
                name: "IX_User_EmailAddress",
                schema: "Account",
                table: "User",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fillup_VehicleId",
                schema: "VehRec",
                table: "Fillup",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurance_VehicleId",
                schema: "VehRec",
                table: "Insurance",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_VehicleId",
                schema: "VehRec",
                table: "Maintenance",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_VehicleId",
                schema: "VehRec",
                table: "Registration",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_UserId",
                schema: "VehRec",
                table: "Vehicle",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fillup",
                schema: "VehRec");

            migrationBuilder.DropTable(
                name: "Insurance",
                schema: "VehRec");

            migrationBuilder.DropTable(
                name: "Maintenance",
                schema: "VehRec");

            migrationBuilder.DropTable(
                name: "Registration",
                schema: "VehRec");

            migrationBuilder.DropTable(
                name: "Vehicle",
                schema: "VehRec");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Account");
        }
    }
}
