using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyApplicationDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Projects",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "user", "USER" },
                    { 2, null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Projects",
                newName: "Text");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Projects",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
