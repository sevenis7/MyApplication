using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApplicationDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RequestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "Date", "Status", "Text", "UserId" },
                values: new object[] { 5, new DateTime(2024, 6, 5, 17, 3, 55, 950, DateTimeKind.Local).AddTicks(8203), 0, "prosto text", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
