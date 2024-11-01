using schoolMangment.CustomDataAnotaion;
using System.ComponentModel.DataAnnotations;

namespace schoolMangment.DTO
{
    public class ClassDTO
    {
        public int Id { get; set; }
        [OptionalMinLength(3, ErrorMessage = "Name must be at least 6 characters long")]
        [OptionalMaxLength(50, ErrorMessage = "Name must be less than 50 characters long")]
        public string Name { get; set; }

        public int DepatmentId { get; set; }
    }
    public class ClassAddDTO
    {
        
    
        public string Name { get; set; }
        public int DepatmentId { get; set; }

    }
}
