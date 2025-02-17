using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Flight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, defaultValue: ""),
                    Destination = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, defaultValue: ""),
                    Provider = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, defaultValue: ""),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TakeOffAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightPassenger",
                columns: table => new
                {
                    FlightId = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Seat = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ReservedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightPassenger", x => new { x.FlightId, x.Id });
                    table.ForeignKey(
                        name: "FK_FlightPassenger_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Origin_Destination_Provider",
                table: "Flights",
                columns: new[] { "Origin", "Destination", "Provider" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightPassenger");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
