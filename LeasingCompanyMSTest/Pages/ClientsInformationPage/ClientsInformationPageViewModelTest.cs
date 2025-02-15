using LeasingCompanyMS;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.Pages.ClientsInformationPage;
using Moq;

namespace LeasingCompanyMSTest.Pages.ClientsInformationPage;

using IUsersRepository = IRepository<User, string, UsersFilter>;


[TestClass]
public class ClientsInformationPageViewModelTest {
    private static UsersRepository _userRepository;
    private static ClientsInformationPageViewModel? _viewModel;
    private static Mock<IServiceProvider>? _mockServiceProvider;

    [ClassInitialize]
    public static void Setup(TestContext context) {
        _mockServiceProvider = new Mock<IServiceProvider>();
        _mockServiceProvider.Setup(sp => sp.GetService(typeof(IUsersRepository))).Returns(_userRepository);
        if (!App.IsCreated) {
            App.Instance = new App { ServiceProvider = _mockServiceProvider!.Object };
        }
        else {
            App.Instance.ServiceProvider = _mockServiceProvider!.Object;
        }
    }
    
    [TestInitialize]
    public void TestSetup() {
        _userRepository = new UsersRepository("Json/users_test.json");
        _viewModel = new ClientsInformationPageViewModel(_userRepository);
    }
    
    [TestMethod]
    public void Users_ShouldBeInitialized() {
        // when
        var users = _viewModel!.Users;

        // then
        Assert.IsNotNull(users);
        Assert.AreEqual(3, users.Count);
    }
    
    [TestMethod]
    public void SelectedUser_ShouldRaisePropertyChangedEvent() {
        // given
        var propertyChangedRaised = false;
        _viewModel!.PropertyChanged += (sender, e) => {
            if (e.PropertyName == nameof(_viewModel.SelectedUser)) {
                propertyChangedRaised = true;
            }
        };

        // when
        _viewModel.SelectedUser = new User { Id = "3", Username = "user3", Password = "pass3", Role = "role3" };

        // then
        Assert.IsTrue(propertyChangedRaised);
    }

    [TestMethod]
    public void OpenCreateNewClientWindowCommand_ShouldAlwaysBeExecutable() {
        // given
        var command = _viewModel!.OpenCreateNewClientWindowCommand;

        // when
        var canExecute = command.CanExecute(null);

        // then
        Assert.IsTrue(canExecute);
    }
}