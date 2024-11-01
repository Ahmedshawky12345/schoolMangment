using Microsoft.EntityFrameworkCore;
using schoolMangment.Data;
using schoolMangment.Models;
using schoolMangment.Repository.Interfaces;

namespace schoolMangment.Repository.RepoClass
{
    public class CourseRepostiory : IRepsitory<Course>
    {
        private readonly AppDbContext context;

        public CourseRepostiory(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Course> AddAsync(Course entity)
        {
            await context.courses.AddAsync(entity);
           
            return entity;
        }

        public async Task DeleteAsync(Course entity)
        {
            context.courses.Remove(entity);
          
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var courses = await context.courses.ToListAsync();
            return courses;
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            var course= await context.courses.SingleOrDefaultAsync(Course=>Course.Id==id);
            return course;
        }

        public async Task<Course> UpdateAsync(Course entity)
        {
             context.courses.Update(entity);
            
            return entity;

        }
    }
}
