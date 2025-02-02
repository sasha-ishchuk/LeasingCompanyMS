using System.Globalization;
using System.Windows.Data;
using LeasingCompanyMS.Model;

namespace LeasingCompanyMS.Converters;

[ValueConversion(typeof(Car), typeof(string))]
public class CarToStringConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var car = (Car?)value;
        if (car == null) {
            return "";
        }
        
        return $"{car.Brand} {car.Model} ({car.ProductionYear})";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new InvalidOperationException();
    }
}