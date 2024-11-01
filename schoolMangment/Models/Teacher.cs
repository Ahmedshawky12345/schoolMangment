using System.ComponentModel.DataAnnotations;

namespace schoolMangment.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name requried")]
        [MaxLength(100, ErrorMessage = "max length 100 in Name")]
        [MinLength(3, ErrorMessage = "min length 3 Name")]
        public string Name { get; set; }
       

        // relationship with courses
        public ICollection<Course>? Courses { get; set; }
        // relationship between Teacher and user 
        public string? UserId { get; set; }
        public AppUser? user { get; set; }

        // relationship with Department 
        public int? DepartmentId { get; set; }
        public Department? department { get; set; }


    }
}
