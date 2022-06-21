using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinFormsnEntityFrameworkCoreAppKursova.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExcursionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcursionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    FuelConsumption = table.Column<int>(type: "int", nullable: false),
                    BDriverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buses_Drivers_BDriverId",
                        column: x => x.BDriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Excursions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfExcursions = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfTourists = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExcCustomerId = table.Column<int>(type: "int", nullable: false),
                    ExcTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excursions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Excursions_Customers_ExcCustomerId",
                        column: x => x.ExcCustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Excursions_ExcursionTypes_ExcTypeId",
                        column: x => x.ExcTypeId,
                        principalTable: "ExcursionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusExcursionType",
                columns: table => new
                {
                    BusesId = table.Column<int>(type: "int", nullable: false),
                    ExcursionTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusExcursionType", x => new { x.BusesId, x.ExcursionTypesId });
                    table.ForeignKey(
                        name: "FK_BusExcursionType_Buses_BusesId",
                        column: x => x.BusesId,
                        principalTable: "Buses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusExcursionType_ExcursionTypes_ExcursionTypesId",
                        column: x => x.ExcursionTypesId,
                        principalTable: "ExcursionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusExcursion",
                columns: table => new
                {
                    BusesId = table.Column<int>(type: "int", nullable: false),
                    ExcursionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusExcursion", x => new { x.BusesId, x.ExcursionsId });
                    table.ForeignKey(
                        name: "FK_BusExcursion_Buses_BusesId",
                        column: x => x.BusesId,
                        principalTable: "Buses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusExcursion_Excursions_ExcursionsId",
                        column: x => x.ExcursionsId,
                        principalTable: "Excursions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buses_BDriverId",
                table: "Buses",
                column: "BDriverId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusExcursion_ExcursionsId",
                table: "BusExcursion",
                column: "ExcursionsId");

            migrationBuilder.CreateIndex(
                name: "IX_BusExcursionType_ExcursionTypesId",
                table: "BusExcursionType",
                column: "ExcursionTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_ExcCustomerId",
                table: "Excursions",
                column: "ExcCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_ExcTypeId",
                table: "Excursions",
                column: "ExcTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusExcursion");

            migrationBuilder.DropTable(
                name: "BusExcursionType");

            migrationBuilder.DropTable(
                name: "Excursions");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ExcursionTypes");

            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
