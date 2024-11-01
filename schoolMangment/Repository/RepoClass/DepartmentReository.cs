using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolMangment.Data;
using schoolMangment.Models;
using schoolMangment.Repository.Interfaces;
using System.Linq.Expressions;

namespace schoolMangment.Repository.RepoClass
{
    public class DepartmentReository : IDepartment, IRepsitory<Department>
    {
        private readonly AppDbContext context;

        public DepartmentReository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Department> AddAsync(Department entity)
        {
            await context.departments.AddAsync(entity);
           
            return entity;
        }

        public async Task DeleteAsync(Department entity)
        {
            context.departments.Remove(entity);
            

        }


        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var Departments = await context.departments.ToListAsync();
            return Departments;
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var department = await context.departments.FirstOrDefaultAsync(x=>x.Id==id);
            return department;
        }

        public async Task<Department> GetDepartmentClass(int id)
        {
            var data = await context.departments.Include(x => x.Classes).FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task<Department> GetDepartmentStudent(int id)
        {
            var data = await context.departments.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task<Department> GetDepartmentTeacher(int  id)
        {
            var data = await context.departments.Include(x => x.Teachers).FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task<Department> UpdateAsync(Department entity)
        {
            context.departments.Update(entity);
          
            return entity;
        }
    }
}
