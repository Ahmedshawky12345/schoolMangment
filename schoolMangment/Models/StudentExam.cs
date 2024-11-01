namespace schoolMangment.Models
{
    public class StudentExam
    {
      
        // relationship with Exam
        public int? examId { get; set; }
        public Exam? exam { get; set; }

        // relationshiop with student 
        public int? studentId { get; set; }
        public Student? student { get; set; }


        public decimal? Grade { get; set; } // Grade for the student in this exam
    }
}
