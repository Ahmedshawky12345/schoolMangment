using schoolMangment.CustomDataAnotaion;
using System.ComponentModel.DataAnnotations;

namespace schoolMangment.DTO
{
    public class ExamDTO
    {
        public int Id { get; set; } // Unique identifier for the exam

        [OptionalMinLength(2, ErrorMessage = "Title must be at least 2 characters long")]
        [OptionalMaxLength(50, ErrorMessage = "Title must be less than 50 characters long")]
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExamDate { get; set; } // Date of the exam
        [Range(10, 180, ErrorMessage = "Duration must be between 10 and 180 minutes.")]
        public int? Duration { get; set; }
    }
 public class   ExamDetilesDTO{

        [Required(ErrorMessage = "Title is required")]
        [MinLength(2, ErrorMessage = "Title must be at least 6 characters long")]
        [MaxLength(50, ErrorMessage = "Title must be less than 50 characters long")]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExamDate { get; set; } // Date of the exam
        [Range(10, 180, ErrorMessage = "Duration must be between 10 and 180 minutes.")]
        public int Duration { get; set; }
    }
}
