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
    
    public bool UpdateById(string id, Leasing updatedLeasing) {
        var leasing = _leasings.Find(c => c.Id == id);
        if (leasing == null) {
            return false;
        }
        
        leasing = updatedLeasing;
        UpdateLeasingsFileContents();

        return true;
    }
}