using System.Windows.Input;

namespace LeasingCompanyMS.ViewModel;

public class RelayCommand : ICommand {
    private readonly Predicate<object>? _canExecuteAction;
    private readonly Action<object>? _executeAction;

    public RelayCommand(Action<object> executeAction) {
        _executeAction = executeAction;
        _canExecuteAction = null;
    }

    public RelayCommand(Action<object> executeAction, Predicate<object> canExecuteAction) {
        _executeAction = executeAction;
        _canExecuteAction = canExecuteAction;
    }

    public event EventHandler? CanExecuteChanged {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter) {
        return _canExecuteAction == null || _canExecuteAction(parameter);
    }

    public void Execute(object? parameter) {
        _executeAction(parameter);
    }
}