using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class CourseCodeAttribute : ValidationAttribute
{
    private readonly string _prefix;

    // Accept department prefix as parameter
    // Usage: [CourseCode("CS")]  or  [CourseCode("EEE")]
    public CourseCodeAttribute(string prefix)
    {
        _prefix = prefix.ToUpper();
    }

    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        if (value != null)
        {
            string code = value.ToString().ToUpper();

            // Must match: CS-101 or EEE-202
            string pattern = $@"^{_prefix}-\d{{3}}$";
            bool isValid = Regex.IsMatch(code, pattern);

            if (!isValid)
            {
                // ❌ FAIL
                return new ValidationResult($"Course code must be like {_prefix}-101");
            }
        }

        // ✅ PASS
        return ValidationResult.Success;
    }
}