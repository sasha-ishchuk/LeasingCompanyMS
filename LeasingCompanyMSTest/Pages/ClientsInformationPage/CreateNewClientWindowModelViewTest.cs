using LeasingCompanyMS;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.Pages.ClientsInformationPage.Components.CreateNewClientWindow;
using Moq;

namespace LeasingCompanyMSTest.Pages.ClientsInformationPage;

using IUsersRepository = IRepository<LeasingCompanyMS.Model.User, string, UsersFilter>;

[TestClass]
public class CreateNewClientWindowModelViewTest {
    private static UsersRepository _userRepository;
    private static CreateNewClientWindowModelView? _viewModel;
    private static Mock<IServiceProvider>? _mockServiceProvider;
    private static Action _closeWindowDelegate;
    private string _testFilePath;
    

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
        _closeWindowDelegate = () => { };
        _testFilePath = $"Json/users_test_{Guid.NewGuid()}.json";
        File.Copy("Json/users_test.json", _testFilePath);
        _userRepository = new UsersRepository(_testFilePath);
        _viewModel = new CreateNewClientWindowModelView(_closeWindowDelegate, _userRepository);
    }
    
    [TestCleanup]
    public void Cleanup() {
        if (File.Exists(_testFilePath)) {
            File.Delete(_testFilePath);
        }
    }

    [TestMethod]
    public void CanExecuteCreateNewClientCommand_ValidData_ReturnsTrue() {
        // given
        _viewModel!.Email = "test@example.com";
        _viewModel.FirstName = "John";
        _viewModel.LastName = "Doe";
        _viewModel.AddressLine1 = "Line 1";
        _viewModel.AddressLine2 = "Line 2";
        _viewModel.City = "City";
        _viewModel.State = "State";
        _viewModel.ZipCode = "12345";
        _viewModel.Country = "Country";
        _viewModel.CompanyName = "Test Company";
        _viewModel.TaxId = "123-45-6789";
        _viewModel.Username = "username";
        _viewModel.Password = "password";

        // when
        var canExecute = _viewModel.CreateNewClientCommand.CanExecute(null);

        // then
        Assert.IsTrue(canExecute);
    }
    
    [TestMethod]
    public void CanExecuteCreateNewClientCommand_InvalidData_ReturnsFalse() {
        // given
        _viewModel!.Email = "";
        _viewModel.FirstName = "";
        _viewModel.LastName = "";

        // when
        var canExecute = _viewModel.CreateNewClientCommand.CanExecute(null);

        // then
        Assert.IsFalse(canExecute);
    }
    
    [TestMethod]
    public void ExecuteCreateNewClientCommand_ValidData_AddsUserAndClosesWindow() {
        // given
        _viewModel!.Email = "test@example.com";
        _viewModel.FirstName = "John";
        _viewModel.LastName = "Doe";
        _viewModel.AddressLine1 = "Line 1";
        _viewModel.AddressLine2 = "Line 2";
        _viewModel.City = "City";
        _viewModel.State = "State";
        _viewModel.ZipCode = "12345";
        _viewModel.Country = "Country";
        _viewModel.CompanyName = "Test Company";
        _viewModel.TaxId = "123-45-6789";
        _viewModel.Username = "username";
        _viewModel.Password = "password";

        // when
        _viewModel.CreateNewClientCommand.Execute(null);

        // then
        var filter = new UsersFilter() {
            Username = "username",
            Password = "password"
        };
        var users = _userRepository.Get(filter);
        Assert.IsNotNull(users);
        Assert.AreEqual(1, users.Count);
        var user = users[0];
        Assert.AreEqual("test@example.com", user!.Email);
        Assert.AreEqual("John", user.FirstName);
        Assert.AreEqual("Doe", user.LastName);
        Assert.AreEqual("Line 1", user.AddressLine1);
        Assert.AreEqual("Line 2", user.AddressLine2);
        Assert.AreEqual("City", user.City);
        Assert.AreEqual("State", user.State);
        Assert.AreEqual("12345", user.ZipCode);
        Assert.AreEqual("Country", user.Country);
        Assert.AreEqual("Test Company", user.CompanyName);
        Assert.AreEqual("123-45-6789", user.TaxId);
        Assert.AreEqual("username", user.Username);
        Assert.AreEqual("password", user.Password);
        Assert.AreEqual("user", user.Role);
    }
}