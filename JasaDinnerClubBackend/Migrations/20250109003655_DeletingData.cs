using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JasaDinnerClubBackend.Migrations
{
    /// <inheritdoc />
    public partial class DeletingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DinnerEvents",
                keyColumn: "DinnerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Attendee",
                keyColumn: "AttendeeId",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Attendee",
                columns: new[] { "AttendeeId", "AttendeeName", "AttendeeNumber" },
                values: new object[] { 1, "John Doe", "123-456-7890" });

            migrationBuilder.InsertData(
                table: "DinnerEvents",
                columns: new[] { "DinnerId", "Capacity", "Date", "Description", "Name", "Time" },
                values: new object[] { 1, 6, new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Local), "Exclusive wine tasting", "Wine Night", new TimeSpan(0, 19, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "AttendeeId", "DinnerEventDinnerId", "DinnerId", "Request" },
                values: new object[] { 1, 1, null, 1, "Vegetarian meal" });
        }
    }
}
