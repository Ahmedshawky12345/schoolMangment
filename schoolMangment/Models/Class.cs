using System.ComponentModel.DataAnnotations;

namespace schoolMangment.Models
{
    public class Class
    {
        public int Id { get; set; }
     
        public string Name { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
