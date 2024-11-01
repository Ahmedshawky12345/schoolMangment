namespace schoolMangment.DTO
{
    public class ClassDepartmentDTO
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<ClassData> Classes  { get; set; }


        
    }
    public class ClassData
    {
        public int ClassId { get; set; }

        public string ClassName { get; set; }
    }
}
