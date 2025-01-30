using System.ComponentModel;
using System.Runtime.CompilerServices;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LeasingCompanyMS.Pages;

public sealed class ClientsInformationPageViewModel : INotifyPropertyChanged {
    private readonly IRepository<Leasing, string, LeasingsFilter> _leasingsRepository = App.ServiceProvider.GetService<IRepository<Leasing, string, LeasingsFilter>>()!;
    private Leasing? _selectedLeasing;

    public List<Leasing> Leasings => _leasingsRepository.GetAll();

    public Leasing? SelectedLeasing {
        get => _selectedLeasing;
        set {
            _selectedLeasing = value;
            OnPropertyChanged();
        }
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