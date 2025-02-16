using LeasingCompanyMS;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.Pages.ManageCarsPage;
using Moq;

namespace LeasingCompanyMSTest.Pages.ManageCarsPage;

using ICarsRepository = IRepository<Car, string, CarsFilter>;

[TestClass]
public class ManageCarsPageViewModelTest {
    private static CarsRepository _carsRepository;
    private static ManageCarsPageViewModel? _viewModel;
    private static Mock<IServiceProvider>? _mockServiceProvider;

    [ClassInitialize]
    public static void Setup(TestContext context) {
        _mockServiceProvider = new Mock<IServiceProvider>();
        _mockServiceProvider.Setup(sp => sp.GetService(typeof(ICarsRepository))).Returns(_carsRepository);
        if (!App.IsCreated) {
            App.Instance = new App { ServiceProvider = _mockServiceProvider!.Object };
        }
        else {
            App.Instance.ServiceProvider = _mockServiceProvider!.Object;
        }
    }
    
    [TestInitialize]
    public void TestSetup() {
        _carsRepository = new CarsRepository("Json/cars_test.json");
        _viewModel = new ManageCarsPageViewModel(_carsRepository);
    }
    
    [TestMethod]
    public void Cars_ShouldBeInitialized_WhenViewModelIsCreated() {
        // given
        var cars = _viewModel!.Cars;

        // then
        Assert.IsNotNull(_viewModel.Cars);
        Assert.AreEqual(2, _viewModel.Cars.Count);
    }

    [TestMethod]
    public void SelectedCar_ShouldRaisePropertyChangedEvent() {
        // given
        var propertyChangedRaised = false;
        _viewModel!.PropertyChanged += (sender, e) => {
            if (e.PropertyName == nameof(_viewModel.SelectedCar)) {
                propertyChangedRaised = true;
            }
        };

        // when
        _viewModel.SelectedCar = new Car { Id = "1", Brand = "Toyota", Model = "Corolla" };

        // then
        Assert.IsTrue(propertyChangedRaised);
    }

    [TestMethod]
    public void OpenAddNewCarWindowCommand_ShouldAlwaysBeExecutable() {
        // when
        var canExecute = _viewModel!.OpenAddNewCarWindowCommand.CanExecute(null);

        // then
        Assert.IsTrue(canExecute);
    }
}