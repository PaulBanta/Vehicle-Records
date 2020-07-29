using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleRecords.Migrations
{
    public partial class Registration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                schema: "VehRec",
                table: "Fillup",
                keyColumn: "Id",
                keyValue: 2,
                column: "DaysSinceLastFillup",
                value: 6);

            migrationBuilder.UpdateData(
                schema: "VehRec",
                table: "Fillup",
                keyColumn: "Id",
                keyValue: 3,
                column: "DaysSinceLastFillup",
                value: 9);

            migrationBuilder.UpdateData(
                schema: "VehRec",
                table: "Fillup",
                keyColumn: "Id",
                keyValue: 4,
                column: "DaysSinceLastFillup",
                value: 6);

            migrationBuilder.CreateIndex(
                name: "IX_Registration_VehicleId",
                schema: "VehRec",
                table: "Registration",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registration",
                schema: "VehRec");

            migrationBuilder.UpdateData(
                schema: "VehRec",
                table: "Fillup",
                keyColumn: "Id",
                keyValue: 2,
                column: "DaysSinceLastFillup",
                value: 6);

            migrationBuilder.UpdateData(
                schema: "VehRec",
                table: "Fillup",
                keyColumn: "Id",
                keyValue: 3,
                column: "DaysSinceLastFillup",
                value: 9);

            migrationBuilder.UpdateData(
                schema: "VehRec",
                table: "Fillup",
                keyColumn: "Id",
                keyValue: 4,
                column: "DaysSinceLastFillup",
                value: 6);
        }
    }
}
