using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace LeasingCompanyMS.CustomControls;

/// <summary>
///     Interaction logic for CustomPasswordBox.xaml
/// </summary>
public partial class CustomPasswordBox : UserControl {
    public static readonly DependencyProperty PasswordProperty
        = DependencyProperty.Register(nameof(Password), typeof(string), typeof(CustomPasswordBox));

    public string Password {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }

    public CustomPasswordBox() {
        InitializeComponent();
        txtPassword.PasswordChanged += OnPasswordChanged;
    }

    private void OnPasswordChanged(object sender, RoutedEventArgs e) {
        Password = new NetworkCredential(string.Empty, txtPassword.SecurePassword).Password;
    }
}