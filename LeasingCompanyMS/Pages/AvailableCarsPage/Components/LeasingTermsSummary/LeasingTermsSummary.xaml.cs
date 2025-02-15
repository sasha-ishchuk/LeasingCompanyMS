using System.Windows;
using System.Windows.Input;
using LeasingCompanyMS.Model;

namespace LeasingCompanyMS.Pages.AvailableCarsPage.Components.LeasingTermsSummary;

public partial class LeasingTermsSummary {
    public static readonly DependencyProperty LeasingTermsProperty = DependencyProperty.Register(
        nameof(LeasingTerms),
        typeof(LeasingTerms),
        typeof(LeasingTermsSummary),
        new PropertyMetadata(default(LeasingTerms))
    );

    public static readonly DependencyProperty AcceptLeasingTermsCommandProperty = DependencyProperty.Register(
        nameof(AcceptLeasingTermsCommand),
        typeof(ICommand),
        typeof(LeasingTermsSummary),
        new UIPropertyMetadata(null)
    );

    public static readonly DependencyProperty RejectLeasingTermsCommandProperty = DependencyProperty.Register(
        nameof(RejectLeasingTermsCommand),
        typeof(ICommand),
        typeof(LeasingTermsSummary),
        new UIPropertyMetadata(null)
    );

    public static readonly DependencyProperty AllPaymentsMadeProperty = DependencyProperty.Register(
        nameof(AllPaymentsMade),
        typeof(bool),
        typeof(LeasingTermsSummary),
        new UIPropertyMetadata(null)
    );

    public LeasingTermsSummary() {
        InitializeComponent();
    }

    public LeasingTerms? LeasingTerms {
        get => (LeasingTerms)GetValue(LeasingTermsProperty);
        set => SetValue(LeasingTermsProperty, value);
    }

    public ICommand? AcceptLeasingTermsCommand {
        get => (ICommand)GetValue(AcceptLeasingTermsCommandProperty);
        set => SetValue(AcceptLeasingTermsCommandProperty, value);
    }

    public ICommand? RejectLeasingTermsCommand {
        get => (ICommand)GetValue(RejectLeasingTermsCommandProperty);
        set => SetValue(RejectLeasingTermsCommandProperty, value);
    }
    
    public bool? AllPaymentsMade {
        get => (bool)GetValue(AllPaymentsMadeProperty);
        set => SetValue(AllPaymentsMadeProperty, value);
    }
}