using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeasingCompanyMS.Utils;

namespace LeasingCompanyMS.Model
{
    public class CarsRepository
    {
        private readonly JsonUtils _jsonUtils;

        public CarsRepository()
        {
            _jsonUtils = new JsonUtils();

        }

        public List<Car> GetAll()
        {
            string jsonString = _jsonUtils.ReadFromJson(GetPathToCarsJson());
            List<Car> cars = _jsonUtils.MapJsonStringToCarList(jsonString);
            if (cars.Count == 0)
            {
                throw new Exception("No cars found");
            }
            return cars;
        }

        private string GetPathToCarsJson()
        {
            string? projectPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            if (projectPath == null)
            {
                throw new Exception("Project path not found");
            }
            return Path.Combine(projectPath, "Json", "cars.json");
        }
    }
}
