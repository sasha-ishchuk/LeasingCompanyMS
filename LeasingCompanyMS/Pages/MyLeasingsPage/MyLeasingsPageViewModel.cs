using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using static System.Guid;

namespace LeasingCompanyMS.Pages.ManagePaymentsPage;

using ICarsRepository = IRepository<Car, string, CarsFilter>;
using ILeasingsRepository = IRepository<Leasing, string, LeasingsFilter>;
using IPaymentsRepository = IRepository<Payment, string, PaymentsFilter>;

public sealed class MyLeasingsPageViewModel : INotifyPropertyChanged {
    private readonly ICarsRepository _carsRepository = App.ServiceProvider.GetService<ICarsRepository>()!;
    private readonly ILeasingsRepository _leasingsRepository = App.ServiceProvider.GetService<ILeasingsRepository>()!;
    private readonly IPaymentsRepository _paymentsRepository = App.ServiceProvider.GetService<IPaymentsRepository>()!;

    private void ExecutePurchaseCarCommand(object parameter) {
        SelectedLeasing!.car!.Status = CarStatus.PurchasedByClient;
        _carsRepository.UpdateById(SelectedLeasing.car.Id, SelectedLeasing.car);

        var leasingPaymentsCount = SelectedLeasingPayments.Count;
        _paymentsRepository.Add(new Payment {
            Id = NewGuid().ToString(),
            OrdinalNumber = leasingPaymentsCount + 1,
            UserId = _userId!,
            CarId = SelectedLeasing.car.Id,
            LeasingId = SelectedLeasing!.leasing!.Id,
            NetAmount = SelectedLeasing!.leasing!.ResidualValue,
            GrossAmount = SelectedLeasing!.leasing!.ResidualValue * 1.23,
            IssueDate = DateTime.Now,
            DueDate = DateTime.Now.AddMonths(1),
            Status = PaymentStatus.Issued
        });
        SelectedLeasingPayments = GetSelectedLeasingPayments(SelectedLeasing.leasing.Id);
        CanPurchaseOrReturn = GetCanPurchaseOrReturn();
        IsPurchaseCarDialogOpen = false;
    }

    private bool CanExecutePurchaseCarCommand(object parameter) {
        return SelectedLeasing is { car: not null } &&
               SelectedLeasingPayments.All(payment =>
                   payment.Status is PaymentStatus.Paid or PaymentStatus.PaidOverdue);
    }

    private void ExecuteReturnCarCommand(object parameter) {
        SelectedLeasing!.car!.Status = CarStatus.ReturnedAfterLeasing;
        _carsRepository.UpdateById(SelectedLeasing.car.Id, SelectedLeasing.car);
        CanPurchaseOrReturn = GetCanPurchaseOrReturn();
        IsReturnCarDialogOpen = false;
    }

    private bool CanExecuteReturnCarCommand(object parameter) {
        return SelectedLeasing is { car: not null } &&
               SelectedLeasingPayments.All(payment =>
                   payment.Status is PaymentStatus.Paid or PaymentStatus.PaidOverdue);
    }

    public MyLeasingsPageViewModel() {
        PurchaseCarCommand = new RelayCommand(ExecutePurchaseCarCommand, CanExecutePurchaseCarCommand);
        OpenPurchaseCarDialogCommand = new RelayCommand(_ => IsPurchaseCarDialogOpen = true, _ => GetCanPurchaseOrReturn());
        ClosePurchaseCarDialogCommand = new RelayCommand(_ => IsPurchaseCarDialogOpen = false);

        ReturnCarCommand = new RelayCommand(ExecuteReturnCarCommand, CanExecuteReturnCarCommand);
        OpenReturnCarDialogCommand = new RelayCommand(_ => IsReturnCarDialogOpen = true, _ => GetCanPurchaseOrReturn());
        CloseReturnCarDialogCommand = new RelayCommand(_ => IsReturnCarDialogOpen = false);
    }

    private readonly string? _userId = Thread.CurrentPrincipal.Identity.Name;

    #region User leasings

    public List<LeasingWithCar> UserLeasings {
        get {
            var userLeasings = _leasingsRepository.Get(new LeasingsFilter { UserId = _userId });
            var cars = userLeasings.Select(leasing => new LeasingWithCar {
                leasing = leasing,
                car = _carsRepository.GetById(leasing.CarId)
            }).ToList();

            return cars;
        }
    }

    #endregion

    #region Selected leasing

    private LeasingWithCar? _selectedLeasing;

    public LeasingWithCar? SelectedLeasing {
        get => _selectedLeasing;
        set {
            _selectedLeasing = value;
            SelectedLeasingPayments = GetSelectedLeasingPayments(_selectedLeasing!.leasing!.Id);
            CanPurchaseOrReturn = GetCanPurchaseOrReturn();
            OnPropertyChanged();
        }
    }

    #endregion

    #region Selected leasing payments

    private List<Payment> _selectedLeasingPayments = new();

    public List<Payment> SelectedLeasingPayments {
        get => _selectedLeasingPayments;
        private set {
            _selectedLeasingPayments = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Can purchase or return

    private bool _canPurchaseOrReturn;

    public bool CanPurchaseOrReturn {
        get => _canPurchaseOrReturn;
        set {
            _canPurchaseOrReturn = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Selected payment

    private Payment? _selectedPayment;

    public Payment? SelectedPayment {
        get => _selectedPayment;
        set {
            _selectedPayment = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public ICommand PurchaseCarCommand { get; }
    public ICommand ReturnCarCommand { get; }
    public ICommand OpenPurchaseCarDialogCommand { get; }
    public ICommand ClosePurchaseCarDialogCommand { get; }

    public ICommand OpenReturnCarDialogCommand { get; }
    public ICommand CloseReturnCarDialogCommand { get; }


    private bool _isPurchaseCarDialogOpen;

    public bool IsPurchaseCarDialogOpen {
        get => _isPurchaseCarDialogOpen;
        set {
            _isPurchaseCarDialogOpen = value;
            OnPropertyChanged();
        }
    }

    private bool _isReturnCarDialogOpen;

    public bool IsReturnCarDialogOpen {
        get => _isReturnCarDialogOpen;
        set {
            _isReturnCarDialogOpen = value;
            OnPropertyChanged();
        }
    }

    private void ClosePurchaseCarDialog() {
        IsPurchaseCarDialogOpen = false;
    }

    private void CloseReturnCarDialog() {
        IsReturnCarDialogOpen = false;
    }

    private List<Payment> GetSelectedLeasingPayments(string leasingId) {
        return _paymentsRepository.Get(new PaymentsFilter { LeasingId = leasingId });
    }

    private bool GetCanPurchaseOrReturn() {
        return SelectedLeasing is { car.Status: CarStatus.CurrentlyLeased } &&
               SelectedLeasingPayments.All(p => p.Status is PaymentStatus.Paid or PaymentStatus.PaidOverdue);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}