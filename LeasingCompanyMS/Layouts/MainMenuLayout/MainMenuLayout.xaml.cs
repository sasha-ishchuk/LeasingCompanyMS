using System.Security.Principal;
using System.Windows;
using LeasingCompanyMS.Components.MainWindow;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Pages.LoginPage;

namespace LeasingCompanyMS.Layouts.MainMenuLayout;

public partial class MainMenuLayout {
    public MainMenuLayout() {
        InitializeComponent();
    }

    private void LogoutAction(object sender, RoutedEventArgs e)
    {
        Thread.CurrentPrincipal = null;
        MainWindow.NavigationService.Navigate(new LoginPage());
    }
}