using System.ComponentModel.DataAnnotations;

namespace schoolMangment.CustomDataAnotaion
{
    public class OptionalMinLengthAttribute : ValidationAttribute
    {
        private readonly int _minLength;

        public OptionalMinLengthAttribute(int minLength)
        {
            _minLength = minLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // If value is null, skip validation (as it's optional)
                return ValidationResult.Success;
            }

            // Check if the length meets the minimum requirement
            if (value is string str && str.Length < _minLength)
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must be at least {_minLength} characters long.");
            }

            return ValidationResult.Success;
        }
    }
}
