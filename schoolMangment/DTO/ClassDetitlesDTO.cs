using schoolMangment.Models;

namespace schoolMangment.DTO
{
    public class ClassDetitlesDTO
    {
        public int id { get; set; }
        public string className { get; set; }

        public List<DepartmentDTO> department_Detitles { get; set; }
        public List<StudentDTO> Student_Detiles { get; set; }

        
    }
}
