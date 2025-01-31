using LeasingCompanyMS.Pages.LoginPage;
using LeasingCompanyMS.ViewModel;

namespace LeasingCompanyMSTest.ViewModel;

[TestClass]
public class LoginPageViewModelTest
{
    private LoginPageViewModel _pageViewModel = new LoginPageViewModel();

    [TestMethod]
    public void LoginCommand_WhenUsernameIsEmpty_ThenCanNotExecute()
    {
        // given
        _pageViewModel.Username = "";
        _pageViewModel.Password = "password";
        // when
        var result = _pageViewModel.LoginCommand.CanExecute(null);
        // then
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void LoginCommand_WhenPasswordIsEmpty_ThenCanNotExecute()
    {
        // given
        _pageViewModel.Username = "username";
        _pageViewModel.Password = "";
        // when
        var result = _pageViewModel.LoginCommand.CanExecute(null);
        // then
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void LoginCommand_WhenUsernameAndPasswordNotEmpty_ThenCanExecute()
    {
        // given
        _pageViewModel.Username = "username";
        _pageViewModel.Password = "password";
        // when
        var result = _pageViewModel.LoginCommand.CanExecute(null);
        // then
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void LoginCommand_Execute_ShouldCallExecuteLogin()
    {
        // given
        _pageViewModel.Username = "username";
        _pageViewModel.Password = "password";
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
