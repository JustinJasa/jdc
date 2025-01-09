using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JasaDinnerClubBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendee",
                columns: table => new
                {
                    AttendeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttendeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttendeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendee", x => x.AttendeeId);
                });

            migrationBuilder.CreateTable(
                name: "DinnerEvents",
                columns: table => new
                {
                    DinnerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DinnerEvents", x => x.DinnerId);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DinnerId = table.Column<int>(type: "int", nullable: false),
                    DinnerEventDinnerId = table.Column<int>(type: "int", nullable: true),
                    AttendeeId = table.Column<int>(type: "int", nullable: false),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Attendee_AttendeeId",
                        column: x => x.AttendeeId,
                        principalTable: "Attendee",
                        principalColumn: "AttendeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_DinnerEvents_DinnerEventDinnerId",
                        column: x => x.DinnerEventDinnerId,
                        principalTable: "DinnerEvents",
                        principalColumn: "DinnerId");
                });

            migrationBuilder.InsertData(
                table: "Attendee",
                columns: new[] { "AttendeeId", "AttendeeName", "AttendeeNumber" },
                values: new object[] { 1, "John Doe", "123-456-7890" });

            migrationBuilder.InsertData(
                table: "DinnerEvents",
                columns: new[] { "DinnerId", "Capacity", "Date", "Description", "Name", "Time" },
                values: new object[] { 1, 6, new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), "Exclusive wine tasting", "Wine Night", new TimeSpan(0, 19, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "AttendeeId", "DinnerEventDinnerId", "DinnerId", "Request" },
                values: new object[] { 1, 1, null, 1, "Vegetarian meal" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AttendeeId",
                table: "Bookings",
                column: "AttendeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DinnerEventDinnerId",
                table: "Bookings",
                column: "DinnerEventDinnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Attendee");

            migrationBuilder.DropTable(
                name: "DinnerEvents");
        }
    }
}
