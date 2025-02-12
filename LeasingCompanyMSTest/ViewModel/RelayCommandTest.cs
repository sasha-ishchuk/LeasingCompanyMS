using LeasingCompanyMS.ViewModel;

namespace LeasingCompanyMSTest.ViewModel;

[TestClass]
public class RelayCommandTest {
    private bool _canExecuteCalled = false;
    private bool _executeCalled = false;

    [TestMethod]
    public void CanExecute_WhenCanExecuteActionIsNull_ThenReturnsTrue() {
        // given
        var command = new RelayCommand(param => { });
        // when
        var result = command.CanExecute(null);
        // then
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CanExecute_WhenCanExecuteActionReturnsTrue_ThenReturnsTrue() {
        // given
        var command = new RelayCommand(param => { }, param => true);
        // when
        var result = command.CanExecute(null);
        // then
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CanExecute_WhenCanExecuteActionReturnsFalse_ThenReturnsFalse() {
        // given
        var command = new RelayCommand(param => { }, param => false);
        // when
        var result = command.CanExecute(null);
        // then
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Execute_ShouldCallExecuteAction() {
        // given
        var command = new RelayCommand(param => _executeCalled = true);
        // when
        command.Execute(null);
        // then
        Assert.IsTrue(_executeCalled);
    }

    [TestMethod]
    public void CanExecute_ShouldCallCanExecuteAction() {
        // given
        var command = new RelayCommand(param => { }, param =>
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
