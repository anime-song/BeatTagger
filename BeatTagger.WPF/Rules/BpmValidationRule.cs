using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BeatTagger.WPF.Rules
{
    public class BpmValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (double.TryParse(value.ToString(), out double bpm))
            {
                if (IsValidBpm(bpm))
                {
                    return ValidationResult.ValidResult;
                }
            }
            return new ValidationResult(false, "BPMは1以上を入力してください");
        }

        private bool IsValidBpm(double bpm)
        {
            return bpm >= 1;
        }
    }
}