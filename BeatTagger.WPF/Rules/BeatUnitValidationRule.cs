using BeatTagger.WPF.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BeatTagger.WPF.Rules
{
    public sealed class BeatUnitValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string? timeSignature = value.ToString();
            if (timeSignature is null)
            {
                return new ValidationResult(false, "ビート単位が変換できませんでした");
            }

            string beatUnitString = timeSignature.Split("/").Last();
            if (!int.TryParse(beatUnitString, out int beatUnit))
            {
                return new ValidationResult(false, "ビート単位が変換できませんでした");
            }

            try
            {
                _ = new BeatUnit(beatUnit);
                return ValidationResult.ValidResult;
            }
            catch (ArgumentException ex)
            {
                return new ValidationResult(false, "ビート単位が変換できませんでした");
            }
        }
    }
}