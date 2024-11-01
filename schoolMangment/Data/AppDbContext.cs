using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using schoolMangment.Models;

namespace schoolMangment.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<Student> students { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<StudentCourse> studentCourses { get; set; }
        public DbSet<Teacher> teachers { get;set; }
        public DbSet<Exam> exams { get; set; }
        public DbSet <StudentExam>studentExams { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Class> classes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // relation many to many (student => course)
            modelBuilder.Entity<StudentCourse>().HasKey(key => new { key.CourseId, key.student_id });

            modelBuilder.Entity<StudentCourse>().HasOne(_student => _student.student).
                 WithMany(x => x.studentCourses).HasForeignKey(key => key.student_id).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentCourse>().HasOne(_course => _course.Course).
                 WithMany(x => x.studentCourses).HasForeignKey(key => key.CourseId).OnDelete(DeleteBehavior.NoAction);
//------------------------------------------------------------------------------------------

            // relationship one to many(Teacher and course)
            modelBuilder.Entity<Teacher>().HasMany(_course => _course.Courses).WithOne(_teacher => _teacher.teacher).
                HasForeignKey(key => key.teacher_id).OnDelete(DeleteBehavior.NoAction);
            //------------------------------------------------------------------------------------------
            // relation one to one between (user and student )
            modelBuilder.Entity<AppUser>().HasOne(_student => _student.student).WithOne(_user => _user.user).
                HasForeignKey<Student>(key => key.UserId).OnDelete(DeleteBehavior.NoAction);
            //------------------------------------------------------------------------------------------
            // relationship ont to on e between (user and teacher)
            modelBuilder.Entity<AppUser>().HasOne(_teacher => _teacher.Teacher).WithOne(_user => _user.user).
              HasForeignKey<Teacher>(key => key.UserId).OnDelete(DeleteBehavior.NoAction);
            //------------------------------------------------------------------------------------------
            // relation one to many between (Course and Exam)
            modelBuilder.Entity<Course>().HasMany(_exam => _exam.exams).WithOne(_course => _course.Course).
                HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.NoAction);
            //------------------------------------------------------------------------------------------
            // relation many to many with (exam and student)
            modelBuilder.Entity<StudentExam>().HasKey(key => new { key.studentId, key.examId });
            modelBuilder.Entity<StudentExam>().HasOne(_student => _student.student).WithMany(se => se.studentExams).
                HasForeignKey(key => key.studentId);
            modelBuilder.Entity<StudentExam>().HasOne(_exam => _exam.exam).WithMany(se => se.studentExams).
               HasForeignKey(key => key.examId);
            //------------------------------------------------------------------------------------------
            // relation one to many with Department and class 
            modelBuilder.Entity<Department>().HasMany(_class => _class.Classes).WithOne(_department => _department.Department).
                HasForeignKey(key => key.DepartmentId).OnDelete(DeleteBehavior.NoAction);
            //------------------------------------------------------------------------------------------

            // relation one to many with Department and student 
            modelBuilder.Entity<Department>().HasMany(_student => _student.Students).WithOne(_department => _department.department).
                HasForeignKey(key => key.DepartmentId).OnDelete(DeleteBehavior.NoAction);
            //------------------------------------------------------------------------------------------
            // relation one to many with Department and Course 
            modelBuilder.Entity<Department>().HasMany(_course => _course.Courses).WithOne(_department => _department.department).
                HasForeignKey(key => key.DepartmentId).OnDelete(DeleteBehavior.NoAction);
            //------------------------------------------------------------------------------------------
            // relation one to many with Department and Teacher 
            modelBuilder.Entity<Department>().HasMany(_teacher => _teacher.Teachers).WithOne(_department => _department.department).
                HasForeignKey(key => key.DepartmentId).OnDelete(DeleteBehavior.NoAction);


            //----------------------------------------------------------------------------------
            // relation one to many between student and class 

            modelBuilder.Entity<Class>().HasMany(_student => _student.Students).WithOne(_class => _class.Class).
              HasForeignKey(key => key.classid).OnDelete(DeleteBehavior.NoAction);



            base.OnModelCreating(modelBuilder);
        }
    }
}
