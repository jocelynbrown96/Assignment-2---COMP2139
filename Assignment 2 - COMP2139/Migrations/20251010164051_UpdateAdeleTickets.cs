using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_1___COMP2139.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdeleTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 10, 20, 16, 40, 51, 28, DateTimeKind.Utc).AddTicks(7072));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 10, 30, 16, 40, 51, 28, DateTimeKind.Utc).AddTicks(7078));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2025, 11, 9, 16, 40, 51, 28, DateTimeKind.Utc).AddTicks(7080));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2025, 11, 19, 16, 40, 51, 28, DateTimeKind.Utc).AddTicks(7081));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AvailableTickets", "Date" },
                values: new object[] { 4, new DateTime(2025, 11, 29, 16, 40, 51, 28, DateTimeKind.Utc).AddTicks(7083) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2025, 12, 9, 16, 40, 51, 28, DateTimeKind.Utc).AddTicks(7084));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7,
                column: "Date",
                value: new DateTime(2025, 10, 25, 16, 40, 51, 28, DateTimeKind.Utc).AddTicks(7085));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8,
                column: "Date",
                value: new DateTime(2025, 11, 4, 16, 40, 51, 28, DateTimeKind.Utc).AddTicks(7086));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 9,
                column: "Date",
                value: new DateTime(2025, 11, 14, 16, 40, 51, 28, DateTimeKind.Utc).AddTicks(7087));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 10,
                column: "Date",
                value: new DateTime(2025, 11, 24, 16, 40, 51, 28, DateTimeKind.Utc).AddTicks(7089));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 11,
                column: "Date",
                value: new DateTime(2025, 12, 4, 16, 40, 51, 28, DateTimeKind.Utc).AddTicks(7090));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 12,
                column: "Date",
                value: new DateTime(2025, 12, 14, 16, 40, 51, 28, DateTimeKind.Utc).AddTicks(7091));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 10, 20, 16, 33, 57, 784, DateTimeKind.Utc).AddTicks(2605));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 10, 30, 16, 33, 57, 784, DateTimeKind.Utc).AddTicks(2611));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2025, 11, 9, 16, 33, 57, 784, DateTimeKind.Utc).AddTicks(2612));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2025, 11, 19, 16, 33, 57, 784, DateTimeKind.Utc).AddTicks(2614));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AvailableTickets", "Date" },
                values: new object[] { 250, new DateTime(2025, 11, 29, 16, 33, 57, 784, DateTimeKind.Utc).AddTicks(2615) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2025, 12, 9, 16, 33, 57, 784, DateTimeKind.Utc).AddTicks(2616));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7,
                column: "Date",
                value: new DateTime(2025, 10, 25, 16, 33, 57, 784, DateTimeKind.Utc).AddTicks(2618));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8,
                column: "Date",
                value: new DateTime(2025, 11, 4, 16, 33, 57, 784, DateTimeKind.Utc).AddTicks(2619));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 9,
                column: "Date",
                value: new DateTime(2025, 11, 14, 16, 33, 57, 784, DateTimeKind.Utc).AddTicks(2620));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 10,
                column: "Date",
                value: new DateTime(2025, 11, 24, 16, 33, 57, 784, DateTimeKind.Utc).AddTicks(2621));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 11,
                column: "Date",
                value: new DateTime(2025, 12, 4, 16, 33, 57, 784, DateTimeKind.Utc).AddTicks(2623));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 12,
                column: "Date",
                value: new DateTime(2025, 12, 14, 16, 33, 57, 784, DateTimeKind.Utc).AddTicks(2624));
        }
    }
}
