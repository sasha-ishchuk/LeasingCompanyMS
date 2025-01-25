using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeasingCompanyMS.Utils;

namespace LeasingCompanyMS.Model
{
    public static class LeasingsRepository
    {
        private static readonly JsonUtils _jsonUtils = new JsonUtils();


        public static List<Leasing> GetAll()
        {
            string jsonString = _jsonUtils.ReadFromJson(GetPathToLeasingsJson());
            List<Leasing> leasings = _jsonUtils.MapJsonStringToLeasingList(jsonString);
            if (leasings.Count == 0)
            {
                return new List<Leasing>();
            }
            return leasings;
        }

        private static string GetPathToLeasingsJson()
        {
            string? projectPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            if (projectPath == null)
            {
                throw new Exception("Project path not found");
            }
            return Path.Combine(projectPath, "Json", "leasings.json");
        }
    }
}
