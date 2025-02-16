using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;

namespace LeasingCompanyMSTest.Model.Repositories;

[TestClass]
public class LeasingsRepositoryTest {
    private LeasingsRepository _leasingsRepository;
    private string _testFilePath;

    [TestInitialize]
    public void Setup() {
        _testFilePath = $"Json/leasings_test_{Guid.NewGuid()}.json";
        File.Copy("Json/leasings_test.json", _testFilePath);
        _leasingsRepository = new LeasingsRepository(_testFilePath);
    }

    [TestCleanup]
    public void Cleanup() {
        if (File.Exists(_testFilePath)) {
            File.Delete(_testFilePath);
        }
    }
    
    [TestMethod]
    public void GetAll_ReturnsAllLeasings() {
        // given/when
        var leasings = _leasingsRepository.GetAll();
        // then
        Assert.IsNotNull(leasings);
        Assert.AreEqual(3, leasings.Count);
    }
    
    [TestMethod]
    public void GetById_WhenLeasingExists_ThenReturnsLeasing() {
        // given/when
        var leasing = _leasingsRepository.GetById("7bf48f19-2369-4983-b8dd-ac2c0e7fbd7e");
        // then
        Assert.IsNotNull(leasing);
        Assert.AreEqual("0", leasing.UserId);
        Assert.AreEqual("e3fe5c51-e0fe-4752-bfd0-15e7cde62532", leasing.CarId);
        Assert.AreEqual(24, leasing.LeasingDurationInMonths);
        Assert.AreEqual(11880, leasing.DownPayment);
        Assert.AreEqual(0, leasing.ResidualValue);
        Assert.AreEqual(15000, leasing.YearlyMileageLimit);
        Assert.AreEqual(5653.89, leasing.MonthlyLeaseInstallment);
        Assert.AreEqual(StringToDateTime("2025-02-10T23:00:24.6741761+01:00"), leasing.From);
        Assert.AreEqual(StringToDateTime("2027-02-10T23:00:24.6742598+01:00"), leasing.To);
        Assert.AreEqual("Conditions Basic", leasing.Conditions);
    }

    [TestMethod]
    public void GetById_WhenLeasingDoesNotExist_ThenReturnsNull() {
        // given/when
        var leasing = _leasingsRepository.GetById("nonExistingId");
        // then
        Assert.IsNull(leasing);
    }
    
    [TestMethod]
    public void Get_WithCarIdFilter_ReturnsFilteredLeasings() {
        // given
        var filter = new LeasingsFilter() { CarId = "253af3b8-303e-42e9-9982-17baf5c20272" };
        // when
        var leasings = _leasingsRepository.Get(filter);
        // then
        Assert.IsNotNull(leasings);
        Assert.AreEqual(1, leasings.Count);
    }
    
    [TestMethod]
    public void Get_WithNonExistingCarIdFilter_ReturnsNoLeasings() {
        // given
        var filter = new LeasingsFilter() { CarId = "randomCarId" };
        // when
        var leasings = _leasingsRepository.Get(filter);
        // then
        Assert.IsNotNull(leasings);
        Assert.AreEqual(0, leasings.Count);
    }
    
    [TestMethod]
    public void Get_WithIsActiveFilter_ReturnsActiveLeasings() {
        // given
        var filter = new LeasingsFilter() { IsActive = true };
        // when
        var leasings = _leasingsRepository.Get(filter);
        // then
        Assert.IsNotNull(leasings);
        Assert.AreEqual(2, leasings.Count);
    }
    
    [TestMethod]
    public void Get_WithIsActiveFilter_ReturnsNonActiveLeasings() {
        // given
        var filter = new LeasingsFilter() { IsActive = false };
        // when
        var leasings = _leasingsRepository.Get(filter);
        // then
        Assert.IsNotNull(leasings);
        Assert.AreEqual(1, leasings.Count);
    }
    
    [TestMethod]
    public void UpdateById_WithExistingLeasingId_ReturnsTrue() {
        // given
        var userId = "userTest";
        var carId = "carTest";
        var realId = "7bf48f19-2369-4983-b8dd-ac2c0e7fbd7e";
        // when
        var updated = _leasingsRepository.UpdateById(realId, BuildLeasing(userId, carId));
        // then
        Assert.IsTrue(updated);
        var leasing = _leasingsRepository.GetById(realId);
        Assert.IsNotNull(leasing);
        Assert.AreEqual(userId, leasing.UserId);
        Assert.AreEqual(carId, leasing.CarId);
    }
    
    [TestMethod]
    public void UpdateById_WithNonExistingLeasingId_ReturnsFalse() {
        // given / when
        var updated = _leasingsRepository.UpdateById("randomId", BuildLeasing("randomU", "randomC"));
        // then
        Assert.IsFalse(updated);
    }
    
    [TestMethod]
    public void AddLeasing_WithValidLeasing() {
        // given
        var userId = "userTest";
        var carId = "carTest";
        // when
        var id = _leasingsRepository.Add(BuildLeasing(userId, carId));
        // then
        var leasing = _leasingsRepository.GetById(id);
        Assert.IsNotNull(leasing);
        Assert.AreEqual(userId, leasing.UserId);
        Assert.AreEqual(carId, leasing.CarId);
    }

    private Leasing BuildLeasing(string userId, string carId) {
        return new Leasing {
            UserId = userId,
            CarId = carId,
            LeasingDurationInMonths = 36,
            DownPayment = 12880,
            ResidualValue = 0,
            YearlyMileageLimit = 1234,
            MonthlyLeaseInstallment = 3333.89,
            From = StringToDateTime("2025-02-10T23:00:24.6741761+01:00"),
            To = StringToDateTime("2027-02-10T23:00:24.6742598+01:00"),
            Conditions = "Conditions Test"
        };
    }
    
    private DateTime StringToDateTime(string dateTimeString) {
        return DateTime.Parse(dateTimeString);
    }
}