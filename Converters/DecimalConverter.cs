using System;
using System.Globalization;
using System.Windows.Data;

namespace P16_StepFunctions.Converters
{
    /// <summary>
    /// Class for replacing comma by dot in input of decimal fields.
    /// </summary>
    public class DecimalConverter : IValueConverter
    {
        /// <summary>
        /// Replaces comma by dot in the actual value.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Replace(",", ".");
        }

        /// <summary>
        /// Replaces comma by dot in the actual value.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Replace(",", ".");
        }

    }
}
