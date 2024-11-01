using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolMangment.Migrations
{
    /// <inheritdoc />
    public partial class createTeachertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_Teacher_teacher_id",
                table: "courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teacher",
                table: "Teacher");

            migrationBuilder.RenameTable(
                name: "Teacher",
                newName: "teachers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_teachers",
                table: "teachers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_teachers_teacher_id",
                table: "courses",
                column: "teacher_id",
                principalTable: "teachers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_teachers_teacher_id",
                table: "courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_teachers",
                table: "teachers");

            migrationBuilder.RenameTable(
                name: "teachers",
                newName: "Teacher");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teacher",
                table: "Teacher",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_Teacher_teacher_id",
                table: "courses",
                column: "teacher_id",
                principalTable: "Teacher",
                principalColumn: "Id");
        }
    }
}
