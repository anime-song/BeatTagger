using BeatTagger.WPF.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BeatTagger.WPF.Converters
{
    public sealed class TimeSignatureToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSignature timeSignature)
            {
                return $"{timeSignature.BeatsPerMeasure}/{timeSignature.BeatUnit.Value}";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string timeSignatureStr)
            {
                var parts = timeSignatureStr.Split('/');
                if (parts.Length == 2 && int.TryParse(parts[0], out int beatsPerMeasure) && int.TryParse(parts[1], out int beatUnit))
                {
                    return new TimeSignature(beatsPerMeasure, new BeatUnit(beatUnit));
                }
            }
            throw new ArgumentException("Invalid TimeSignature string format");
        }
    }
}