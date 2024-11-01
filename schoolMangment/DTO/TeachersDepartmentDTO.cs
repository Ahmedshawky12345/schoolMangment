namespace schoolMangment.DTO
{
    public class TeachersDepartmentDTO
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<TeacherDetilesDTO> Teachers { get; set; }
      
    }
    public class TeahcerData
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
}
