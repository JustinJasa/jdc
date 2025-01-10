using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JasaDinnerClubBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddDataTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DinnerEvents",
                keyColumn: "DinnerId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DinnerEvents",
                keyColumn: "DinnerId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
