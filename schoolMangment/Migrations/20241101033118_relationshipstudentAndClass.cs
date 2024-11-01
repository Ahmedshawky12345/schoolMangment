using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolMangment.Migrations
{
    /// <inheritdoc />
    public partial class relationshipstudentAndClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_classes_ClassId",
                table: "students");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "students",
                newName: "classid");

            migrationBuilder.RenameIndex(
                name: "IX_students_ClassId",
                table: "students",
                newName: "IX_students_classid");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classes_classid",
                table: "students",
                column: "classid",
                principalTable: "classes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_classes_classid",
                table: "students");

            migrationBuilder.RenameColumn(
                name: "classid",
                table: "students",
                newName: "ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_students_classid",
                table: "students",
                newName: "IX_students_ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classes_ClassId",
                table: "students",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id");
        }
    }
}
