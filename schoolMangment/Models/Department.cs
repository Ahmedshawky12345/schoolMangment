using System.ComponentModel.DataAnnotations;

namespace schoolMangment.Models
{
    public class Department
    {
        public int Id { get; set; }
    
        public string Name { get; set; }
        public ICollection<Teacher>? Teachers { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public ICollection<Class>? Classes { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
