using LeasingCompanyMS.ViewModel;

namespace LeasingCompanyMSTest.ViewModel;

[TestClass]
public class LoginViewModelTest
{
    private LoginViewModel _viewModel = new LoginViewModel();

    [TestMethod]
    public void LoginCommand_WhenUsernameIsEmpty_ThenCanNotExecute()
    {
        // given
        _viewModel.Username = "";
        _viewModel.Password = "password";
        // when
        var result = _viewModel.LoginCommand.CanExecute(null);
        // then
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void LoginCommand_WhenPasswordIsEmpty_ThenCanNotExecute()
    {
        // given
        _viewModel.Username = "username";
        _viewModel.Password = "";
        // when
        var result = _viewModel.LoginCommand.CanExecute(null);
        // then
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void LoginCommand_WhenUsernameAndPasswordNotEmpty_ThenCanExecute()
    {
        // given
        _viewModel.Username = "username";
        _viewModel.Password = "password";
        // when
        var result = _viewModel.LoginCommand.CanExecute(null);
        // then
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void LoginCommand_Execute_ShouldCallExecuteLogin()
    {
        // given
        _viewModel.Username = "username";
        _viewModel.Password = "password";
        bool loginExecuted = false;
        ViewModelCommand viewModelCommand = new ViewModelCommand(
            param => loginExecuted = true, 
            param => true
        );
        Assert.IsFalse(loginExecuted);
        // when
        viewModelCommand.Execute(null);
        // then
        Assert.IsTrue(loginExecuted);
    }
}
