using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace LeasingCompanyMS.Components.MainWindow;

public partial class MainWindow {
    // MainWindow ...
    public MainWindow() {
        InitializeComponent();
    }

    public static NavigationService NavigationService =>
        ((MainWindow)Application.Current.MainWindow).MainWindowFrame.NavigationService;

    // HandleMinimizeWindow ...
    private void HandleMinimizeWindow(object sender, RoutedEventArgs e) {
        WindowState = WindowState.Minimized;
    }

    // HandleMaximizeWindow ...
    private void HandleMaximizeWindow(object sender, RoutedEventArgs e) {
        WindowState = WindowState.Maximized;
    }

    // HandleCloseWindow ...
    private void HandleCloseWindow(object sender, RoutedEventArgs e) {
        this.Close();
    }

    // HandleWindowMove ...
    private void HandleApplicationBarMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
        switch (e.ClickCount) {
            case 1:
                HandleApplicationBarDrag(sender, e);
                break;
            default:
                HandleApplicationBarDoubleClick(sender, e);
                break;
        }
    }

    private void HandleApplicationBarDoubleClick(object sender, MouseButtonEventArgs e) {
        WindowState = WindowState switch {
            WindowState.Normal => WindowState.Maximized,
            _ => WindowState.Normal
        };
    }

    private void HandleApplicationBarDrag(object sender, MouseButtonEventArgs e) {
        DragMove();
    }
}