using schoolMangment.DTO;
using schoolMangment.Models;

namespace schoolMangment.Repository.Interfaces
{
    public interface IExamStudent
    {
        
        Task EnrollStudentInExamAsync(StudentExam studentExam);
        Task<StudentExam> CheckEnrollmentAsync(int studentId, int examId);
        Task RemoveStudentFromExamAsync(int studentId, int examId);
    }
}
