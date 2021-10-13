// Group2: Jingfei Yao, Grace Pauly, Xiaotong Han.
using System.Globalization;
using System.Windows.Controls;

namespace PositiveIntegerValidationRule
{
    class PositiveIntegerValidationRule : ValidationRule
    {
        public string ValidationType { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}
