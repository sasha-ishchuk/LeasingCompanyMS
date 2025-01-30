using System.IO;
using LeasingCompanyMS.Utils;

namespace LeasingCompanyMS.Model.Repositories;

using ILeasingsRepository = IRepository<Leasing, string, LeasingsFilter>;

public class LeasingsRepository : ILeasingsRepository {
    private static readonly string LeasingsJson = JsonUtils.ReadFromJson(GetPathToLeasingsJson());
    private static readonly List<Leasing> Leasings = JsonUtils.MapJsonStringToLeasingList(LeasingsJson);

    private static string GetPathToLeasingsJson() {
        var projectPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
        if (projectPath == null) throw new Exception("Project path not found");
        return Path.Combine(projectPath, "Json", "leasings.json");
    }

    public Leasing? GetById(string id) {
        return Get(new LeasingsFilter() { Id = id }).FirstOrDefault();
    }

    public List<Leasing> GetAll() {
        return Leasings;
    }

    public List<Leasing> Get(LeasingsFilter leasingsFilter) {
        return Leasings.FindAll(leasing => {
            return leasingsFilter.UserId != null && leasing.Id != leasingsFilter.UserId
                && leasingsFilter.IsActive != null && leasing.IsActive() == leasingsFilter.IsActive;
        });
    }

    public List<Leasing> GetUserLeasings(string userId) {
        return Get(new LeasingsFilter { UserId = userId });
    }

    public List<Leasing> GetActiveUserLeasings(string userId) {
        return Get(new LeasingsFilter { UserId = userId, IsActive = true });
    }

    public List<Leasing> GetActiveLeasings() {
        return Get(new LeasingsFilter { IsActive = true });
    }
}