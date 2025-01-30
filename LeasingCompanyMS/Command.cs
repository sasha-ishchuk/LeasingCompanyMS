using System.Windows.Input;

namespace LeasingCompanyMS;

public sealed class Command : ICommand
{
    private readonly Action<object?> execute;
    private readonly Func<object?, bool> canExecute;
    public Command(
        Action<object?> execute,
        Func<object?, bool>? canExecute = null
    )
    {
        this.execute = execute;
        this.canExecute = canExecute ?? (static _ => true);

    }
    
    public void Execute(object? parameter) => execute(parameter);
    public bool CanExecute(object? parameter) => canExecute(parameter);
    public event EventHandler? CanExecuteChanged;
    public void NotifyCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}