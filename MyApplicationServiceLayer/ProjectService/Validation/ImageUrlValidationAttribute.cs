using System.ComponentModel.DataAnnotations;

namespace MyApplicationServiceLayer.ProjectService.Validation
{
    public class ImageUrlValidationAttribute : ValidationAttribute
    {
        private static readonly string[] AllowedExtensions = { ".jpeg", ".jpg", ".png", ".gif", ".webp" };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string imageUrl)
            {
                var extension = Path.GetExtension(imageUrl).ToLower();
                if (!AllowedExtensions.Contains(extension))
                {
                    return new ValidationResult("Wrong image url extension");
                }
            }
            return ValidationResult.Success;
        }

    }
}
