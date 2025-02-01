using System.Windows;
using System.Windows.Input;
using LeasingCompanyMS.Model;

namespace LeasingCompanyMS.Pages.Components.ReturnCar;

public partial class ReturnCarSummary {
    public static readonly DependencyProperty CarProperty = DependencyProperty.Register(
        nameof(Car),
        typeof(Car),
        typeof(ReturnCarSummary),
        new PropertyMetadata(default(Car))
    );
    
    public static readonly DependencyProperty ConfirmCarReturnCommandProperty = DependencyProperty.Register(
        nameof(ConfirmCarReturnCommand),
        typeof(ICommand),
        typeof(ReturnCarSummary),
        new UIPropertyMetadata(null)
    );

    public static readonly DependencyProperty DeclineCarReturnCommandProperty = DependencyProperty.Register(
        nameof(DeclineCarReturnCommand),
        typeof(ICommand),
        typeof(ReturnCarSummary),
        new UIPropertyMetadata(null)
    );
    
    public ReturnCarSummary() {
        InitializeComponent();
    }
    
    public Car? Car {
        get => (Car)GetValue(CarProperty);
        set => SetValue(CarProperty, value);
    }

    public ICommand? ConfirmCarReturnCommand {
        get => (ICommand)GetValue(ConfirmCarReturnCommandProperty);
        set => SetValue(ConfirmCarReturnCommandProperty, value);
    }

    public ICommand? DeclineCarReturnCommand {
        get => (ICommand)GetValue(DeclineCarReturnCommandProperty);
        set => SetValue(DeclineCarReturnCommandProperty, value);
    }
}