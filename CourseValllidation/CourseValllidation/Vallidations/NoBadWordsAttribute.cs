using System.ComponentModel.DataAnnotations;

public class NoBadWordsAttribute : ValidationAttribute
{
    // list of banned words
    private readonly string[] _banned = { "spam", "fake", "test", "dummy" };

    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        if (value != null)
        {
            string input = value.ToString().ToLower();

            foreach (var word in _banned)
            {
                if (input.Contains(word))
                {
                    // ❌ FAIL — bad word found
                    return new ValidationResult($"'{word}' is not allowed in {context.DisplayName}");
                }
            }
        }

        // ✅ PASS — no bad words
        return ValidationResult.Success;
    }
}