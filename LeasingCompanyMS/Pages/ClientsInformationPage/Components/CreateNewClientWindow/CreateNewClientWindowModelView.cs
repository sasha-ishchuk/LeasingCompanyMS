using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using static System.Guid;

namespace LeasingCompanyMS.Pages.Components.NewClient;

using IUsersRepository = IRepository<User, string, UsersFilter>;

public class CreateNewClientWindowModelView : INotifyPropertyChanged {
    private readonly IUsersRepository _usersRepository = App.ServiceProvider.GetService<IUsersRepository>()!;
    private readonly Action _closeWindowDelegate;
    
    public CreateNewClientWindowModelView(Action closeWindowDelegate) {
        CreateNewClientCommand = new RelayCommand(ExecuteCreateNewClientCommand, CanExecuteCreateNewClientCommand);
        _closeWindowDelegate = closeWindowDelegate;
    }

    private void ExecuteCreateNewClientCommand(object? parameter) {
        _usersRepository.Add(new User {
            Id = NewGuid().ToString(),
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            PhoneNumber = PhoneNumber,
            AddressLine1 = AddressLine1,
            AddressLine2 = AddressLine2,
            City = City,
            State = State,
            ZipCode = ZipCode,
            Country = Country,
            CompanyName = CompanyName,
            TaxId = TaxId,
            Username = Username,
            Password = Password,
            Role = "user",
        });
        _closeWindowDelegate();
    }

    private bool CanExecuteCreateNewClientCommand(object? parameter) {
        return Email != "" &&
               FirstName != "" &&
               LastName != "" &&
               AddressLine1 != "" &&
               AddressLine2 != "" &&
               City != "" &&
               State != "" &&
               ZipCode != "" &&
               Country != "" &&
               CompanyName != "" &&
               TaxId != "" &&
               Username != "" &&
               Password != "";
    }

    private string _email = "";

    public string Email {
        get => _email;
        set {
            _email = value;
            OnPropertyChanged();
        }
    }

    private string _firstName = "";

    public string FirstName {
        get => _firstName;
        set {
            _firstName = value;
            OnPropertyChanged();
        }
    }

    private string _lastName = "";

    public string LastName {
        get => _lastName;
        set {
            _lastName = value;
            OnPropertyChanged();
        }
    }

    private string _phoneNumber = "";

    public string PhoneNumber {
        get => _phoneNumber;
        set {
            _phoneNumber = value;
            OnPropertyChanged();
        }
    }

    private string _addressLine1 = "";

    public string AddressLine1 {
        get => _addressLine1;
        set {
            _addressLine1 = value;
            OnPropertyChanged();
        }
    }

    private string _addressLine2 = "";

    public string AddressLine2 {
        get => _addressLine2;
        set {
            _addressLine2 = value;
            OnPropertyChanged();
        }
    }

    private string _city = "";

    public string City {
        get => _city;
        set {
            _city = value;
            OnPropertyChanged();
        }
    }

    private string _state = "";

    public string State {
        get => _state;
        set {
            _state = value;
            OnPropertyChanged();
        }
    }

    private string _zipCode = "";

    public string ZipCode {
        get => _zipCode;
        set {
            _zipCode = value;
            OnPropertyChanged();
        }
    }

    private string _country = "";

    public string Country {
        get => _country;
        set {
            _country = value;
            OnPropertyChanged();
        }
    }

    private string _companyName = "";

    public string CompanyName {
        get => _companyName;
        set {
            _companyName = value;
            OnPropertyChanged();
        }
    }

    private string _taxId = "";

    public string TaxId {
        get => _taxId;
        set {
            _taxId = value;
            OnPropertyChanged();
        }
    }

    private string _username = "";

    public string Username {
        get => _username;
        set {
            _username = value;
            OnPropertyChanged();
        }
    }

    private string _password = "";

    public string Password {
        get => _password;
        set {
            _password = value;
            OnPropertyChanged();
        }
    }

    private string _role = "";

    public string Role {
        get => _role;
        set {
            _role = value;
            OnPropertyChanged();
        }
    }

    public ICommand CreateNewClientCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}