using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApplicationDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class component : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "Date", "Status", "Text", "UserId" },
                values: new object[] { 5, new DateTime(2024, 6, 5, 17, 3, 55, 950, DateTimeKind.Local).AddTicks(8203), 0, "prosto text", 1 });
        }
    }
}
