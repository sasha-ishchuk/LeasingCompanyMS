using LeasingCompanyMS.Model.Repositories;

namespace LeasingCompanyMSTest.Model.Repositories;

[TestClass]
public class LeasingsRepositoryTest {
    private readonly LeasingsRepository _leasingsRepository = new();
    
    [TestMethod]
    public void GetAll_ReturnsAllLeasings() {
        // given/when
        var leasings = _leasingsRepository.GetAll();
        // then
        Assert.IsNotNull(leasings);
        Assert.AreEqual(2, leasings.Count);
    }
    
    [TestMethod]
    public void GetById_WhenLeasingExists_ThenReturnsLeasing() {
        // given/when
        var leasing = _leasingsRepository.GetById("0");
        // then
        Assert.IsNotNull(leasing);
        Assert.AreEqual("user", leasing.User);
        Assert.AreEqual("1", leasing.UserId);
        Assert.AreEqual(0, leasing.CarId);
        Assert.AreEqual(DateTime.Parse("2024-12-01T00:00:00"), leasing.From);
        Assert.AreEqual(DateTime.Parse("2029-12-31T00:00:00"), leasing.To);
        Assert.AreEqual("Condition A", leasing.Conditions);
    }

    [TestMethod]
    public void GetById_WhenLeasingDoesNotExist_ThenReturnsNull() {
        // given/when
        var leasing = _leasingsRepository.GetById("nonexistingid");
        // then
        Assert.IsNull(leasing);
    }
    
    [TestMethod]
    public void Get_WithUserIdFilter_ReturnsFilteredLeasings() {
        // given
        var filter = new LeasingsFilter() { UserId = "1" };
        // when
        var leasings = _leasingsRepository.Get(filter);
        // then
        Assert.IsNotNull(leasings);
        Assert.AreEqual(1, leasings.Count);
        Assert.AreEqual("0", leasings[0].Id);
        Assert.AreEqual("user", leasings[0].User);
        Assert.AreEqual(0, leasings[0].CarId);
        Assert.AreEqual(DateTime.Parse("2024-12-01T00:00:00"), leasings[0].From);
        Assert.AreEqual(DateTime.Parse("2029-12-31T00:00:00"), leasings[0].To);
        Assert.AreEqual("Condition A", leasings[0].Conditions);
    }
    
    [TestMethod]
    public void Get_WithNonExistingUserIdFilter_ReturnsNoLeasings() {
        // given
        var filter = new LeasingsFilter() { UserId = "random" };
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
        Assert.AreEqual(1, leasings.Count);
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
    public void GetUserLeasings_WithUserHasLeasings_ReturnsLeasings() {
        // given/when
        var leasings = _leasingsRepository.GetUserLeasings("1");
        // then
        Assert.IsNotNull(leasings);
        Assert.AreEqual(1, leasings.Count);
    }
    
    [TestMethod]
    public void GetUserLeasings_WithUserDoesNotHaveLeasings_ReturnsNoLeasings() {
        // given/when
        var leasings = _leasingsRepository.GetUserLeasings("999");
        // then
        Assert.IsNotNull(leasings);
        Assert.AreEqual(0, leasings.Count);
    }
    
    [TestMethod]
    public void GetActiveUserLeasings_WithUserHasActiveLeasings_ReturnsLeasings() {
        // given/when
        var leasings = _leasingsRepository.GetActiveUserLeasings("1");
        // then
        Assert.IsNotNull(leasings);
        Assert.AreEqual(1, leasings.Count);
        Assert.IsTrue(leasings[0].IsActive());
    }
    
    [TestMethod]
    public void GetActiveUserLeasings_WithUserDoesNotHaveActiveLeasings_ReturnsNoLeasings() {
        // given/when
        var leasings = _leasingsRepository.GetActiveUserLeasings("123");
        // then
        Assert.IsNotNull(leasings);
        Assert.AreEqual(0, leasings.Count);
    }
    
    [TestMethod]
    public void GetActiveLeasings_ReturnsAllActiveLeasings() {
        // given/when
        var leasings = _leasingsRepository.GetActiveLeasings();
        // then
        Assert.IsNotNull(leasings);
        Assert.AreEqual(1, leasings.Count);
        Assert.IsTrue(leasings[0].IsActive());
    }
}