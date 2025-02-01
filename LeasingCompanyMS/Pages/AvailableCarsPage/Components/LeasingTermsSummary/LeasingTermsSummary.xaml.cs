using System.Windows;
using LeasingCompanyMS.Model;

namespace LeasingCompanyMS.Pages.Components.LeasingTermsSummary;

public partial class LeasingTermsSummary {
    public static readonly DependencyProperty LeasingTermsProperty = DependencyProperty.Register(
        nameof(LeasingTerms),
        typeof(LeasingTerms),
        typeof(LeasingTermsSummary),
        new PropertyMetadata(default(LeasingTerms))
    );


    public LeasingTermsSummary() {
        InitializeComponent();
    }

    public LeasingTerms? LeasingTerms {
        get => (LeasingTerms)GetValue(LeasingTermsProperty);
        set => SetValue(LeasingTermsProperty, value);
    }
}