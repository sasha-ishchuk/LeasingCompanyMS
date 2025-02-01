using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;

namespace LeasingCompanyMSTest.Model.Repositories;

[TestClass]
public class CarsRepositoryTest {
    private readonly CarsRepository _carsRepository = new();
       
    [TestMethod]
    public void GetAll_ReturnsAllCars() {
        // given/when
        List<Car> cars = _carsRepository.GetAll();
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(10, cars.Count);
    }
    
    [TestMethod]
    public void GetById_WhenCarExists_ThenReturnsCar() {
        // given/when
        Car? car = _carsRepository.GetById("1");
        // then
        Assert.IsNotNull(car);
        Assert.AreEqual("DWR7351", car.Registration);
        Assert.AreEqual("Audi", car.Mark);
        Assert.AreEqual("Q8", car.Model);
        Assert.AreEqual(2019, car.Year);
        Assert.AreEqual("LPA0LMHJ02JFW6040", car.VIN);
    }

    [TestMethod]
    public void GetById_WhenCarDoesNotExist_ThenReturnsNull() {
        // given/when
        Car? car = _carsRepository.GetById("nonexistingid");
        // then
        Assert.IsNull(car);
    }
    
    [TestMethod]
    public void Get_WithRegistrationFilter_ReturnsFilteredCars() {
        // given
        var filter = new CarsFilter() { Registration = "KWA5516" };
        // when
        List<Car> cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(1, cars.Count);
        Assert.AreEqual("0", cars[0].Id);
        Assert.AreEqual("KWA5516", cars[0].Registration);
        Assert.AreEqual("Mazda", cars[0].Mark);
        Assert.AreEqual("CX-5", cars[0].Model);
        Assert.AreEqual(2016, cars[0].Year);
        Assert.AreEqual("BF9MR42V8EBR67858", cars[0].VIN);
    }
    
    [TestMethod]
    public void Get_WithNonExistingRegistrationFilter_ReturnsNoCars() {
        // given
        var filter = new CarsFilter() { Registration = "KRK777" };
        // when
        List<Car> cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }
    
    [TestMethod]
    public void Get_WithMarkFilter_ReturnsFilteredCars() {
        // given
        var filter = new CarsFilter() { Mark = "Mazda" };
        // when
        List<Car> cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(1, cars.Count);
        Assert.AreEqual("0", cars[0].Id);
        Assert.AreEqual("KWA5516", cars[0].Registration);
        Assert.AreEqual("Mazda", cars[0].Mark);
        Assert.AreEqual("CX-5", cars[0].Model);
        Assert.AreEqual(2016, cars[0].Year);
        Assert.AreEqual("BF9MR42V8EBR67858", cars[0].VIN);
    }
    
    [TestMethod]
    public void Get_WithNonExistingMarkFilter_ReturnsNoCars() {
        // given
        var filter = new CarsFilter() { Mark = "BatmanCar" };
        // when
        List<Car> cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }
    
    [TestMethod]
    public void Get_WithModelFilter_ReturnsFilteredCars() {
        // given
        var filter = new CarsFilter() { Model = "CX-5" };
        // when
        List<Car> cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(1, cars.Count);
        Assert.AreEqual("0", cars[0].Id);
        Assert.AreEqual("KWA5516", cars[0].Registration);
        Assert.AreEqual("Mazda", cars[0].Mark);
        Assert.AreEqual("CX-5", cars[0].Model);
        Assert.AreEqual(2016, cars[0].Year);
        Assert.AreEqual("BF9MR42V8EBR67858", cars[0].VIN);
    }
    
    [TestMethod]
    public void Get_WithNonExistingModelFilter_ReturnsNoCars() {
        // given
        var filter = new CarsFilter() { Model = "UJ" };
        // when
        List<Car> cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }
    
    [TestMethod]
    public void Get_WithYearFilter_ReturnsFilteredCars() {
        // given
        var filter = new CarsFilter() { Year = 2016 };
        // when
        List<Car> cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(1, cars.Count);
        Assert.AreEqual("0", cars[0].Id);
        Assert.AreEqual("KWA5516", cars[0].Registration);
        Assert.AreEqual("Mazda", cars[0].Mark);
        Assert.AreEqual("CX-5", cars[0].Model);
        Assert.AreEqual(2016, cars[0].Year);
        Assert.AreEqual("BF9MR42V8EBR67858", cars[0].VIN);
    }
    
    [TestMethod]
    public void Get_WithNonExistingYearFilter_ReturnsNoCars() {
        // given
        var filter = new CarsFilter() { Year = 2077 };
        // when
        List<Car> cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }
    
    [TestMethod]
    public void Get_WithVinFilter_ReturnsFilteredCars() {
        // given
        var filter = new CarsFilter() { VIN = "BF9MR42V8EBR67858" };
        // when
        List<Car> cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(1, cars.Count);
        Assert.AreEqual("0", cars[0].Id);
        Assert.AreEqual("KWA5516", cars[0].Registration);
        Assert.AreEqual("Mazda", cars[0].Mark);
        Assert.AreEqual("CX-5", cars[0].Model);
        Assert.AreEqual(2016, cars[0].Year);
        Assert.AreEqual("BF9MR42V8EBR67858", cars[0].VIN);
    }
    
    [TestMethod]
    public void Get_WithNonExistingVinFilter_ReturnsNoCars() {
        // given
        var filter = new CarsFilter() { VIN = "000000111111" };
        // when
        List<Car> cars = _carsRepository.Get(filter);
        // then
        Assert.IsNotNull(cars);
        Assert.AreEqual(0, cars.Count);
    }
}