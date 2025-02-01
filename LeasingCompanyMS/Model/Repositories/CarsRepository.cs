using System.IO;
using LeasingCompanyMS.Utils;
using static System.Guid;

namespace LeasingCompanyMS.Model.Repositories;

using ICarsRepository = IRepository<Car, string, CarsFilter>;

public class CarsRepository : ICarsRepository {
    private readonly List<Car> _cars;
    private readonly string _carsFilePath;

    public CarsRepository(string carsFilePath = "cars.json") {
        _carsFilePath = carsFilePath;

        var carsJson = File.ReadAllText(_carsFilePath);
        _cars = JsonUtils.MapJsonStringToCarList(carsJson);
    }

    private void UpdateCarsFileContents() {
        var stringifiedJsonCars = JsonUtils.MapCarListToJsonString(_cars);
        File.WriteAllText(_carsFilePath, stringifiedJsonCars);
    }

    public Car? GetById(string id) {
        return Get(new CarsFilter { Id = id }).First();
    }

    public List<Car> GetAll() {
        return _cars;
    }

    public void Add(Car car) {
        car.Id = NewGuid().ToString();
        _cars.Add(car);
        UpdateCarsFileContents();
    }

    // INFO(piotr.klosowski): This function is BIG OOF. Kill it with fire.
    public List<Car> Get(CarsFilter carsFilter) {
        return _cars.Where(car => {
            return (carsFilter.Id == null || car.Id == carsFilter.Id) &&
                   (carsFilter.Brand == null || car.Brand == carsFilter.Brand) &&
                   (carsFilter.Model == null || car.Model == carsFilter.Model) &&
                   (carsFilter.ProductionYear == null || car.ProductionYear == carsFilter.ProductionYear) &&
                   (carsFilter.RegistrationNumber == null || car.RegistrationNumber == carsFilter.RegistrationNumber) &&
                   (carsFilter.BodyColor == null || car.BodyColor == carsFilter.BodyColor) &&
                   (carsFilter.Engine == null || (
                       (carsFilter.Engine!.Value.Displacement == null || car.Engine.Displacement == carsFilter.Engine!.Value.Displacement) &&
                       (carsFilter.Engine!.Value.Type == null || car.Engine.Type == carsFilter.Engine!.Value.Type) &&
                       (carsFilter.Engine!.Value.Power == null || car.Engine.Power == carsFilter.Engine!.Value.Power)
                   )) &&
                   (carsFilter.Vin == null || car.Vin == carsFilter.Vin) &&
                   (carsFilter.Packages == null || car.Packages == carsFilter.Packages) &&
                   (carsFilter.EstimatedNetValue == null || car.EstimatedNetValue == carsFilter.EstimatedNetValue) &&
                   (carsFilter.Status == null || car.Status == carsFilter.Status);
        }).ToList();
    }

    public bool UpdateById(string id, Car updatedCar) {
        var car = _cars.Find(c => c.Id == id);
        if (car == null) return false;

        car = updatedCar;
        UpdateCarsFileContents();

        return true;
    }
}