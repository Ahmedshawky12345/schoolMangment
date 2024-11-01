using schoolMangment.CustomDataAnotaion;
using System.ComponentModel.DataAnnotations;

namespace schoolMangment.DTO
{
    public class DepartmentDTO
    {
        public int id { get; set; }
        
        [OptionalMinLength(3, ErrorMessage = "Title must be at least 6 characters long")]
        [OptionalMaxLength(50, ErrorMessage = "Title must be less than 50 characters long")]
        
        public string name { get; set; }
    }
    public class DepartmentAddDTO
    {
        [Unique]
        [Required(ErrorMessage = "Title is required")]
        [MinLength(3, ErrorMessage = "Title must be at least 6 characters long")]
        [MaxLength(50, ErrorMessage = "Title must be less than 50 characters long")]
        public string name { get; set; }
       
        
    }
}
