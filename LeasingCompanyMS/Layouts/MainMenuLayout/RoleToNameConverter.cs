using System.Globalization;
using System.Security.Principal;
using System.Windows.Data;

namespace LeasingCompanyMS.Layouts.MainMenuLayout;

public class RoleToNameConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var principal = value as GenericPrincipal;
        if (principal == null) {
            return "Leasing MS - User";
        }

        var validRoles = (parameter as string)?.Split(',');
        if (validRoles == null || validRoles.Length == 0) {
            return "Leasing MS - Admin";
        }
        
        return validRoles.Any(role => principal.IsInRole(role)) ? "Leasing MS - Admin" : "Leasing MS - User";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}