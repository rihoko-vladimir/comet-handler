using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CometHandler.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NameType = table.Column<string>(type: "text", nullable: false),
                    RecordedClassification = table.Column<string>(type: "text", nullable: false),
                    Mass = table.Column<int>(type: "integer", nullable: true),
                    Fall = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    RecorderLatitude = table.Column<string>(type: "text", nullable: true),
                    RecorderLongitude = table.Column<string>(type: "text", nullable: true),
                    Geolocation_Type = table.Column<string>(type: "text", nullable: true),
                    Geolocation_Coordinates = table.Column<List<double>>(type: "double precision[]", nullable: true),
                    ComputedRegionCbhkFwbd = table.Column<string>(type: "text", nullable: true),
                    ComputedRegionNnqa25f4 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comets_Name",
                table: "Comets",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Comets_RecordedClassification",
                table: "Comets",
                column: "RecordedClassification");

            migrationBuilder.CreateIndex(
                name: "IX_Comets_Year",
                table: "Comets",
                column: "Year");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comets");
        }
    }
}
