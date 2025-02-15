using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace LeasingCompanyMS.Pages.ManageCarsPage.Components.AddNewCar;

using ICarsRepository = IRepository<Car, string, CarsFilter>;

public class AddNewCarViewModel : INotifyPropertyChanged {
    private readonly ICarsRepository _carsRepository = App.Instance.ServiceProvider.GetService<ICarsRepository>()!;
    private readonly Action _closeWindowDelegate;
    public static string[] FuelTypeOptions { get; } = [
        "Petrol",
        "Diesel",
        "Electric",
        "Hydrogen"
    ];

    public AddNewCarViewModel(Action closeWindowDelegate) {
        AddNewCarCommand = new RelayCommand(ExecuteAddNewCarCommand, CanExecuteAddNewCarCommand);
        _closeWindowDelegate = closeWindowDelegate;
    }
    
    // this constructor is used for testing purposes
    public AddNewCarViewModel(Action closeWindowDelegate, ICarsRepository carsRepository) {
        _carsRepository = carsRepository;
        AddNewCarCommand = new RelayCommand(ExecuteAddNewCarCommand, CanExecuteAddNewCarCommand);
        _closeWindowDelegate = closeWindowDelegate;
    }
    
    #region Properties

    private string _brand;

    public string Brand {
        get => _brand;
        set {
            _brand = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    private string _model;

    public string Model {
        get => _model;
        set {
            _model = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    private int _productionYear;

    public int ProductionYear {
        get => _productionYear;
        set {
            _productionYear = value;
            OnPropertyChanged();
        }
    }

    private string _bodyColor;

    public string BodyColor {
        get => _bodyColor;
        set {
            _bodyColor = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    private string _displacement;

    public string Displacement {
        get => _displacement;
        set {
            _displacement = value;
            OnPropertyChanged();
        }
    }

    private string _horsepower;

    public string Horsepower {
        get => _horsepower;
        set {
            _horsepower = value;
            OnPropertyChanged();
        }
    }

    private string _vinNumber;

    public string VinNumber {
        get => _vinNumber;
        set {
            _vinNumber = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    private decimal _netValue;

    public decimal NetValue {
        get => _netValue;
        set {
            _netValue = value;
            OnPropertyChanged();
        }
    }

    private string _selectedFuelType;

    public string SelectedFuelType {
        get => _selectedFuelType;
        set {
            _selectedFuelType = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddNewCarCommand { get; }
    
    #endregion

    private bool CanExecuteAddNewCarCommand(object? _) {
        return !string.IsNullOrWhiteSpace(Brand) &&
               !string.IsNullOrWhiteSpace(Model) &&
               ProductionYear != 0 &&
               !string.IsNullOrWhiteSpace(BodyColor) &&
               !string.IsNullOrWhiteSpace(SelectedFuelType) &&
               !string.IsNullOrWhiteSpace(Displacement) &&
               !string.IsNullOrWhiteSpace(Horsepower) &&
               !string.IsNullOrWhiteSpace(VinNumber) &&
               NetValue != 0;
    }
    
    private void ExecuteAddNewCarCommand(object? _) {
        _carsRepository.Add(new Car {
            Id = Guid.NewGuid().ToString(),
            Brand = Brand,
            Model = Model,
            ProductionYear = ProductionYear,
            RegistrationNumber = "",
            BodyColor = BodyColor,
            Engine = new Engine {
                Type = SelectedFuelType,
                Displacement = Displacement,
                Power = Horsepower,
            },
            Vin = VinNumber,
            Packages = [],
            EstimatedNetValue = (int)NetValue,
            Status = CarStatus.New
        });
        _closeWindowDelegate();
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}