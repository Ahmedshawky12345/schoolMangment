using Microsoft.EntityFrameworkCore;
using schoolMangment.Data;
using schoolMangment.Models;
using schoolMangment.Repository.Interfaces;
using schoolMangment.Repository.RepoClass;
using System.Reflection.Metadata.Ecma335;

namespace schoolMangment.UnitOfWork.BaseClass
{
    public class Unit:IUnitOfWork
    {
        private readonly AppDbContext context;

        public Unit(AppDbContext context)
        {
            this.context = context;
            DepartmentRepository = new DepartmentReository(context);
            StudentRepository = new StudentRepository(context);
            ExamRepository=new ExamRepository(context);
            CourseRepository = new CourseRepostiory(context);
            TeacherRepository = new TeacherRepository(context);
            ClassRepository = new ClassRepository(context);

            //StudentDetailRepository = (StudentRepository)StudentRepository;
            //TeacherDetailRepository=

            DepartmentDetailRepository = new DepartmentReository(context);
            StudentDetailRepository = new StudentRepository(context);
            TeacherDetailRepository = new TeacherRepository(context);
            ClassDetailRepository = new ClassRepository(context);
            ExamStudentDetailRepository = new ExamRepository(context);







        }

        public IRepsitory<Department> DepartmentRepository { get; private set; }

        public IDepartment DepartmentDetailRepository { get; private set; }

        public IStudent StudentRepsitory { get; private set; }

        public ITeacher TeacherRepsitory { get; private set; }

        public IClass ClassRepsitory { get; private set; }
        public IExamStudent ExamStudentRepsitory { get; private set; }

        public IRepsitory<Course> CourseRepository { get; private set; }

        public IRepsitory<Exam> ExamRepository { get; private set; }

        public IRepsitory<Class> ClassRepository { get; private set; }

        public IRepsitory<Student> StudentRepository { get; private set; }

        public IRepsitory<Teacher> TeacherRepository { get; private set; }

        public IStudent StudentDetailRepository { get; private set; }

        public ITeacher TeacherDetailRepository { get; private set; }

        public IClass ClassDetailRepository { get; private set; }

        public IExamStudent ExamStudentDetailRepository { get; private set; }

        public async Task<int> CompleteAsync()
        {
        return    await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
