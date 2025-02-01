using System.IO;
using LeasingCompanyMS.Utils;

namespace LeasingCompanyMS.Model.Repositories;

using ILeasingsRepository = IRepository<Leasing, string, LeasingsFilter>;

public class LeasingsRepository : ILeasingsRepository {
    private static List<Leasing> _leasings = LoadLeasings();

    public void Add(Leasing leasing) {
        _leasings.Add(leasing);
        JsonUtils.WriteToJson(JsonUtils.MapLeasingListToJsonString(_leasings), GetPathToLeasingsJson());
        _leasings = LoadLeasings();
    }

    public Leasing? GetById(string id) {
        return Get(new LeasingsFilter { Id = id }).FirstOrDefault();
    }

    public List<Leasing> GetAll() {
        return _leasings;
    }

    public List<Leasing> Get(LeasingsFilter leasingsFilter) {
        return _leasings.FindAll(leasing => {
            return leasingsFilter.UserId != null && leasing.Id != leasingsFilter.UserId 
                && leasingsFilter.IsActive != null && leasing.IsActive() == leasingsFilter.IsActive
                && leasingsFilter.CarId != null && leasing.CarId == leasingsFilter.CarId;
        });
    }

    private static List<Leasing> LoadLeasings() {
        var leasingsJson = JsonUtils.ReadFromJson(GetPathToLeasingsJson());
        return JsonUtils.MapJsonStringToLeasingList(leasingsJson);
    }

    private static string GetPathToLeasingsJson() {
        var projectPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
        if (projectPath == null) throw new Exception("Project path not found");
        return Path.Combine(projectPath, "Json", "leasings.json");
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