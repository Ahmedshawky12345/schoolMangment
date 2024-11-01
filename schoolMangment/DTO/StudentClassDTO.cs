namespace schoolMangment.DTO
{
    public class StudentClassDTO
    {
        public string ClassName { get; set; }
        public int ClassId { get; set; }
        public List<StudentsDataDTO> Students { get; set; }
    }

    public class StudentsDataDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
