using schoolMangment.DTO;
using schoolMangment.Models;

namespace schoolMangment.Repository.Interfaces
{
    public interface ITeacher
    {
        Task<IEnumerable<Teacher>> GetTeachersAndCourses();
        Task AddTeacher(RegsterDTO regsterDTO,string UserId);

    }
}
