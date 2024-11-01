using schoolMangment.Models;

namespace schoolMangment.Repository.Interfaces
{
    public interface IClass
    {
        Task<Class> ClassDetitles(int id);
        Task<Class> StudentClass(int id);
     
    }
}
