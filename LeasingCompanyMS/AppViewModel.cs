using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LeasingCompanyMS.Pages.LoginPage;

namespace LeasingCompanyMS;

public class AppViewModel : INotifyPropertyChanged {
    private object? _content = new LoginPage();

    public AppViewModel() {
        SetContentCommand = new Command(value => Content = value);
    }

    public ICommand SetContentCommand { get; }

    public object? Content {
        get => _content;
        set => SetProperty(ref _content, value);
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}