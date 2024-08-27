
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Math;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ResumeBuilder
{
    public class ExperienceDetailsToHeaderConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 5)
                return "New Experience Details";
            string role = values[0] as string;
            string company = values[1] as string;
            string city = values[2] as string;
            string startdate = values[3] as string;
            string enddate = values[4] as string;

            bool hasMainInfo = !string.IsNullOrWhiteSpace(role) || !string.IsNullOrWhiteSpace(company) || !string.IsNullOrWhiteSpace(city);
            bool hasDate = !string.IsNullOrWhiteSpace(startdate) || !string.IsNullOrWhiteSpace(enddate);
            if (!hasMainInfo && !hasDate)
            {
                return new
                {
                    MainText = "New Experience Details",
                    DateText = "Start date - End date"
                };
            }
            string mainText = $"{role} at {company} in {city}".Trim(new char[] { ',',' ' });
            return new
            {
                MainText = string.IsNullOrWhiteSpace(mainText) ? "New Education Details" : mainText,
                DateText = hasDate ? $"Start: {startdate} - End: {enddate}" : string.Empty,
            };

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

