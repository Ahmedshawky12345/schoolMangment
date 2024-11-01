using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolMangment.Migrations
{
    /// <inheritdoc />
    public partial class cascateOndeleteBehavoiderStudentAndStudentCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_students_student_id",
                table: "studentCourses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "teachers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_studentCourses_students_student_id",
                table: "studentCourses",
                column: "student_id",
                principalTable: "students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_students_student_id",
                table: "studentCourses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "teachers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_studentCourses_students_student_id",
                table: "studentCourses",
                column: "student_id",
                principalTable: "students",
                principalColumn: "Id");
        }
    }
}
