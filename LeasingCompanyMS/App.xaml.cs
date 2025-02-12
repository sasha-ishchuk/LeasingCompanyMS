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
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e) {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IUsersRepository>(new UsersRepository());
        serviceCollection.AddSingleton<ICarsRepository>(new CarsRepository());
        serviceCollection.AddSingleton<ILeasingsRepository>(new LeasingsRepository());
        serviceCollection.AddSingleton<IPaymentsRepository>(new PaymentsRepository());

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }
}