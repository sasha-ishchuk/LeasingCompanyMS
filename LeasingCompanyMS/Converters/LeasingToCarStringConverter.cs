using System.Globalization;
using System.Windows.Data;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Pages;

namespace LeasingCompanyMS.Converters;

[ValueConversion(typeof(Leasing), typeof(string))]
public class LeasingToCarStringConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var leasingWithCar = (LeasingWithCar?)value;
        if (leasingWithCar == null) {
            return "";
        }
        
        return $"{leasingWithCar.leasing.Id} - {leasingWithCar.car.Brand} {leasingWithCar.car.Model} ({leasingWithCar.car.ProductionYear})";
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new InvalidOperationException();
    }
}