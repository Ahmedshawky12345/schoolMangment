using AutoMapper;
using Microsoft.EntityFrameworkCore;
using schoolMangment.Data;
using schoolMangment.DTO;
using schoolMangment.Models;
using schoolMangment.Repository.Interfaces;
using System.Linq.Expressions;

namespace schoolMangment.Repository.RepoClass
{
    public class StudentRepository :IRepsitory<Student>,IStudent
    {
        private readonly AppDbContext context;
   

        public StudentRepository(AppDbContext context)
        {
            this.context = context;
            
        }

        public Task<Student> AddAsync(Student entity)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentCourse> AddCourseToStudent(StudentCourse studentCourse)
        {
            await context.studentCourses.AddAsync(studentCourse); // Ensure you're adding a new entity
           
            return studentCourse;
        }

        public async Task AddStudent(RegsterDTO regsterDTO, string UserId)
        {
            var student = new Student
            {
                Name = regsterDTO.StudentName,
                DateOfBirth = regsterDTO.DateOfBirth.Value,
                Gender = regsterDTO.Gender,
                UserId= UserId,
            };
            await context.students.AddAsync(student);
            await context.SaveChangesAsync();
            
           
        }

        public async Task DeleteAsync(Student entity)
        {
          
            context.students.Remove(entity);
           
        }

        public async Task<StudentCourse> FindAsync(Expression<Func<StudentCourse, bool>> predicate)
        {
            return await context.studentCourses.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var data = await context.students.Include(x=>x.user).ToListAsync();
            return data;

        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var data=await context.students.Include(x=>x.user).FirstOrDefaultAsync(x=>x.Id==id);
            return data;
        }

        public async Task<Student> GetStudentClass(int id)
        {
            var student = await context.students.Include(_class=>_class.Class).ThenInclude(x=>x.Students).FirstOrDefaultAsync(x => x.Id == id);
            return student;
        }

        public async Task< Student > GetStudentWithCoursesByIdAsync(int id)
        {
            var courses = await context.students.Include(x => x.studentCourses).ThenInclude(x => x.Course).
                FirstOrDefaultAsync(x => x.Id == id);
            return courses;
        }

        public async Task<Student> GetStudentWithExamsAsync(int id)
        {
            var data = await context.students.Include(x => x.studentExams).ThenInclude(x => x.exam).FirstOrDefaultAsync(key => key.Id == id);
            return data;
        }

        public async Task<StudentCourse> GradeForUser(string userid)
        {
            var data = await context.studentCourses.Include(x => x.student).Include(x => x.Course)
                .FirstOrDefaultAsync(x => x.student.UserId == userid);
            return data;
        }

        public async Task<Student> UpdateAsync(Student entity)
        {
             context.students.Update(entity);
            
            return entity;
        }
    }
}
