using LeasingCompanyMS;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.Pages.ManageCarsPage.Components.AddNewCar;
using Moq;

namespace LeasingCompanyMSTest.Pages.ManageCarsPage;

using ICarsRepository = IRepository<Car, string, CarsFilter>;

[TestClass]
public class AddNewCarViewModelTest {
    private static CarsRepository _carsRepository;
    private static AddNewCarViewModel? _viewModel;
    private static Mock<IServiceProvider>? _mockServiceProvider;
    private static Action _closeWindowDelegate;
    private string _testFilePath;

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
        _closeWindowDelegate = () => { };
        _testFilePath = $"Json/cars_test_{Guid.NewGuid()}.json";
        File.Copy("Json/cars_test.json", _testFilePath);
        _carsRepository = new CarsRepository(_testFilePath);
        _viewModel = new AddNewCarViewModel(_closeWindowDelegate, _carsRepository);
    }
    
    [TestMethod]
    public void PropertyChanged_ShouldBeRaised_WhenPropertyIsChanged() {
        // given
        var propertyChangedRaised = false;
        _viewModel!.PropertyChanged += (sender, e) => {
            if (e.PropertyName == nameof(_viewModel.Brand)) {
                propertyChangedRaised = true;
            }
        };

        // when
        _viewModel.Brand = "Toyota";

        // then
        Assert.IsTrue(propertyChangedRaised);
    }
 
    [TestMethod]
    public void AddNewCarCommand_ShouldAddCarAndCloseWindow_WhenExecuted() {
        // given
        _viewModel!.Brand = "Toyota";
        _viewModel.Model = "Corolla";
        _viewModel.ProductionYear = 2022;
        _viewModel.BodyColor = "Red";
        _viewModel.SelectedFuelType = "Petrol";
        _viewModel.Displacement = "1.8L";
        _viewModel.Horsepower = "140";
        _viewModel.VinNumber = "1HGCM82633A123456";
        _viewModel.NetValue = 20000;

        // when
        _viewModel.AddNewCarCommand.Execute(null);

        // then
        var filter = new CarsFilter() {Brand = "Toyota", Model = "Corolla", ProductionYear = 2022};
        var cars = _carsRepository.Get(filter);
        Assert.IsNotNull(cars);
        Assert.AreEqual(1, cars!.Count);
        var car = cars[0];
        Assert.AreEqual("Toyota", car!.Brand);
        Assert.AreEqual("Corolla", car.Model);
        Assert.AreEqual(2022, car.ProductionYear);
        Assert.AreEqual("Red", car.BodyColor);
        Assert.AreEqual("Petrol", car.Engine.Type);
        Assert.AreEqual("1.8L", car.Engine.Displacement);
        Assert.AreEqual("1HGCM82633A123456", car.Vin);
    }

    [TestMethod]
    public void AddNewCarCommand_ShouldNotExecute_WhenRequiredFieldsAreEmpty() {
        // given
        _viewModel!.Brand = "";
        _viewModel.Model = "";

        // when
        var canExecute = _viewModel.AddNewCarCommand.CanExecute(null);

        // then
        Assert.IsFalse(canExecute);
    }
}