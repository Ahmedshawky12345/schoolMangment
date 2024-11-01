using System.ComponentModel.DataAnnotations;

namespace schoolMangment.Models
{
    public class Course
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
    
        public string Description { get; set; }


        // relation with student course
        public ICollection<StudentCourse>? studentCourses { get; set; }

        // relationship with Teacher

        public int? teacher_id { get; set; }
        public Teacher? teacher { get; set; }

        // relationship with Exam
     public  ICollection<Exam>? exams { get; set; }
        // relationship with Department 
        public int? DepartmentId { get; set; }
        public Department? department { get; set; }

    }
}
