using schoolMangment.Models;
using System.Linq.Expressions;

namespace schoolMangment.Repository.Interfaces
{
    public interface IDepartment:IRepsitory<Department>
    {
       Task<Department> GetDepartmentStudent(int id);
        Task<Department> GetDepartmentClass(int id);
        Task<Department> GetDepartmentTeacher(int id);
       


    }
}
