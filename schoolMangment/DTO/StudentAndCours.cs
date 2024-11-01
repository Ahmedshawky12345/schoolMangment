using System.ComponentModel.DataAnnotations;

namespace schoolMangment.DTO
{
    public class StudentAndCours
    {
        public int id { get; set; }
        [Required(ErrorMessage = "name requried")]
        [MaxLength(100, ErrorMessage = "max length 100")]
        [MinLength(3, ErrorMessage = "min length 3")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email requried")]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public ICollection<string> coursename { get; set; }
    }
}
