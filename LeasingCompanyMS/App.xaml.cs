using System.Windows;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LeasingCompanyMS;

using ICarsRepository = IRepository<Car, string, CarsFilter>;
using ILeasingsRepository = IRepository<Leasing, string, LeasingsFilter>;
using IUsersRepository = IRepository<User, string, UsersFilter>;
using IPaymentsRepository = IRepository<Payment, string, PaymentsFilter>;

public partial class App : Application {
    
    private static App? _instance;
    private IServiceProvider _serviceProvider = null!;

    public static App Instance {
        get => _instance ?? throw new InvalidOperationException("App instance is not initialized.");
        set => _instance = value;
    }

    public IServiceProvider ServiceProvider {
        get => _serviceProvider;
        set => _serviceProvider = value;
    }
    
    public static bool IsCreated => _instance != null;

    protected override void OnStartup(StartupEventArgs e) {
        base.OnStartup(e);
        _instance = this;

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IUsersRepository>(new UsersRepository());
        serviceCollection.AddSingleton<ICarsRepository>(new CarsRepository());
        serviceCollection.AddSingleton<ILeasingsRepository>(new LeasingsRepository());
        serviceCollection.AddSingleton<IPaymentsRepository>(new PaymentsRepository());

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }
}