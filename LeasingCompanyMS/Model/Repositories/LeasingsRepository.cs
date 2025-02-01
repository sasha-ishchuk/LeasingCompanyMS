using System.IO;
using LeasingCompanyMS.Utils;
using static System.Guid;

namespace LeasingCompanyMS.Model.Repositories;

using ILeasingsRepository = IRepository<Leasing, string, LeasingsFilter>;

public class LeasingsRepository : ILeasingsRepository {
    private readonly List<Leasing> _leasings;
    private readonly string _leasingsFilePath;

    public LeasingsRepository(string leasingsFilePath = "leasings.json") {
        _leasingsFilePath = leasingsFilePath;

        var leasingsJson = File.ReadAllText(_leasingsFilePath);
        _leasings = JsonUtils.MapJsonStringToLeasingList(leasingsJson);
    }

    private void UpdateLeasingsFileContents() {
        var stringifiedJsonLeasings = JsonUtils.MapLeasingListToJsonString(_leasings);
        File.WriteAllText(_leasingsFilePath, stringifiedJsonLeasings);
    }

    public void Add(Leasing leasing) {
        leasing.Id = NewGuid().ToString();
        _leasings.Add(leasing);
        UpdateLeasingsFileContents();
    }

    public Leasing? GetById(string id) {
        return Get(new LeasingsFilter { Id = id }).FirstOrDefault();
    }

    public List<Leasing> GetAll() {
        return _leasings;
    }

    public List<Leasing> Get(LeasingsFilter leasingsFilter) {
        return _leasings.FindAll(leasing => {
            return (leasingsFilter.Id == null || leasingsFilter.Id == leasing.Id)
                   && (leasingsFilter.IsActive == null || leasingsFilter.IsActive == leasing.IsActive())
                   && (leasingsFilter.CarId == null || leasingsFilter.CarId == leasing.CarId);
        });
    }

    private static string GetPathToLeasingsJson() {
        var projectPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
        if (projectPath == null) throw new Exception("Project path not found");
        return Path.Combine(projectPath, "Json", "leasings.json");
    }
}