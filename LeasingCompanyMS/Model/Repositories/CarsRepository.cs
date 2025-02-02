using System.IO;
using LeasingCompanyMS.Utils;

namespace LeasingCompanyMS.Model.Repositories;

using ICarsRepository = IRepository<Car, string, CarsFilter>;

public class CarsRepository : ICarsRepository {
    private static readonly string CarsJson = JsonUtils.ReadFromJson(GetPathToCarsJson());
    private static readonly List<Car> Cars = JsonUtils.MapJsonStringToCarList(CarsJson);
    
    private static string GetPathToCarsJson() {
        var projectPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
        if (projectPath == null) throw new Exception("Project path not found");
        return Path.Combine(projectPath, "Json", "cars.json");
    }

    public Car? GetById(string id) {
        return Get(new CarsFilter() { Id = id }).FirstOrDefault();
    }

    public List<Car> GetAll() {
        return Cars;
    }

    public List<Car> Get(CarsFilter carsFilter) {
        return Cars.FindAll(car => {
            return (carsFilter.Id == null || car.Id == carsFilter.Id)
                   && (carsFilter.Registration == null || car.Registration == carsFilter.Registration)
                 && (carsFilter.Mark == null || car.Mark == carsFilter.Mark) 
                 && (carsFilter.Model == null || car.Model == carsFilter.Model)
                 && (carsFilter.Year == null || car.Year == carsFilter.Year)
                 && (carsFilter.VIN == null || car.VIN == carsFilter.VIN);
        });
    }
}
