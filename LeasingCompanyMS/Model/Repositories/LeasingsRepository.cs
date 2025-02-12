using System.IO;
using LeasingCompanyMS.Utils;
using static System.Guid;

namespace LeasingCompanyMS.Model.Repositories;

using ILeasingsRepository = IRepository<Leasing, string, LeasingsFilter>;

public class LeasingsRepository : ILeasingsRepository {
    private readonly List<Leasing> _leasings;
    private readonly string _leasingsFilePath;

    public LeasingsRepository(string leasingsFilePath = "Json/leasings.json") {
        _leasingsFilePath = leasingsFilePath;

        var leasingsJson = File.ReadAllText(_leasingsFilePath);
        _leasings = JsonUtils.MapJsonStringToLeasingList(leasingsJson);
    }

    private void UpdateLeasingsFileContents() {
        var stringifiedJsonLeasings = JsonUtils.MapLeasingListToJsonString(_leasings);
        File.WriteAllText(_leasingsFilePath, stringifiedJsonLeasings);
    }

    public String Add(Leasing leasing) {
        leasing.Id = NewGuid().ToString();
        _leasings.Add(leasing);
        UpdateLeasingsFileContents();
        return leasing.Id;
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
        
        UpdateLeasing(leasing, updatedLeasing);
        UpdateLeasingsFileContents();

        return true;
    }
    
    private void UpdateLeasing(Leasing leasing, Leasing updatedLeasing) {
        leasing.UserId = updatedLeasing.UserId;
        leasing.CarId = updatedLeasing.CarId;
        leasing.LeasingDurationInMonths = updatedLeasing.LeasingDurationInMonths;
        leasing.DownPayment = updatedLeasing.DownPayment;
        leasing.ResidualValue = updatedLeasing.ResidualValue;
        leasing.YearlyMileageLimit = updatedLeasing.YearlyMileageLimit;
        leasing.MonthlyLeaseInstallment = updatedLeasing.MonthlyLeaseInstallment;
        leasing.From = updatedLeasing.From;
        leasing.To = updatedLeasing.To;
        leasing.Conditions = updatedLeasing.Conditions;
    }
}