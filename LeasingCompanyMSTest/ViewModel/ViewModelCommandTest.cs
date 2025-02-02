using LeasingCompanyMS.ViewModel;

namespace LeasingCompanyMSTest.ViewModel;

[TestClass]
public class ViewModelCommandTest {
    private bool _canExecuteCalled;
    private bool _executeCalled;

    [TestMethod]
    public void CanExecute_WhenCanExecuteActionIsNull_ThenReturnsTrue() {
        // given
        var command = new ViewModelCommand(param => { });
        // when
        var result = command.CanExecute(null);
        // then
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CanExecute_WhenCanExecuteActionReturnsTrue_ThenReturnsTrue() {
        // given
        var command = new ViewModelCommand(param => { }, param => true);
        // when
        var result = command.CanExecute(null);
        // then
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CanExecute_WhenCanExecuteActionReturnsFalse_ThenReturnsFalse() {
        // given
        var command = new ViewModelCommand(param => { }, param => false);
        // when
        var result = command.CanExecute(null);
        // then
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Execute_ShouldCallExecuteAction() {
        // given
        var command = new ViewModelCommand(param => _executeCalled = true);
        // when
        command.Execute(null);
        // then
        Assert.IsTrue(_executeCalled);
    }

    [TestMethod]
    public void CanExecute_ShouldCallCanExecuteAction() {
        // given
        var command = new ViewModelCommand(param => { }, param =>
        {
            _canExecuteCalled = true;
            return true;
        });
        // when
        command.CanExecute(null);
        // then
        Assert.IsTrue(_canExecuteCalled);
    }
}
