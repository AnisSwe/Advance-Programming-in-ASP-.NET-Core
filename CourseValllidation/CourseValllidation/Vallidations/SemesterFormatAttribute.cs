using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class SemesterFormatAttribute : ValidationAttribute
{
    // Must match: Fall-2024 or Spring-2025 or Summer-2024
    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        if (value != null)
        {
            string semester = value.ToString();

            // Regex pattern: Fall/Spring/Summer - 4 digit year
            bool isValid = Regex.IsMatch(semester, @"^(Fall|Spring|Summer)-\d{4}$");

            if (!isValid)
            {
                // ❌ FAIL
                return new ValidationResult("Semester must be like Fall-2024 or Spring-2025");
            }
        }

        // ✅ PASS
        return ValidationResult.Success;
    }
}