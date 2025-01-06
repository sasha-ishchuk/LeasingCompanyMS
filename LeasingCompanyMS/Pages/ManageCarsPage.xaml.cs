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
    /// Logika interakcji dla klasy ManageCarsPage.xaml
    /// </summary>
    public partial class ManageCarsPage : Page
    {
        private IEnumerable<String> cars;
        private CarsRepository carsRepository = new CarsRepository();

        public ManageCarsPage()
        {
            InitializeComponent();
            PopulateDataGrid();
        }

        private void PopulateDataGrid()
        {
            var cars = carsRepository.GetAll();
            carsDataGrid.ItemsSource = cars;
        }
    }
}
