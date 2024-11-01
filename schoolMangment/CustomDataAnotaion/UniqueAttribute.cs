using schoolMangment.Data;
using System.ComponentModel.DataAnnotations;

namespace schoolMangment.CustomDataAnotaion
{
    public class UniqueAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        
            {
                var new_value = value.ToString();
                if (new_value != null)
                {
                    var context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
                    var exist = context.departments.Any(x => x.Name == new_value);
                    if (exist)
                    {
                        return new ValidationResult("the name must be unique");
                    }



                }
                return ValidationResult.Success;

           
        }
    }
}
