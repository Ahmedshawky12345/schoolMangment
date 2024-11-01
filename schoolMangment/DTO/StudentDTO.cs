using schoolMangment.CustomDataAnotaion;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace schoolMangment.DTO
{
 

    public class StudentDTO
    {
        public int id { get; set; }
      
        [OptionalMaxLength(100, ErrorMessage = "max length 100")]
        [OptionalMinLength(3, ErrorMessage = "min length 3")]
        public string? Name { get; set; }
       
        [EmailAddress]
        public string? Email { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0-yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [RegularExpression("Male|Female", ErrorMessage = "Gender must be either Male or Female")]
        public string? Gender { get; set; }
        public string user_id { get; set; }
        //public int class_id { get; set; }
    }
}
