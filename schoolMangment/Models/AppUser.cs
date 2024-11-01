using Microsoft.AspNetCore.Identity;

namespace schoolMangment.Models
{
    public class AppUser:IdentityUser
    {
        // for student 
        
        public Student? student { get; set; }
        // for Teacher
        
        public Teacher? Teacher { get; set; }

    }
}
