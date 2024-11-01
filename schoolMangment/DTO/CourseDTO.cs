using schoolMangment.CustomDataAnotaion;
using System.ComponentModel.DataAnnotations;

namespace schoolMangment.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }

        [OptionalMinLength(3, ErrorMessage = "CourseName must have a minimum length of 3 if provided.")]
        public string? Name { get; set; }

        [OptionalMinLength(3, ErrorMessage = "CourseName must have a minimum length of 3 if provided.")]
        public string? Description { get; set; }
        public int? teacher_id { get; set; }
    }
    public class CourseDetilesDTO
    {
        [Required(ErrorMessage = "Course_Name is requried")]
        [MaxLength(100, ErrorMessage = "  max length for CourseName is  100 length")]
        [MinLength(3, ErrorMessage = "CourseName must has min length 3")]
        public string Name { get; set; }
       
        [Required(ErrorMessage = "Description must be requried")]
        [MinLength(10, ErrorMessage = "Description must has min length 3")]
        public string Description { get; set; }
        public int teacher_id { get;set; }
    }
}
