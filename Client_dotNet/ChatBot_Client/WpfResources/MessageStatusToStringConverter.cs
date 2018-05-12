using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChatBot_Client.WpfResources
{
    public class MessageStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MessageStatus status = (MessageStatus?) value ?? MessageStatus.Error;

            return Enum.GetName(typeof(MessageStatus), status);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MessageStatus message;
            if (!Enum.TryParse((string) value, out message))
                message = MessageStatus.Error;

            return message;
        }
    }
}
