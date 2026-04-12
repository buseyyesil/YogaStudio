using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YogaStudio.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLessonCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Lessons");
        }
    }
}
