using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;

namespace LeasingCompanyMSTest.Model.Repositories;

[TestClass]
public class CarsRepositoryTest {
    private CarsRepository _carsRepository;
    private string _testFilePath;

    [TestInitialize]
    public void Setup() {
        _testFilePath = $"Json/cars_test_{Guid.NewGuid()}.json";
        File.Copy("Json/cars_test.json", _testFilePath);
        _carsRepository = new CarsRepository(_testFilePath);
    }

    [TestCleanup]
    public void Cleanup() {
        if (File.Exists(_testFilePath)) {
            File.Delete(_testFilePath);
        }
    }
       
    [TestMethod]
    public void GetAll_ReturnsAllCars() {
        // given/when
        var cars = _carsRepository.GetAll();
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(2, cars.Count);
    }
    
    [TestMethod]
    public void GetById_WhenCarExists_ThenReturnsCar() {
        // given/when
        var car = _carsRepository.GetById("d5a6d106-5bda-4e57-9df7-5ac35e12d8de");
        // then
        Assert.IsNotNull(car);
        Assert.AreEqual("Audi", car.Brand);
        Assert.AreEqual("A4", car.Model);
        Assert.AreEqual(2020, car.ProductionYear);
        Assert.AreEqual("KWA5516", car.RegistrationNumber);
        Assert.AreEqual("black", car.BodyColor);
        Assert.AreEqual("WAUZZZ8V7BA123456", car.Vin);
        Assert.AreEqual(154000, car.EstimatedNetValue);
        Assert.AreEqual(CarStatus.PurchasedByClient, car.Status);
        var engine = car.Engine;
        Assert.IsNotNull(engine);
        Assert.AreEqual("petrol", engine.Type);
        Assert.AreEqual("2.0", engine.Displacement);
        Assert.AreEqual("190", engine.Power);
        var packages = car.Packages;
        Assert.IsNotNull(packages);
        Assert.AreEqual(5, packages.Count);
    }

    [TestMethod]
    public void GetById_WhenCarDoesNotExist_ThenReturnsNull() {
        // given/when
        var car = _carsRepository.GetById("nonExistingId");
        // then
        Assert.IsNull(car);
    }
    
    [TestMethod]
    public void Get_WithBrandFilter_ReturnsFilteredCars() {
        // given
        var filter = new CarsFilter() { Brand = "Audi" };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(1, cars.Count);
        Assert.AreEqual("d5a6d106-5bda-4e57-9df7-5ac35e12d8de", cars[0].Id);
    }
    
    [TestMethod]
    public void Get_WithNonExistingBrandFilter_ReturnsNoCars() {
        // given
        var filter = new CarsFilter() { Brand = "BatmanCar" };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }
    
    [TestMethod]
    public void Get_WithModelFilter_ReturnsFilteredCars() {
        // given
        var filter = new CarsFilter() { Model = "A4" };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(1, cars.Count);
        Assert.AreEqual("d5a6d106-5bda-4e57-9df7-5ac35e12d8de", cars[0].Id);
    }
    
    [TestMethod]
    public void Get_WithNonExistingModelFilter_ReturnsNoCars() {
        // given
        var filter = new CarsFilter() { Model = "UJ" };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }
    
    [TestMethod]
    public void Get_WithYearFilter_ReturnsFilteredCars() {
        // given
        var filter = new CarsFilter() { ProductionYear = 2020 };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(1, cars.Count);
        Assert.AreEqual("d5a6d106-5bda-4e57-9df7-5ac35e12d8de", cars[0].Id);
    }
    
    [TestMethod]
    public void Get_WithNonExistingYearFilter_ReturnsNoCars() {
        // given
        var filter = new CarsFilter() { ProductionYear = 2077 };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }
    
    [TestMethod]
    public void Get_WithRegistrationFilter_ReturnsFilteredCars() {
        // given
        var filter = new CarsFilter() { RegistrationNumber = "KWA5516" };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(1, cars.Count);
        Assert.AreEqual("d5a6d106-5bda-4e57-9df7-5ac35e12d8de", cars[0].Id);
    }
    
    [TestMethod]
    public void Get_WithNonExistingRegistrationFilter_ReturnsNoCars() {
        // given
        var filter = new CarsFilter() { RegistrationNumber = "KRK777" };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }
    
    [TestMethod]
    public void Get_WithVinFilter_ReturnsFilteredCars() {
        // given
        var filter = new CarsFilter() { Vin = "WAUZZZ8V7BA123456" };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(1, cars.Count);
        Assert.AreEqual("d5a6d106-5bda-4e57-9df7-5ac35e12d8de", cars[0].Id);
    }
    
    [TestMethod]
    public void Get_WithNonExistingVinFilter_ReturnsNoCars() {
        // given
        var filter = new CarsFilter() { Vin = "000000111111" };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }
    
    [TestMethod]
    public void Get_WithNetValueFilter_ReturnsFilteredCars() {
        // given
        var filter = new CarsFilter() { EstimatedNetValue = 154000 };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(1, cars.Count);
        Assert.AreEqual("d5a6d106-5bda-4e57-9df7-5ac35e12d8de", cars[0].Id);
    }
    
    [TestMethod]
    public void Get_WithNonExistingNetValueFilter_ReturnsNoCars() {
        // given
        var filter = new CarsFilter() { EstimatedNetValue = 777 };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }
    
    [TestMethod]
    public void Get_WithStatusFilter_ReturnsFilteredCars() {
        // given
        var filter = new CarsFilter() { Status = CarStatus.PurchasedByClient };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(2, cars.Count);
        Assert.AreEqual("d5a6d106-5bda-4e57-9df7-5ac35e12d8de", cars[0].Id);
        Assert.AreEqual("3b57b876-d4fa-44d3-b5e5-bc399b7f26a6", cars[1].Id);
    }
    
    [TestMethod]
    public void Get_WithNonExistingStatusFilter_ReturnsNoCars() {
        // given
        var filter = new CarsFilter() { Status = CarStatus.New };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }
    
    [TestMethod]
    public void Get_WithPackageFilterWhenAllPackagesPresent_ReturnsFilteredCars() {
        // given
        List<string> packages =
        [
            "sport package",
            "comfort package",
            "lane assist package"
        ];
        var filter = new CarsFilter() { Packages = packages };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(1, cars.Count);
        Assert.AreEqual("d5a6d106-5bda-4e57-9df7-5ac35e12d8de", cars[0].Id);
    }
    
    [TestMethod]
    public void Get_WithPackageFilterWhenOnlySomePackagesPresent_ReturnsNoCars() {
        // given
        List<string> packages =
        [
            "sport package",
            "random package"
        ];
        var filter = new CarsFilter() { Packages = packages };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }
    
    [TestMethod]
    public void Get_WithNonExistingPackageFilter_ReturnsNoCars() {
        // given
        List<string> packages = ["random package"];
        var filter = new CarsFilter() { Packages = packages };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }
    
    [TestMethod]
    public void Get_WithEngineFilter_ReturnsFilteredCars() {
        // given
        var engine = new EngineFilter?( new EngineFilter() {
            Displacement = "2.0",
            Type = "petrol",
            Power = "190"
        });
        var filter = new CarsFilter() { Engine = engine };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(1, cars.Count);
        Assert.AreEqual("d5a6d106-5bda-4e57-9df7-5ac35e12d8de", cars[0].Id);
    }
    
    [TestMethod]
    public void Get_WithNonExistingEngineFilter_ReturnsNoCars() {
        // given
        var engine = new EngineFilter?( new EngineFilter() {
            Displacement = "2.0",
            Type = "petrol",
            Power = "200"
        });
        var filter = new CarsFilter() { Engine = engine };
        // when
        var cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }

    [TestMethod]
    public void UpdateById_WithExistingCarId_ReturnsTrue() {
        // given
        var brand = "BarbieJeep";
        var model = "GRL-1";
        var year = 2333;
        var realId = "d5a6d106-5bda-4e57-9df7-5ac35e12d8de";
        // when
        var updated = _carsRepository.UpdateById(realId, BuildCar(brand, model, year));
        // then
        Assert.IsTrue(updated);
        var car = _carsRepository.GetById(realId);
        Assert.IsNotNull(car);
        Assert.AreEqual(brand, car.Brand);
        Assert.AreEqual(model, car.Model);
        Assert.AreEqual(year, car.ProductionYear);
    }
    
    [TestMethod]
    public void UpdateById_WithNonExistingCarId_ReturnsFalse() {
        // given / when
        var updated = _carsRepository.UpdateById("randomId", BuildCar("randomCar", "randomModel", 2011));
        // then
        Assert.IsFalse(updated);
    }
    
    [TestMethod]
    public void AddCar_WithValidCar() {
        // given
        var brand = "BarbieJeep";
        var model = "GRL-1";
        var year = 2333;
        // when
        var id = _carsRepository.Add(BuildCar(brand, model, year));
        // then
        var car = _carsRepository.GetById(id);
        Assert.IsNotNull(car);
        Assert.AreEqual(brand, car.Brand);
        Assert.AreEqual(model, car.Model);
        Assert.AreEqual(year, car.ProductionYear);
    }

    private Car BuildCar(string brand, string model, int year) {
        return new Car() {
            Brand = brand,
            Model = model,
            ProductionYear = year,
            RegistrationNumber = "SASHA1",
            BodyColor = "black",
            Engine = new Engine() {
                Displacement = "2.0",
                Type = "petrol",
                Power = "190"
            },
            Vin = "TESTVIN123456",
            Packages =
            [
                "sport package",
                "comfort package"
            ],
            EstimatedNetValue = 354000,
            Status = CarStatus.New
        };
    }
}