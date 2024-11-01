using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolMangment.Migrations
{
    /// <inheritdoc />
    public partial class removeidforexam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "studentExams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "studentExams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
