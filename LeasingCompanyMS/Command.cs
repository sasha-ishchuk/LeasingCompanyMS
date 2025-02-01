using System.Windows.Input;

namespace LeasingCompanyMS;

public sealed class Command : ICommand {
    private readonly Func<object?, bool> canExecute;
    private readonly Action<object?> execute;

    public Command(
        Action<object?> execute,
        Func<object?, bool>? canExecute = null
    ) {
        this.execute = execute;
        this.canExecute = canExecute ?? (static _ => true);
    }

    public void Execute(object? parameter) {
        execute(parameter);
    }

    public bool CanExecute(object? parameter) {
        return canExecute(parameter);
    }

    public event EventHandler? CanExecuteChanged;

    public void NotifyCanExecuteChanged() {
        this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}