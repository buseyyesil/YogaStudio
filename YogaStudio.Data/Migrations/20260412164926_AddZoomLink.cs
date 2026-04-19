using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YogaStudio.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddZoomLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ZoomLink",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZoomLink",
                table: "Lessons");
        }
    }
}
