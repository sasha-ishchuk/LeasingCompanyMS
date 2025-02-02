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
        return _cars.If(carsFilter.Brand != null, cars => cars.Where(c => carsFilter.Id == c.Id))
            .If(carsFilter.Brand != null, cars => cars.Where(c => carsFilter.Brand == c.Brand))
            .If(carsFilter.Model != null, cars => cars.Where(c => carsFilter.Model == c.Model))
            .If(carsFilter.ProductionYear != null,
                cars => cars.Where(c => carsFilter.ProductionYear == c.ProductionYear))
            .If(carsFilter.RegistrationNumber != null,
                cars => cars.Where(c => carsFilter.RegistrationNumber == c.RegistrationNumber))
            .If(carsFilter.BodyColor != null, cars => cars.Where(c => carsFilter.BodyColor == c.BodyColor))
            .If(carsFilter.Engine != null, cars => {
                return cars.If(carsFilter.Engine!.HasValue,
                        cars => cars.Where(c => carsFilter.Engine!.Value.Type == c.Engine.Type))
                    .If(carsFilter.Engine!.HasValue,
                        cars => cars.Where(c => carsFilter.Engine!.Value.Displacement == c.Engine.Displacement))
                    .If(carsFilter.Engine!.HasValue,
                        cars => cars.Where(c => carsFilter.Engine!.Value.Power == c.Engine.Power));
            })
            .If(carsFilter.Vin != null, cars => cars.Where(c => carsFilter.Vin == c.Vin))
            .If(carsFilter.Packages != null, cars => cars.Where(c => carsFilter.Packages == c.Packages))
            .If(carsFilter.EstimatedNetValue != null,
                cars => cars.Where(c => carsFilter.EstimatedNetValue == c.EstimatedNetValue)).ToList();
    }

    private static string GetPathToCarsJson() {
        var projectPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
        if (projectPath == null) throw new Exception("Project path not found");
        return Path.Combine(projectPath, "Json", "cars.json");
    }
}