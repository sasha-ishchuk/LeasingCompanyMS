﻿using System.Configuration;
using System.Data;
using System.Windows;
using LeasingCompanyMS.Model;

namespace LeasingCompanyMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private UserRepository userRepository = new UserRepository();
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var loginView = new View.LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (!loginView.IsVisible && loginView.IsLoaded)
                {
                    var identity = Thread.CurrentPrincipal?.Identity;
                    if (identity != null && !string.IsNullOrEmpty(identity.Name))
                    {
                        var role = userRepository.GetRoleByUsername(identity.Name);
                        if (role == "admin")
                        {
                            var mainWindow = new MainWindow();
                            mainWindow.Show();
                            loginView.Close();
                        }
                        else
                        {
                            MessageBox.Show("You are not authorized to access this application. App will be close");
                            Application.Current.Shutdown();
                        }
                    }
                }
            };
        }
    }

}