using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_1___COMP2139.Migrations
{
    /// <inheritdoc />
    public partial class PurchasesSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 10, 24, 19, 58, 40, 928, DateTimeKind.Utc).AddTicks(6556));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 10, 29, 19, 58, 40, 928, DateTimeKind.Utc).AddTicks(6563));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2025, 11, 3, 19, 58, 40, 928, DateTimeKind.Utc).AddTicks(6565));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2025, 11, 8, 19, 58, 40, 928, DateTimeKind.Utc).AddTicks(6566));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2025, 11, 13, 19, 58, 40, 928, DateTimeKind.Utc).AddTicks(6568));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2025, 11, 18, 19, 58, 40, 928, DateTimeKind.Utc).AddTicks(6569));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7,
                column: "Date",
                value: new DateTime(2025, 11, 23, 19, 58, 40, 928, DateTimeKind.Utc).AddTicks(6570));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8,
                column: "Date",
                value: new DateTime(2025, 11, 28, 19, 58, 40, 928, DateTimeKind.Utc).AddTicks(6572));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 9,
                column: "Date",
                value: new DateTime(2025, 12, 3, 19, 58, 40, 928, DateTimeKind.Utc).AddTicks(6573));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 10,
                column: "Date",
                value: new DateTime(2025, 12, 8, 19, 58, 40, 928, DateTimeKind.Utc).AddTicks(6575));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 11,
                column: "Date",
                value: new DateTime(2025, 12, 13, 19, 58, 40, 928, DateTimeKind.Utc).AddTicks(6576));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 12,
                column: "Date",
                value: new DateTime(2025, 12, 18, 19, 58, 40, 928, DateTimeKind.Utc).AddTicks(6577));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 10, 24, 19, 31, 31, 954, DateTimeKind.Utc).AddTicks(9552));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 10, 29, 19, 31, 31, 954, DateTimeKind.Utc).AddTicks(9559));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2025, 11, 3, 19, 31, 31, 954, DateTimeKind.Utc).AddTicks(9561));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2025, 11, 8, 19, 31, 31, 954, DateTimeKind.Utc).AddTicks(9562));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2025, 11, 13, 19, 31, 31, 954, DateTimeKind.Utc).AddTicks(9564));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2025, 11, 18, 19, 31, 31, 954, DateTimeKind.Utc).AddTicks(9565));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7,
                column: "Date",
                value: new DateTime(2025, 11, 23, 19, 31, 31, 954, DateTimeKind.Utc).AddTicks(9567));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8,
                column: "Date",
                value: new DateTime(2025, 11, 28, 19, 31, 31, 954, DateTimeKind.Utc).AddTicks(9568));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 9,
                column: "Date",
                value: new DateTime(2025, 12, 3, 19, 31, 31, 954, DateTimeKind.Utc).AddTicks(9570));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 10,
                column: "Date",
                value: new DateTime(2025, 12, 8, 19, 31, 31, 954, DateTimeKind.Utc).AddTicks(9571));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 11,
                column: "Date",
                value: new DateTime(2025, 12, 13, 19, 31, 31, 954, DateTimeKind.Utc).AddTicks(9572));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 12,
                column: "Date",
                value: new DateTime(2025, 12, 18, 19, 31, 31, 954, DateTimeKind.Utc).AddTicks(9574));
        }
    }
}
