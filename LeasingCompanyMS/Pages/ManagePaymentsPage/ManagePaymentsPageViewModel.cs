using System.ComponentModel;
using System.Runtime.CompilerServices;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LeasingCompanyMS.Pages.ManagePaymentsPage;

using ICarsRepository = IRepository<Car, string, CarsFilter>;
using ILeasingsRepository = IRepository<Leasing, string, LeasingsFilter>;
public class ManagePaymentsPageViewModel : INotifyPropertyChanged {
    private readonly ICarsRepository _carsRepository = App.Instance.ServiceProvider.GetService<ICarsRepository>()!;
    private readonly ILeasingsRepository _leasingsRepository = App.Instance.ServiceProvider.GetService<ILeasingsRepository>()!;

    public Leasing? Leasings {
        get {
            Console.WriteLine(_leasingsRepository
                .GetAll().ToString());
            
            return _leasingsRepository
                .Get(new LeasingsFilter { UserId = Thread.CurrentPrincipal.Identity.Name})
                .FirstOrDefault()!;
        }
    }

    private Leasing? _selectedLeasing;
    public Leasing? SelectedLeasing {
        get => _selectedLeasing;
        set {
            _selectedLeasing = value;
            OnPropertyChanged();
        }
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