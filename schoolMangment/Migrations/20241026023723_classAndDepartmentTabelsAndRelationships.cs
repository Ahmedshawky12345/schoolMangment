using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolMangment.Migrations
{
    /// <inheritdoc />
    public partial class classAndDepartmentTabelsAndRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "teachers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_classes_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_teachers_DepartmentId",
                table: "teachers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_students_ClassId",
                table: "students",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_students_DepartmentId",
                table: "students",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_courses_DepartmentId",
                table: "courses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_classes_DepartmentId",
                table: "classes",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_departments_DepartmentId",
                table: "courses",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classes_ClassId",
                table: "students",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_students_departments_DepartmentId",
                table: "students",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_departments_DepartmentId",
                table: "teachers",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_departments_DepartmentId",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "FK_students_classes_ClassId",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_students_departments_DepartmentId",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_teachers_departments_DepartmentId",
                table: "teachers");

            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropIndex(
                name: "IX_teachers_DepartmentId",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_students_ClassId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_DepartmentId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_courses_DepartmentId",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "courses");
        }
    }
}
