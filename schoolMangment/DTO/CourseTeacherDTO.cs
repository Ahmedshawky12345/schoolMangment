using System.ComponentModel.DataAnnotations;

namespace schoolMangment.DTO
{
    public class CourseTeacherDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "name requried")]
        [MaxLength(100, ErrorMessage = "max length 100")]
        [MinLength(3, ErrorMessage = "min length 3")]

        public string TeacherName { get; set; }
        [Required(ErrorMessage = "Email requried")]
        [EmailAddress]
        public string TeacherEmail { get; set; }

        public List<string> CourseName { get; set; }
    }
}
