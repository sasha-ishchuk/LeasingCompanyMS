using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Windows.Input;
using LeasingCompanyMS.Components.MainWindow;
using LeasingCompanyMS.Layouts.MainMenuLayout;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace LeasingCompanyMS.Pages.LoginPage;

using IUsersRepository = IRepository<User, string, UsersFilter>;

public sealed class LoginPageViewModel : INotifyPropertyChanged {
    private readonly IUsersRepository _userRepository = App.ServiceProvider.GetService<IUsersRepository>()!;

    private string _username = string.Empty;
    private string _password = string.Empty;
    private string _errorMessage = string.Empty;

    public ICommand LoginCommand { get; }

    public LoginPageViewModel() {
        LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
    }

    public string Username {
        get => _username;
        set {
            _username = value;
            OnPropertyChanged();
        }
    }

    public string Password {
        get => _password;
        set {
            _password = value;
            OnPropertyChanged();
        }
    }

    public string ErrorMessage {
        get => _errorMessage;
        private set {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

    private bool CanExecuteLoginCommand(object obj) {
        return !(string.IsNullOrWhiteSpace(Username) || Username.Length < 4 || Password.Length < 4);
    }

    private void ExecuteLoginCommand(object obj) {
        var user = _userRepository.Get(new UsersFilter { Username = Username, Password = Password })
            .SingleOrDefault();
        if (user == null) {
            ErrorMessage = "* Invalid username or password";
            return;
        }
        
        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(user.Id), [user.Role]);

        MainWindow.NavigationService.Navigate(new MainMenuLayout());
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}