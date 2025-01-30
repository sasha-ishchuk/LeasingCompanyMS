using System.Security.Principal;

namespace LeasingCompanyMS.Layouts.MainMenuLayout;

public class MainMenuLayoutViewModel {
        
    public GenericPrincipal? CurrentPrincipal {
        get => (GenericPrincipal)Thread.CurrentPrincipal!;
    }
}