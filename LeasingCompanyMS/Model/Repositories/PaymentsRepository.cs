using System.IO;
using LeasingCompanyMS.Utils;
using static System.Guid;

namespace LeasingCompanyMS.Model.Repositories;

using IPaymentsRepository = IRepository<Payment, string, PaymentsFilter>;

public class PaymentsRepository : IPaymentsRepository {
    private readonly List<Payment> _payments;
    private readonly string _paymentsFilePath;

    public PaymentsRepository(string paymentsFilePath = "Json/payments.json") {
        _paymentsFilePath = paymentsFilePath;

        var paymentsJson = File.ReadAllText(_paymentsFilePath);
        _payments = JsonUtils.MapJsonStringToPaymentsList(paymentsJson);
    }

    private void UpdatePaymentsFileContents() {
        var stringifiedJsonPayments = JsonUtils.MapListToJsonString(_payments);
        File.WriteAllText(_paymentsFilePath, stringifiedJsonPayments);
    }

    public void Add(Payment payment) {
        payment.Id = NewGuid().ToString();
        _payments.Add(payment);
        UpdatePaymentsFileContents();
    }

    public Payment? GetById(string id) {
        return Get(new PaymentsFilter { Id = id }).FirstOrDefault();
    }

    public List<Payment> GetAll() {
        return _payments;
    }

    public List<Payment> Get(PaymentsFilter paymentsFilter) {
        return _payments.If(paymentsFilter.Id != null, payments => payments.Where(p => paymentsFilter.Id == p.Id))
            .If(paymentsFilter.UserId != null, payments => payments.Where(p => paymentsFilter.UserId == p.UserId))
            .If(paymentsFilter.CarId != null, payments => payments.Where(p => paymentsFilter.CarId == p.CarId))
            .If(paymentsFilter.LeasingId != null, payments => payments.Where(p => paymentsFilter.LeasingId == p.LeasingId))
            .If(paymentsFilter.NetAmount != null,payments => payments.Where(p => paymentsFilter.NetAmount == p.NetAmount))
            .If(paymentsFilter.GrossAmount != null,payments => payments.Where(p => paymentsFilter.GrossAmount == p.GrossAmount))
            .If(paymentsFilter.IssueDate != null,payments => payments.Where(p => paymentsFilter.IssueDate == p.IssueDate))
            .If(paymentsFilter.DueDate != null, payments => payments.Where(p => paymentsFilter.DueDate == p.DueDate))
            .ToList();
    }
    
    public bool UpdateById(string id, Payment updatedPayment) {
        var payment = _payments.Find(c => c.Id == id);
        if (payment == null) {
            return false;
        }
        
        payment = updatedPayment;
        UpdatePaymentsFileContents();

        return true;
    }
}