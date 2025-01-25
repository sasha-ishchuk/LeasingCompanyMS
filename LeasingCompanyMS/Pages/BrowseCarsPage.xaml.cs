using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LeasingCompanyMS.Model;

namespace LeasingCompanyMS.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy BrowseCarsPage.xaml
    /// </summary>
    public partial class BrowseCarsPage : Page
    {
        public BrowseCarsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            carsComboBox.ItemsSource = CarsRepository.GetAvailableCars();   //todo possibly need to refresh
        }

        private void CarsComboBoxValueChanged(object sender, EventArgs e)
        {
            carInfoBox.Visibility = Visibility.Visible;
            var car = (Car) carsComboBox.SelectedItem;
            registrationValue.Content = car.Registration;
            vinValue.Content = car.VIN;
        }
    }
}
