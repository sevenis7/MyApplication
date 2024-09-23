using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApplicationDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ProjectModelChanged : Migration
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
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Components",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Projects",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Components",
                newName: "id");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Projects",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
