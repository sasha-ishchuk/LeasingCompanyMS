using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;

namespace LeasingCompanyMSTest.Model.Repositories;

[TestClass]
public class PaymentsRepositoryTest {
    private PaymentsRepository _paymentsRepository;
    private string _testFilePath;

    [TestInitialize]
    public void Setup() {
        _testFilePath = $"Json/payments_test_{Guid.NewGuid()}.json";
        File.Copy("Json/payments_test.json", _testFilePath);
        _paymentsRepository = new PaymentsRepository(_testFilePath);
    }

    [TestCleanup]
    public void Cleanup() {
        if (File.Exists(_testFilePath)) {
            File.Delete(_testFilePath);
        }
    }
    
    [TestMethod]
    public void GetAll_ReturnsAllPayments() {
        // given/when
        var leasings = _paymentsRepository.GetAll();
        // then
        Assert.IsNotNull(leasings);
        Assert.AreEqual(2, leasings.Count);
    }
    
    [TestMethod]
    public void GetById_WhenPaymentExists_ThenReturnsPayment() {
        // given/when
        var payment = _paymentsRepository.GetById("99a9fcdd-42b3-4df9-abff-1c70db3dcdc9");
        // then
        Assert.IsNotNull(payment);
        Assert.AreEqual(0, payment.OrdinalNumber);
        Assert.AreEqual("0", payment.UserId);
        Assert.AreEqual("d5a6d106-5bda-4e57-9df7-5ac35e12d8de", payment.CarId);
        Assert.AreEqual("405a30ed-6e93-4d31-9205-723639a2d310", payment.LeasingId);
        Assert.AreEqual(13648, payment.NetAmount);
        Assert.AreEqual(16787, payment.GrossAmount);
        Assert.AreEqual(StringToDateTime("2025-02-03T00:49:17.0790515+01:00"), payment.IssueDate);
        Assert.AreEqual(StringToDateTime("2025-02-03T00:49:17.0791269+01:00"), payment.DueDate);
        Assert.AreEqual(PaymentStatus.Overdue, payment.Status);
    }

    [TestMethod]
    public void GetById_WhenPaymentDoesNotExist_ThenReturnsNull() {
        // given/when
        var payment = _paymentsRepository.GetById("nonExistingId");
        // then
        Assert.IsNull(payment);
    }
    
    [TestMethod]
    public void Get_WithUserIdFilter_ReturnsFilteredPayments() {
        // given
        var filter = new PaymentsFilter() { UserId = "0" };
        // when
        var payments = _paymentsRepository.Get(filter);
        // then
        Assert.IsNotNull(payments);
        Assert.AreEqual(2, payments.Count);
    }
    
    [TestMethod]
    public void Get_WithNonExistingUserIdFilter_ReturnsNoPayments() {
        // given
        var filter = new PaymentsFilter() { UserId = "randomId" };
        // when
        var payments = _paymentsRepository.Get(filter);
        // then
        Assert.IsNotNull(payments);
        Assert.AreEqual(0, payments.Count);
    }
    
    [TestMethod]
    public void Get_WithCarIdFilter_ReturnsFilteredPayments() {
        // given
        var filter = new PaymentsFilter() { CarId = "d5a6d106-5bda-4e57-9df7-5ac35e12d8de" };
        // when
        var payments = _paymentsRepository.Get(filter);
        // then
        Assert.IsNotNull(payments);
        Assert.AreEqual(2, payments.Count);
    }
    
    [TestMethod]
    public void Get_WithNonExistingCarIdFilter_ReturnsNoPayments() {
        // given
        var filter = new PaymentsFilter() { CarId = "randomCarId" };
        // when
        var payments = _paymentsRepository.Get(filter);
        // then
        Assert.IsNotNull(payments);
        Assert.AreEqual(0, payments.Count);
    }
    
    [TestMethod]
    public void Get_WithLeasingIdFilter_ReturnsFilteredPayments() {
        // given
        var filter = new PaymentsFilter() { LeasingId = "405a30ed-6e93-4d31-9205-723639a2d310" };
        // when
        var payments = _paymentsRepository.Get(filter);
        // then
        Assert.IsNotNull(payments);
        Assert.AreEqual(2, payments.Count);
    }
    
    [TestMethod]
    public void Get_WithNonExistingLeasingIdFilter_ReturnsNoPayments() {
        // given
        var filter = new PaymentsFilter() { LeasingId = "randomId" };
        // when
        var payments = _paymentsRepository.Get(filter);
        // then
        Assert.IsNotNull(payments);
        Assert.AreEqual(0, payments.Count);
    }
    
    [TestMethod]
    public void Get_WithNetAmountFilter_ReturnsFilteredPayments() {
        // given
        var filter = new PaymentsFilter() { NetAmount = 13648};
        // when
        var payments = _paymentsRepository.Get(filter);
        // then
        Assert.IsNotNull(payments);
        Assert.AreEqual(2, payments.Count);
    }
    
    [TestMethod]
    public void Get_WithNonExistingNetAmountFilter_ReturnsNoPayments() {
        // given
        var filter = new PaymentsFilter() { NetAmount = 111111};
        // when
        var payments = _paymentsRepository.Get(filter);
        // then
        Assert.IsNotNull(payments);
        Assert.AreEqual(0, payments.Count);
    }
    
    [TestMethod]
    public void Get_WithGrossAmountFilter_ReturnsFilteredPayments() {
        // given
        var filter = new PaymentsFilter() { GrossAmount = 16787.4705};
        // when
        var payments = _paymentsRepository.Get(filter);
        // then
        Assert.IsNotNull(payments);
        Assert.AreEqual(1, payments.Count);
    }
    
    [TestMethod]
    public void Get_WithNonExistingGrossAmountFilter_ReturnsNoPayments() {
        // given
        var filter = new PaymentsFilter() { GrossAmount = 143.876};
        // when
        var payments = _paymentsRepository.Get(filter);
        // then
        Assert.IsNotNull(payments);
        Assert.AreEqual(0, payments.Count);
    }
    
    [TestMethod]
    public void Get_WithIssueDateFilter_ReturnsFilteredPayments() {
        // given
        var filter = new PaymentsFilter() { IssueDate = StringToDateTime("2025-02-03T00:49:17.0790515+01:00")};
        // when
        var payments = _paymentsRepository.Get(filter);
        // then
        Assert.IsNotNull(payments);
        Assert.AreEqual(1, payments.Count);
    }
    
    [TestMethod]
    public void Get_WithNonExistingIssueDateFilter_ReturnsNoPayments() {
        // given
        var filter = new PaymentsFilter() { IssueDate = StringToDateTime("2029-02-03T00:49:17.0790515+01:00")};
        // when
        var payments = _paymentsRepository.Get(filter);
        // then
        Assert.IsNotNull(payments);
        Assert.AreEqual(0, payments.Count);
    }
    
    [TestMethod]
    public void Get_WithDueDateFilter_ReturnsFilteredPayments() {
        // given
        var filter = new PaymentsFilter() { DueDate = StringToDateTime("2025-03-03T00:49:17.0816947+01:00")};
        // when
        var payments = _paymentsRepository.Get(filter);
        // then
        Assert.IsNotNull(payments);
        Assert.AreEqual(1, payments.Count);
    }
    
    [TestMethod]
    public void Get_WithNonExistingDueDateFilter_ReturnsNoPayments() {
        // given
        var filter = new PaymentsFilter() { DueDate = StringToDateTime("2019-02-03T00:49:17.0790515+01:00")};
        // when
        var payments = _paymentsRepository.Get(filter);
        // then
        Assert.IsNotNull(payments);
        Assert.AreEqual(0, payments.Count);
    }
    
    [TestMethod]
    public void UpdateById_WithExistingPaymentId_ReturnsTrue() {
        // given
        var userId = "userTest";
        var carId = "carTest";
        var leasingId = "leasingTest";
        var realId = "875eef4d-d47b-430b-b32d-d0e1a3596a3d";
        // when
        var updated = _paymentsRepository.UpdateById(realId, BuildPayment(userId, carId, leasingId));
        // then
        Assert.IsTrue(updated);
        var payment = _paymentsRepository.GetById(realId);
        Assert.IsNotNull(payment);
        Assert.AreEqual(userId, payment.UserId);
        Assert.AreEqual(carId, payment.CarId);
        Assert.AreEqual(leasingId, payment.LeasingId);
    }
    
    [TestMethod]
    public void UpdateById_WithNonExistingPaymentId_ReturnsFalse() {
        // given / when
        var updated = _paymentsRepository.UpdateById("randomId", BuildPayment("randomU", "randomC", "randomL"));
        // then
        Assert.IsFalse(updated);
    }
    
    [TestMethod]
    public void AddPayment_WithValidPayment() {
        // given
        var userId = "userTest";
        var carId = "carTest";
        var leasingId = "leasingTest";
        // when
        var id = _paymentsRepository.Add(BuildPayment(userId, carId, leasingId));
        // then
        var payment = _paymentsRepository.GetById(id);
        Assert.IsNotNull(payment);
        Assert.AreEqual(userId, payment.UserId);
        Assert.AreEqual(carId, payment.CarId);
        Assert.AreEqual(leasingId, payment.LeasingId);
    }

    private Payment BuildPayment(string userId, string carId, string leasingId) {
        return new Payment {
            OrdinalNumber = 0,
            UserId = userId,
            CarId = carId,
            LeasingId = leasingId,
            NetAmount = 1000,
            GrossAmount = 1230,
            IssueDate = StringToDateTime("2025-02-03T00:49:17.0790515+01:00"),
            DueDate = StringToDateTime("2025-03-03T00:49:17.0791269+01:00"),
            Status = PaymentStatus.Overdue
        };
    }
    
    private DateTime StringToDateTime(string dateTimeString) {
        return DateTime.Parse(dateTimeString);
    }
}