using System.Globalization;
using System.Windows.Controls;

namespace BeatKeeper.ValidationRules
{
    public class ShortValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool canConvert = short.TryParse(value as string, out _);
            return new ValidationResult(canConvert, "Not a valid short.");
        }
    }
}
