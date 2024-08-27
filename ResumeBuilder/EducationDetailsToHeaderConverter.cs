using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace ResumeBuilder
{
    public class EducationDetailsToHeaderConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 4)
                return "New Education Details";
            string degree = values[0] as string;
            string institution = values[1] as string;
            string city = values[2] as string;
            string graduationDate = values[3] as string;

            bool hasMainInfo = !string.IsNullOrWhiteSpace(degree) || !string.IsNullOrWhiteSpace(institution) || !string.IsNullOrWhiteSpace(city);
            bool hasDate = !string.IsNullOrWhiteSpace(graduationDate);

            if (!hasMainInfo && !hasDate)
            {
                return new
                {
                    MainText = "New Education Details",
                    DateText = "Graduate date: "
                };
            }

            string mainText = $"{degree} at {institution} in {city}".Trim(new char[] { ',', ' ' });
            return new
            {
                MainText = string.IsNullOrWhiteSpace(mainText) ? "New Education Details" : mainText,
                DateText = hasDate ? $"Graduation date: {graduationDate}" : string.Empty
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

