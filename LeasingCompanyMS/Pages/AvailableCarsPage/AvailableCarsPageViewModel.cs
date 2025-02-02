using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using static System.Guid;

namespace LeasingCompanyMS.Pages.BrowseCarsPage;

using ICarsRepository = IRepository<Car, string, CarsFilter>;
using ILeasingsRepository = IRepository<Leasing, string, LeasingsFilter>;

public sealed class AvailableCarsPageViewModel : INotifyPropertyChanged {
    private readonly ICarsRepository _carsRepository = App.ServiceProvider.GetService<ICarsRepository>()!;
    private readonly ILeasingsRepository _leasingsRepository = App.ServiceProvider.GetService<ILeasingsRepository>()!;
    private readonly double _interestRate = 0.017;

    public static KeyValuePair<int, string>[] LeasingDurationOptions { get; } = {
        new(12, "12 months"),
        new(24, "24 months"),
        new(36, "36 months"),
        new(48, "48 months"),
        new(60, "60 months"),
        new(72, "72 months"),
        new(84, "84 months")
    };

    public static KeyValuePair<double, string>[] DownPaymentOptions { get; } = {
        new(0.05, "5%"),
        new(0.1, "10%"),
        new(0.2, "20%"),
        new(0.3, "30%"),
        new(0.4, "40%"),
        new(0.5, "50%"),
        new(0.6, "60%"),
        new(0.7, "70%"),
        new(0.8, "80%"),
        new(0.9, "90%")
    };

    public static KeyValuePair<int, YearlyMileageLimitOption>[] YearlyMileageLimitOptions { get; } = {
        new(2500, new YearlyMileageLimitOption { Limit = 2500, Coefficient = 1.005 }),
        new(5000, new YearlyMileageLimitOption { Limit = 5000, Coefficient = 1.01 }),
        new(7500, new YearlyMileageLimitOption { Limit = 7500, Coefficient = 1.015 }),
        new(10000, new YearlyMileageLimitOption { Limit = 10000, Coefficient = 1.02 }),
        new(12500, new YearlyMileageLimitOption { Limit = 12500, Coefficient = 1.025 }),
        new(15000, new YearlyMileageLimitOption { Limit = 15000, Coefficient = 1.035 }),
        new(20000, new YearlyMileageLimitOption { Limit = 20000, Coefficient = 1.045 }),
        new(25000, new YearlyMileageLimitOption { Limit = 25000, Coefficient = 1.055 }),
        new(30000, new YearlyMileageLimitOption { Limit = 30000, Coefficient = 1.065 }),
        new(50000, new YearlyMileageLimitOption { Limit = 50000, Coefficient = 1.09 })
    };

    private Car? _selectedCar;

    public Car? SelectedCar {
        get => _selectedCar;
        set {
            _selectedCar = value;
            MonthlyLeaseInstalment = GetMonthlyLeaseInstallment();
            OnPropertyChanged();
        }
    }

    private List<Car>? _availableCars;

    public List<Car>? AvailableCars {
        get => _availableCars;
        set {
            _availableCars = value;
            OnPropertyChanged();
        }
    }


    private LeasingTerms? _leasingTerms;

    public LeasingTerms? LeasingTerms {
        get => _leasingTerms;
        set {
            _leasingTerms = value;
            OnPropertyChanged();
        }
    }

    private Visibility _isLeaseCarDialogOpen = Visibility.Collapsed;

    public Visibility IsLeaseCarDialogOpen {
        get => _isLeaseCarDialogOpen;
        set {
            _isLeaseCarDialogOpen = value;
            OnPropertyChanged();
        }
    }

    private int? _selectedLeasingDurationInMonths;

    public int? SelectedLeasingDurationInMonths {
        get => _selectedLeasingDurationInMonths;
        set {
            _selectedLeasingDurationInMonths = value;
            MonthlyLeaseInstalment = GetMonthlyLeaseInstallment();
            OnPropertyChanged();
        }
    }

    private double? _selectedDownPaymentOption;

    public double? SelectedDownPaymentOption {
        get => _selectedDownPaymentOption;
        set {
            _selectedDownPaymentOption = value;
            MonthlyLeaseInstalment = GetMonthlyLeaseInstallment();
            OnPropertyChanged();
        }
    }

    private YearlyMileageLimitOption? _selectedYearlyMileageLimitOption;

    public YearlyMileageLimitOption? SelectedYearlyMileageLimitOption {
        get => _selectedYearlyMileageLimitOption;
        set {
            _selectedYearlyMileageLimitOption = value;
            MonthlyLeaseInstalment = GetMonthlyLeaseInstallment();
            OnPropertyChanged();
        }
    }

    private double? _monthlyLeaseInstalment;

    public double? MonthlyLeaseInstalment {
        get => _monthlyLeaseInstalment;
        set {
            _monthlyLeaseInstalment = value;
            OnPropertyChanged();
        }
    }

    public AvailableCarsPageViewModel() {
        OpenLeaseCarDialogCommand =
            new ViewModelCommand(ExecuteOpenLeaseCarDialogCommand, CanExecuteOpenLeaseCarDialogCommand);
        CloseLeaseCarDialogCommand =
            new ViewModelCommand(ExecuteCloseLeaseCarDialogCommand, CanExecuteCloseLeaseCarDialogCommand);
        AcceptLeasingConditionCommand = new ViewModelCommand(ExecuteAcceptLeasingConditionsCommand,
            CanExecuteAcceptLeasingConditionsCommand);

        AvailableCars = GetAvailableCars();
        MonthlyLeaseInstalment = GetMonthlyLeaseInstallment();
    }


    public ICommand OpenLeaseCarDialogCommand { get; }
    public ICommand CloseLeaseCarDialogCommand { get; }
    public ICommand AcceptLeasingConditionCommand { get; }

    private bool CanExecuteOpenLeaseCarDialogCommand(object obj) {
        return SelectedCar != null
               && SelectedLeasingDurationInMonths != null
               && SelectedDownPaymentOption != null
               && SelectedYearlyMileageLimitOption != null;
    }

    private void ExecuteOpenLeaseCarDialogCommand(object obj) {
        IsLeaseCarDialogOpen = Visibility.Visible;
        LeasingTerms = new LeasingTerms {
            MonthlyLeaseInstallment = MonthlyLeaseInstalment!.Value,
            LeasingDurationInMonths = SelectedLeasingDurationInMonths!.Value,
            DownPayment = GetDownPayment(),
            YearlyMileageLimit = SelectedYearlyMileageLimitOption!.Limit,
            Car = SelectedCar!
        };
    }

    private void ExecuteCloseLeaseCarDialogCommand(object obj) {
        IsLeaseCarDialogOpen = Visibility.Collapsed;
    }

    private bool CanExecuteCloseLeaseCarDialogCommand(object obj) {
        return IsLeaseCarDialogOpen == Visibility.Visible;
    }

    private bool CanExecuteAcceptLeasingConditionsCommand(object obj) {
        return IsLeaseCarDialogOpen == Visibility.Visible && SelectedCar is not null;
    }

    private void ExecuteAcceptLeasingConditionsCommand(object obj) {
        if (SelectedCar is null || LeasingTerms is null) return;

        _leasingsRepository.Add(new Leasing {
            Id = NewGuid().ToString(),
            CarId = SelectedCar!.Id,
            UserId = Thread.CurrentPrincipal.Identity.Name,
            YearlyMileageLimit = LeasingTerms.YearlyMileageLimit,
            MonthlyLeaseInstallment = LeasingTerms!.MonthlyLeaseInstallment,
            DownPayment = LeasingTerms!.DownPayment,
            LeasingDurationInMonths = LeasingTerms!.LeasingDurationInMonths,
            From = DateTime.Now,
            To = DateTime.Now.AddMonths(LeasingTerms!.LeasingDurationInMonths)
        });

        // Leasing = new Leasing {
        //     CarId = SelectedCar!.Id,
        //     UserId = Thread.CurrentPrincipal.Identity.Name,
        //     From = DateTime.Now,
        //     To = DateTime.Now.AddMonths(SelectedLeasingDurationInMonths!.Value)
        // },
        // _leasingsRepository.Add(LeasingTerms.Leasing);
        AvailableCars = GetAvailableCars();
        LeasingTerms = null;
        IsLeaseCarDialogOpen = Visibility.Collapsed;
    }

    private List<Car>? GetAvailableCars() {
        var leasings = _leasingsRepository.Get(new LeasingsFilter { IsActive = true });
        return _carsRepository.GetAll().FindAll(car => leasings.All(leasing => car.Id != leasing.CarId));
    }

    private double GetDownPayment() {
        if (SelectedDownPaymentOption is null || SelectedCar is null) return 0;

        return SelectedCar!.EstimatedNetValue * SelectedDownPaymentOption!.Value;
    }

    private double GetMonthlyLeaseInstallment() {
        if (SelectedCar is null
            || SelectedLeasingDurationInMonths is null
            || SelectedYearlyMileageLimitOption is null
            || SelectedDownPaymentOption is null
           )
            return 0.0;

        var downPayment = GetDownPayment();
        var valueAfterDownPayment = SelectedCar!.EstimatedNetValue - downPayment;
        var leasingInstallment = valueAfterDownPayment /
                                 ((1 - 1 / Math.Pow(1 + _interestRate, SelectedLeasingDurationInMonths.Value)) /
                                  _interestRate) *
                                 SelectedYearlyMileageLimitOption.Coefficient;

        return double.Round(leasingInstallment, 2);
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