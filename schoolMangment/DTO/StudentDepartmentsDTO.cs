namespace schoolMangment.DTO
{
    public class StudentDepartmentsDTO
    {
        
            public int DepartmentId { get; set; }
            public string DepartmentName { get; set; }
            public List<StudentData> students { get; set; }

        
        
    }
    public class StudentData
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
    }
}
