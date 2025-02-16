using LeasingCompanyMS;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.Pages.LoginPage;
using Moq;

namespace LeasingCompanyMSTest.Pages.LoginPage;

using IUsersRepository = IRepository<LeasingCompanyMS.Model.User, string, UsersFilter>;


[TestClass]
public class LoginPageViewModelTest {
    private static UsersRepository _userRepository;
    private static LoginPageViewModel? _viewModel;
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
        _viewModel = new LoginPageViewModel(_userRepository);
    }
    
    [TestMethod]
    public void CanExecuteLoginCommand_ValidInput_ReturnsTrue() {
        // given
        _viewModel!.Username = "admin";
        _viewModel.Password = "admin";

        // when
        var result = _viewModel.LoginCommand.CanExecute(null);

        // then
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    public void CanExecuteLoginCommand_InvalidInput_ReturnsFalse() {
        // given
        _viewModel!.Username = "ad";
        _viewModel.Password = "ad";

        // when
        var result = _viewModel.LoginCommand.CanExecute(null);

        // then
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void ExecuteLoginCommand_InvalidUserData_ReturnsErrorMessage() {
        // given
        _viewModel!.Username = "admin";
        _viewModel.Password = "admin123";

        // when
        _viewModel.LoginCommand.Execute(null);

        // then
        Assert.AreEqual("* Invalid username or password", _viewModel.ErrorMessage);
    }
}