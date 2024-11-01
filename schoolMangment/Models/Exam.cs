using System.ComponentModel.DataAnnotations;

namespace schoolMangment.Models
{
    public class Exam
    {
        public int Id { get; set; } // Unique identifier for the exam
     
        public string Title { get; set; }

  

        public DateTime ExamDate { get; set; } // Date of the exam
        
        public int Duration { get; set; } // Duration of the exam in minutes
        

        


        // relationshipwith course
        public int? CourseId { get; set; }
        public Course? Course { get; set; }
        // relationshiop with StudentExam
        public ICollection<StudentExam>? studentExams { get; set; }


    }
}
