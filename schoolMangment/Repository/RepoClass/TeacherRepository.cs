using Microsoft.EntityFrameworkCore;
using schoolMangment.Data;
using schoolMangment.DTO;
using schoolMangment.Models;
using schoolMangment.Repository.Interfaces;

namespace schoolMangment.Repository.RepoClass
{
    public class TeacherRepository : IRepsitory<Teacher>,ITeacher
    {
        private readonly AppDbContext context;

        public TeacherRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Teacher> AddAsync(Teacher entity)
        {
            await context.teachers.AddAsync(entity);
            
            return entity;

        }
        
        public async Task DeleteAsync(Teacher entity)
        {
            context.teachers.Remove(entity);
           
        }

        public async Task<IEnumerable<Teacher>> GetTeachersAndCourses()
        {
            var data = await context.teachers.Include(x => x.Courses).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            var data = await context.teachers.ToListAsync();
            return data;
        }

        public async Task<Teacher> GetByIdAsync(int id)
        {
            var data = await context.teachers.FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task<Teacher> UpdateAsync(Teacher entity)
        {
           context.teachers.Update(entity);
            
            return entity;
        }

        public async Task AddTeacher(RegsterDTO regsterDTO, string UserId)
        {
            var Teacher = new Teacher
            {
                Name = regsterDTO.TeacherName,
                UserId = UserId,
            };
            await context.teachers.AddAsync(Teacher);
            
        }
    }
}
