using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Data;

namespace Connect4.Resources.Converters
{
    public class StringToSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string imagePath)
            {
                // Create a BitmapImage from the string path
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                image.EndInit();
                return image;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ImageSource imageSource)
            {
                // Attempt to convert ImageSource back to string (example: absolute URI)
                if (imageSource is BitmapImage bitmapImage && bitmapImage.UriSource != null)
                {
                    return bitmapImage.UriSource.ToString();
                }
            }

            throw new NotSupportedException("Converting ImageSource back to string is not supported.");
        }
    }
}
