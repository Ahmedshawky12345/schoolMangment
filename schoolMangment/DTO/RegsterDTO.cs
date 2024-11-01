using System.ComponentModel.DataAnnotations;

namespace schoolMangment.DTO
{
    public class RegsterDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ComfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        // Only for students
        [MinLength(3, ErrorMessage = "Student name must be at least 3 characters long")]
        [MaxLength(50, ErrorMessage = "Student name must be at most 50 characters long")]
        public string? StudentName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        // Only for teachers
        [MinLength(3, ErrorMessage = "Teacher name must be at least 3 characters long")]
        [MaxLength(50, ErrorMessage = "Teacher name must be at most 50 characters long")]
        public string? TeacherName { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression("Male|Female", ErrorMessage = "Gender must be either Male or Female")]
      
         public int StudentDepartmentId { get;set; }
        public int TeacherDepartmentId { get; set; }


        public string? Gender { get; set; }
        public int classId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Role == "Student")
            {
                if (string.IsNullOrEmpty(StudentName))
                {
                    yield return new ValidationResult("StudentName is required for students", new[] { nameof(StudentName) });
                }

                if (!DateOfBirth.HasValue)
                {
                    yield return new ValidationResult("DateOfBirth is required for students", new[] { nameof(DateOfBirth) });
                }
            }
            else if (Role == "Teacher")
            {
                if (string.IsNullOrEmpty(TeacherName))
                {
                    yield return new ValidationResult("TeacherName is required for teachers", new[] { nameof(TeacherName) });
                }
            }
        }

    }

}
