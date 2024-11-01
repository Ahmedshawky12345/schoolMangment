using System.ComponentModel.DataAnnotations;

namespace schoolMangment.DTO
{
    public class ExamStudentDTO
    {
        public int Student_id { get; set; }
        public int Examp_id { get; set; }

        public decimal? Grade { get; set; }
    }
    public class ExamStudentDetitles
    {
        public int id { get; set; }
        [Required(ErrorMessage = "name requried")]
        [MaxLength(100, ErrorMessage = "max length 100")]
        [MinLength(3, ErrorMessage = "min length 3")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email requried")]
        [EmailAddress]
       

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public List<string> ExamTitle { get; set; }



        
    }
    public class RemoveStudentExamDTO
    {
        public int Student_id { get; set; }
        public int Examp_id { get; set; }
    }
}
