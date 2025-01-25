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
        private static List<Leasing> leasings;


        public static List<Leasing> GetAll()
        {
            if (leasings == null)
            {
                string jsonString = _jsonUtils.ReadFromJson(GetPathToLeasingsJson());
                leasings = _jsonUtils.MapJsonStringToLeasingList(jsonString);
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

        public static List<Leasing> getUserLeasings(string userId)
        {
            return GetAll().Where(l => l.User ==  userId).ToList();
        }

        public static List<Leasing> getActiveUserLeasings(string userId)
        {
            return getUserLeasings(userId).Union(getActiveLeasings()).ToList();
        }

        public static List<Leasing> getActiveLeasings()
        {
            return GetAll().Where(l => l.IsActive()).ToList();
        }
    }
}
