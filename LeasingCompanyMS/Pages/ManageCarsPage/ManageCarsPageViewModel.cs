using System.ComponentModel;
using System.Runtime.CompilerServices;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LeasingCompanyMS.Pages.ManageCarsPage;

using ICarsRepository = IRepository<Car, string, CarsFilter>;

public class ManageCarsPageViewModel : INotifyPropertyChanged {
    private readonly ICarsRepository _carsRepository = App.ServiceProvider.GetService<ICarsRepository>()!;
    private Car? _selectedCar;

    public List<Car> Cars => _carsRepository.GetAll();

    public Car? SelectedCar {
        get => _selectedCar;
        set {
            _selectedCar = value;
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