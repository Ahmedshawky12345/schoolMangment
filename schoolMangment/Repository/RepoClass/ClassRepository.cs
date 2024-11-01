using Microsoft.EntityFrameworkCore;
using schoolMangment.Data;
using schoolMangment.Models;
using schoolMangment.Repository.Interfaces;

namespace schoolMangment.Repository.RepoClass
{
    public class ClassRepository : IRepsitory<Class>,IClass
    {
        private readonly AppDbContext context;

        public ClassRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Class> AddAsync(Class entity)
        {
            await context.classes.AddAsync(entity);
            
            return entity;
        }

        public async Task<Class> ClassDetitles(int id)
        {
            var data = await context.classes.Include(x => x.Department).ThenInclude(x => x.Students)
                .FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task DeleteAsync(Class entity)
        {
            context.classes.Remove(entity);
           
        }

        public async Task<IEnumerable<Class>> GetAllAsync()
        {
            var Classes = await context.classes.ToListAsync();
            return Classes;
        }

        public async Task<Class> GetByIdAsync(int id)
        {
            var _class = await context.classes.FirstOrDefaultAsync(x=>x.Id==id);
            return _class;
        }

        public async Task<Class> GetStudentClass(int id)
        {
            var students = await context.classes.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == id);
            return students;
        }

        public async Task<Class> StudentClass(int id)
        {
            var data = await context.classes.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task<Class> UpdateAsync(Class entity)
        {
            context.classes.Update(entity);
           
            return entity;
        }
    }
}
