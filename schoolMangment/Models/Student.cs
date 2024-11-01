namespace schoolMangment.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        // relation with student course
        public ICollection<StudentCourse>? studentCourses { get;set; }

        // relationship between student and user 
        public string? UserId { get; set; }
        public AppUser? user { get; set; }

        // relationshiop with StudentExam
        public ICollection<StudentExam>? studentExams { get; set; }

        // relationship with Department 
       public int? DepartmentId { get; set; }
        public Department? department { get; set; }

        public Class? Class { get; set; }
        public int? classid { get; set; }




    }
}
