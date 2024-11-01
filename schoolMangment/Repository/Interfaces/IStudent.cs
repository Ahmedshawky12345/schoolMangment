using schoolMangment.DTO;
using schoolMangment.Models;
using System.Linq.Expressions;

namespace schoolMangment.Repository.Interfaces
{
    public interface IStudent
    {
        Task AddStudent(RegsterDTO regsterDTO, string UserId);
        Task<StudentCourse> AddCourseToStudent(StudentCourse _studentcourse);
        Task< Student> GetStudentWithCoursesByIdAsync(int id);
        Task<StudentCourse> FindAsync(Expression<Func<StudentCourse, bool>> predicate);
        Task<Student> GetStudentWithExamsAsync(int studentId);

        Task<StudentCourse> GradeForUser(string userid);

        Task<Student> GetStudentClass(int id);


    }
}
