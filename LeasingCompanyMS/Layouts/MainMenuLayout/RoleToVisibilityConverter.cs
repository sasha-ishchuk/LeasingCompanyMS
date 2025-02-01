using System.Globalization;
using System.Security.Principal;
using System.Windows;
using System.Windows.Data;

namespace LeasingCompanyMS.Layouts.MainMenuLayout;

public class RoleToVisibilityConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var principal = value as GenericPrincipal;
        if (principal == null) return Visibility.Collapsed;

        var validRoles = (parameter as string)?.Split(',');
        if (validRoles == null || validRoles.Length == 0) return Visibility.Visible;

        return validRoles.Any(role => principal.IsInRole(role)) ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}