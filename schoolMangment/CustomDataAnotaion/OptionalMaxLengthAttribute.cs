using System.ComponentModel.DataAnnotations;

namespace schoolMangment.CustomDataAnotaion
{
    public class OptionalMaxLengthAttribute : ValidationAttribute
    {
        private readonly int maxLength;

        public OptionalMaxLengthAttribute(int maxLength)
        {
            this.maxLength = maxLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // If value is null, skip validation (as it's optional)
                return ValidationResult.Success;
            }

            // Check if the length exceeds the maximum requirement
            if (value is string str && str.Length > maxLength)
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must be at most {maxLength} characters long.");
            }

            return ValidationResult.Success;
        }
    }
}
