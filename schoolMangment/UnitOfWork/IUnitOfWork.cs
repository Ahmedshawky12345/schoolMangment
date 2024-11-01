using schoolMangment.Models;
using schoolMangment.Repository.Interfaces;

namespace schoolMangment.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {

        IRepsitory<Department> DepartmentRepository { get; }
        IRepsitory<Course> CourseRepository { get; }
        IRepsitory<Exam> ExamRepository { get; }
        IRepsitory<Class> ClassRepository { get; }
        IRepsitory<Student> StudentRepository { get; }
        IRepsitory<Teacher> TeacherRepository { get; }
        IDepartment DepartmentDetailRepository { get; }
        IStudent StudentDetailRepository { get; }
        ITeacher TeacherDetailRepository { get; }
        IClass ClassDetailRepository { get; }
        IExamStudent ExamStudentDetailRepository { get; }
       
        Task<int> CompleteAsync(); // to commit changes to the databas
    }
}