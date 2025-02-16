using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using LeasingCompanyMS.Components.MainWindow;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.Pages.ClientsInformationPage.Components.CreateNewClientWindow;
using LeasingCompanyMS.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace LeasingCompanyMS.Pages.ClientsInformationPage;

using IUsersRepository = IRepository<User, string, UsersFilter>;

public sealed class ClientsInformationPageViewModel : INotifyPropertyChanged {
    private readonly IUsersRepository _usersRepository = App.Instance.ServiceProvider.GetService<IUsersRepository>()!;
    
    public ClientsInformationPageViewModel() {
        OpenCreateNewClientWindowCommand =
            new RelayCommand(ExecuteOpenNewClientWindowCommand, CanExecuteOpenNewClientWindowCommand);
        Users = GetUsers();
    }
    
    // This constructor is used for testing purposes
    public ClientsInformationPageViewModel(IUsersRepository usersRepository) {
        _usersRepository = usersRepository;
        OpenCreateNewClientWindowCommand =
            new RelayCommand(ExecuteOpenNewClientWindowCommand, CanExecuteOpenNewClientWindowCommand);
        Users = GetUsers();
    }

    // Disgusting workaround for 6017 error, retarded xaml behaviour, lack of time.
    private void ExecuteOpenNewClientWindowCommand(object? parameter) {
        MainWindow createNewClientWindow = new MainWindow {
            SizeToContent = SizeToContent.WidthAndHeight,
            Owner = Application.Current.MainWindow,
        };

        CreateNewClientWindow createNewClientWindowPage = new CreateNewClientWindow() {
            DataContext = new CreateNewClientWindowModelView(createNewClientWindow.Close),
        };
        
        createNewClientWindow.MainWindowFrame.Content = createNewClientWindowPage;
        createNewClientWindow.Closed += (_, __) => { Users = [..GetUsers()]; };
        createNewClientWindow.ShowDialog();
    }

    private bool CanExecuteOpenNewClientWindowCommand(object? parameter) {
        return true;
    }

    private List<User> _users;

    public List<User> Users {
        get => _users;
        set {
            _users = value;
            OnPropertyChanged();
        }
    }
    
    private User? _selectedUser;

    public User? SelectedUser {
        get => _selectedUser;
        set {
            _selectedUser = value;
            OnPropertyChanged();
        }
    }

    public ICommand OpenCreateNewClientWindowCommand { get; }

    private List<User> GetUsers() {
        return _usersRepository.GetAll();
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}