using System.ComponentModel.DataAnnotations;

namespace schoolMangment.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email  requried")]
       

        public string username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "passwored  requried")]
        public string Password { get; set; }
    }
}
