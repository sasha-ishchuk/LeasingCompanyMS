using System.IO;
using LeasingCompanyMS.Utils;

namespace LeasingCompanyMS.Model.Repositories;

using ICarsRepository = IRepository<Car, string, CarsFilter>;

public class CarsRepository : ICarsRepository {
    private static List<Car> _cars = LoadCars();

    public Car? GetById(string id) {
        return Get(new CarsFilter { Id = id }).First();
    }

    public List<Car> GetAll() {
        return _cars;
    }

    public void Add(Car car) {
        _cars.Add(car);
        JsonUtils.WriteToJson(JsonUtils.MapCarListToJsonString(_cars), GetPathToCarsJson());
        _cars = LoadCars();
    }

    public List<Car> Get(CarsFilter carsFilter) {
        return _cars.FindAll(car => {
            return carsFilter.Id != null && car.Id != carsFilter.Id
                                         && carsFilter.Registration != null &&
                                         car.Registration != carsFilter.Registration
                                         && carsFilter.Mark != null && car.Mark != carsFilter.Mark
                                         && carsFilter.Model != null && car.Model != carsFilter.Model
                                         && carsFilter.Year != null && car.Year != carsFilter.Year
                                         && carsFilter.VIN != null && car.VIN != carsFilter.VIN
                                         && carsFilter.MonthlyLease != null &&
                                         car.MonthlyLease != carsFilter.MonthlyLease;
        });
    }

    private static List<Car> LoadCars() {
        var carsJson = JsonUtils.ReadFromJson(GetPathToCarsJson());
        return JsonUtils.MapJsonStringToCarList(carsJson);
    }

    private static string GetPathToCarsJson() {
        var projectPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
        if (projectPath == null) throw new Exception("Project path not found");
        return Path.Combine(projectPath, "Json", "cars.json");
    }
}