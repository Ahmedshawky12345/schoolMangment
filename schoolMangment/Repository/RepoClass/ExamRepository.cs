using Microsoft.EntityFrameworkCore;
using schoolMangment.Data;
using schoolMangment.DTO;
using schoolMangment.Models;
using schoolMangment.Repository.Interfaces;

namespace schoolMangment.Repository.RepoClass
{
    public class ExamRepository:IRepsitory<Exam>,IExamStudent
    {
        private readonly AppDbContext context;

        public ExamRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Exam> AddAsync(Exam entity)
        {
            await context.exams.AddAsync(entity);
            
            return entity;
        }

        public async Task<StudentExam> CheckEnrollmentAsync(int studentId, int examId)
        {
            var data = await context.studentExams.FirstOrDefaultAsync(x => x.studentId == studentId && x.examId == examId);
            return data;

        }

        public async Task DeleteAsync(Exam entity)
        {
            context.exams.Remove(entity);
           

            
        }

        public async Task EnrollStudentInExamAsync(StudentExam studentExam)
        {
            await context.studentExams.AddAsync(studentExam);
            
        }

        public async Task<IEnumerable<Exam>> GetAllAsync()
        {
            var exams = await context.exams.ToListAsync();
            return exams;

        }

        public async Task<Exam> GetByIdAsync(int id)
        {
            var exam = await context.exams.FirstOrDefaultAsync(exam => exam.Id == id);
            return exam;
        }

        

        public async Task RemoveStudentFromExamAsync(int studentId, int examId)
        {
            var data = await context.studentExams.FirstOrDefaultAsync(x => x.examId == examId && x.studentId == studentId);
            
                context.studentExams.Remove(data);
               

            
        }

        public async Task<Exam> UpdateAsync(Exam entity)
        {
            context.exams.Update(entity);
           
            return entity;
        }
    }
}
