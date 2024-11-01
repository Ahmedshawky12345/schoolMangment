using System.ComponentModel.DataAnnotations;

namespace schoolMangment.DTO
{
    public class TeacherDTO
    {
        public int Id { get; set; }
       
        [MaxLength(100, ErrorMessage = "max length 100")]
        [MinLength(3, ErrorMessage = "min length 3")]
        public string? Name { get; set; }
        //[Required(ErrorMessage = "Email requried")]
        //[EmailAddress]
        //public string Email { get; set; }

    }
    public class TeacherDetilesDTO
    {
        [Required(ErrorMessage = "name requried")]
        [MaxLength(100, ErrorMessage = "max length 100")]
        [MinLength(3, ErrorMessage = "min length 3")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email requried")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
