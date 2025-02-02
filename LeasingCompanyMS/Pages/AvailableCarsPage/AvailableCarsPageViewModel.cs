using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace LeasingCompanyMS.Pages.BrowseCarsPage;

using ICarsRepository = IRepository<Car, string, CarsFilter>;
using ILeasingsRepository = IRepository<Leasing, string, LeasingsFilter>;

public sealed class AvailableCarsPageViewModel : INotifyPropertyChanged {
    private readonly ICarsRepository _carsRepository = App.ServiceProvider.GetService<ICarsRepository>()!;
    private readonly ILeasingsRepository _leasingsRepository = App.ServiceProvider.GetService<ILeasingsRepository>()!;
    
    private Car? _selectedCar;
    private List<Car>? _availableCars;
    private LeasingTerms? _leasingTerms;
    private Visibility _isLeaseCarDialogOpen = Visibility.Collapsed;
    private DateTime? _leaseUntil;
    
    public AvailableCarsPageViewModel() {
        OpenLeaseCarDialogCommand =
            new ViewModelCommand(ExecuteOpenLeaseCarDialogCommand, CanExecuteOpenLeaseCarDialogCommand);
        CloseLeaseCarDialogCommand =
            new ViewModelCommand(ExecuteCloseLeaseCarDialogCommand, CanExecuteCloseLeaseCarDialogCommand);
        AcceptLeasingConditionCommand = new ViewModelCommand(ExecuteAcceptLeasingConditionsCommand,
            CanExecuteAcceptLeasingConditionsCommand);

        AvailableCars = GetAvailableCars();
    }

    public Car? SelectedCar {
        get => _selectedCar;
        set {
            _selectedCar = value;
            OnPropertyChanged();
        }
    }

    public LeasingTerms? LeasingTerms {
        get => _leasingTerms;
        set {
            _leasingTerms = value;
            OnPropertyChanged();
        }
    }

    public DateTime? LeaseUntil {
        get => _leaseUntil;
        set {
            _leaseUntil = value;
            OnPropertyChanged();
        }
    }

    public List<Car>? AvailableCars {
        get => _availableCars;
        set {
            _availableCars = value;
            OnPropertyChanged();
        }
    }

    public Visibility IsLeaseCarDialogOpen {
        get => _isLeaseCarDialogOpen;
        set {
            _isLeaseCarDialogOpen = value;
            OnPropertyChanged();
        }
    }

    public ICommand OpenLeaseCarDialogCommand { get; }
    public ICommand CloseLeaseCarDialogCommand { get; }
    public ICommand AcceptLeasingConditionCommand { get; }

    private bool CanExecuteOpenLeaseCarDialogCommand(object obj) {
        return (IsLeaseCarDialogOpen == Visibility.Hidden || IsLeaseCarDialogOpen == Visibility.Collapsed) &&
               SelectedCar is not null;
    }

    private void ExecuteOpenLeaseCarDialogCommand(object obj) {
        IsLeaseCarDialogOpen = Visibility.Visible;
        LeasingTerms = new LeasingTerms {
            Leasing = new Leasing() {
                CarId = SelectedCar!.Id,
                UserId = Thread.CurrentPrincipal.Identity.Name,
                From = DateTime.Now,
                To = LeaseUntil!.Value,
            },
            Car = SelectedCar!
        };
    }

    private void ExecuteCloseLeaseCarDialogCommand(object obj) {
        IsLeaseCarDialogOpen = Visibility.Collapsed;
    }

    private bool CanExecuteCloseLeaseCarDialogCommand(object obj) {
        return IsLeaseCarDialogOpen == Visibility.Visible;
    }

    private void ExecuteAcceptLeasingConditionsCommand(object obj) {
        _leasingsRepository.Add(LeasingTerms.Leasing);
        AvailableCars = GetAvailableCars();
        LeasingTerms = null;
        IsLeaseCarDialogOpen = Visibility.Collapsed;
    }

    private bool CanExecuteAcceptLeasingConditionsCommand(object obj) {
        return IsLeaseCarDialogOpen == Visibility.Visible && LeaseUntil is not null && SelectedCar is not null;
    }

    private List<Car>? GetAvailableCars() {
        var leasings = _leasingsRepository.Get(new LeasingsFilter { IsActive = true });
        return _carsRepository.GetAll().FindAll(car => leasings.All(leasing => car.Id != leasing.CarId));
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}