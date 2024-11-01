using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace schoolMangment.Models
{
    public class StudentCourse
    {
      

        public decimal? Grade { get; set; }
        // relationship with course
        public int? CourseId { get; set; }
        public Course? Course { get; set; }
        // relationship with studeent
        public int? student_id { get;set; }
        public Student? student { get; set; }
    }
}
