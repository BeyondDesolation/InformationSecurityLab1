using System;
using System.Globalization;
using System.Windows.Data;

namespace D.IBlab1.Infrastructure.Converters
{
    internal class ObjectsToArrayConverter : IMultiValueConverter
    {
        public object Convert(object[] v, Type tp, object p, CultureInfo c)
        {
            return v.Clone();
        }

        public object[] ConvertBack(object v, Type[] tp, object p, CultureInfo c)
        {
            throw new NotImplementedException();
        }
    }
}
