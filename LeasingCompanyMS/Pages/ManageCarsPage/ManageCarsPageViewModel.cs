using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using LeasingCompanyMS.Components.MainWindow;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.Pages.ManageCarsPage.Components.AddNewCar;
using LeasingCompanyMS.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace LeasingCompanyMS.Pages.ManageCarsPage;

using ICarsRepository = IRepository<Car, string, CarsFilter>;

public class ManageCarsPageViewModel : INotifyPropertyChanged {
    private readonly ICarsRepository _carsRepository = App.Instance.ServiceProvider.GetService<ICarsRepository>()!;

    public ManageCarsPageViewModel() {
        Cars = GetCars();
        OpenAddNewCarWindowCommand = new RelayCommand(ExecuteOpenAddNewCarWindowCommand, CanExecuteOpenAddNewCarWindowCommand);
    }
    
    // this constructor is used for testing purposes
    public ManageCarsPageViewModel(ICarsRepository carsRepository) {
        _carsRepository = carsRepository;
        Cars = GetCars();
        OpenAddNewCarWindowCommand = new RelayCommand(ExecuteOpenAddNewCarWindowCommand, CanExecuteOpenAddNewCarWindowCommand);
    }
    
    // Disgusting workaround for 6017 error, retarded xaml behaviour, lack of time.
    private void ExecuteOpenAddNewCarWindowCommand(object? parameter) {
        MainWindow addNewCarWindow = new MainWindow {
            SizeToContent = SizeToContent.WidthAndHeight,
            Owner = Application.Current.MainWindow,
        };

        AddNewCarPageWindow addNewCarWindowPage = new AddNewCarPageWindow() {
            DataContext = new AddNewCarViewModel(addNewCarWindow.Close),
        };
        
        addNewCarWindow.MainWindowFrame.Content = addNewCarWindowPage;
        addNewCarWindow.Closed += (_, __) => { Cars = [..GetCars()]; };
        addNewCarWindow.ShowDialog();
    }

    private bool CanExecuteOpenAddNewCarWindowCommand(object? parameter) {
        return true;
    }

    private List<Car>? _cars;

    public List<Car> Cars {
        get => _cars;
        set {
            _cars = value;
            OnPropertyChanged();
        }
    }
    
    private Car? _selectedCar;
    
    public Car? SelectedCar {
        get => _selectedCar;
        set {
            _selectedCar = value;
            OnPropertyChanged();
        }
    }

    public ICommand OpenAddNewCarWindowCommand { get; }

    private List<Car> GetCars() {
        return _carsRepository.GetAll();
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}