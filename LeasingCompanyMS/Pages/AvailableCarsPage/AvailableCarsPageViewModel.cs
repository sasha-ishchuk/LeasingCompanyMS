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
    public AvailableCarsPageViewModel() {
        OpenLeaseCarDialogCommand = new ViewModelCommand(ExecuteOpenLeaseCarDialogCommand, CanExecuteOpenLeaseCarDialogCommand);
        CloseLeaseCarDialogCommand = new ViewModelCommand(ExecuteCloseLeaseCarDialogCommand, CanExecuteCloseLeaseCarDialogCommand);
        AcceptLeasingConditionCommand = new ViewModelCommand(ExecuteAcceptLeasingConditionsCommand, CanExecuteAcceptLeasingConditionsCommand);
    }

    private readonly ICarsRepository _carsRepository = App.ServiceProvider.GetService<ICarsRepository>()!;
    private readonly ILeasingsRepository _leasingsRepository = App.ServiceProvider.GetService<ILeasingsRepository>()!;
    private Car? _selectedCar;
    private LeasingTerms _leasingTerms = new() {
        Leasing = new(),
        Car = new(),
    };
    private Visibility _isLeaseCarDialogOpen = Visibility.Collapsed;
    private DateTime? _leaseUntil; // TODO(piotr.klosowski): remove this in future
    
    public Car? SelectedCar {
        get => _selectedCar;
        set {
            _selectedCar = value;
            OnPropertyChanged();
        }
    }

    public LeasingTerms LeasingTerms {
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
            
            var now = DateTime.Now;
            LeasingTerms.Leasing.To = (DateTime)value!; // Oooof
            LeasingTerms.Leasing.Months = (LeaseUntil.Value.Year - now.Year) * 12 + LeaseUntil.Value.Month - now.Month;
            OnPropertyChanged(nameof(LeasingTerms));
        }
    }

    public List<Car> Cars => _carsRepository.GetAll(); // TODO(piotr.klosowski): This needs to be refreshed and handle events

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
        LeasingTerms = new() {
            Leasing = new(),
            Car = SelectedCar!,
        };
    }

    private void ExecuteCloseLeaseCarDialogCommand(object obj) {
        IsLeaseCarDialogOpen = Visibility.Collapsed;
    }

    private bool CanExecuteCloseLeaseCarDialogCommand(object obj) {
        return IsLeaseCarDialogOpen == Visibility.Visible;
    }

    private void ExecuteAcceptLeasingConditionsCommand(object obj) {
        var now = DateTime.Now;
        if (LeaseUntil > DateTime.Now) {
            LeasingTerms = new LeasingTerms() {
                Leasing = {
                    UserId = Thread.CurrentPrincipal!.Identity!.Name!,
                    CarId = _selectedCar!.Id,
                    From = now,
                    To = LeaseUntil.Value,
                    Months = (LeaseUntil.Value.Year - now.Year) * 12 + LeaseUntil.Value.Month - now.Month,
                    Conditions = "",
                }
            };
        }

        if (_leasingTerms != null) _leasingsRepository.Add(_leasingTerms.Leasing);
        IsLeaseCarDialogOpen = Visibility.Collapsed;
    }

    private bool CanExecuteAcceptLeasingConditionsCommand(object obj) {
        return IsLeaseCarDialogOpen == Visibility.Visible && LeaseUntil is not null && SelectedCar is not null;
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