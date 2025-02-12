using System.Windows;
using System.Windows.Input;
using LeasingCompanyMS.Model;

namespace LeasingCompanyMS.Pages.Components.PurchaseCar;

public partial class PurchaseCarSummary {
    public static readonly DependencyProperty CarProperty = DependencyProperty.Register(
        nameof(Car),
        typeof(Car),
        typeof(PurchaseCarSummary),
        new PropertyMetadata(default(Car))
    );

    public static readonly DependencyProperty CarPriceProperty = DependencyProperty.Register(
        nameof(CarPrice),
        typeof(decimal),
        typeof(PurchaseCarSummary),
        new PropertyMetadata(default(decimal))
    );

    public static readonly DependencyProperty ConfirmCarPurchaseCommandProperty = DependencyProperty.Register(
        nameof(ConfirmCarPurchaseCommand),
        typeof(ICommand),
        typeof(PurchaseCarSummary),
        new UIPropertyMetadata(null)
    );

    public static readonly DependencyProperty DeclineCarPurchaseCommandProperty = DependencyProperty.Register(
        nameof(DeclineCarPurchaseCommand),
        typeof(ICommand),
        typeof(PurchaseCarSummary),
        new UIPropertyMetadata(null)
    );

    public PurchaseCarSummary() {
        InitializeComponent();
    }

    public Car? Car {
        get => (Car)GetValue(CarProperty);
        set => SetValue(CarProperty, value);
    }

    public decimal? CarPrice {
        get => (decimal)GetValue(CarPriceProperty);
        set => SetValue(CarPriceProperty, value);
    }

    public ICommand? ConfirmCarPurchaseCommand {
        get => (ICommand)GetValue(ConfirmCarPurchaseCommandProperty);
        set => SetValue(ConfirmCarPurchaseCommandProperty, value);
    }

    public ICommand? DeclineCarPurchaseCommand {
        get => (ICommand)GetValue(DeclineCarPurchaseCommandProperty);
        set => SetValue(DeclineCarPurchaseCommandProperty, value);
    }
}