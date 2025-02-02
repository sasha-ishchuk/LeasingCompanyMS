namespace LeasingCompanyMS.Model;

public record Leasing {
    public string Id { get; set; }
    public string UserId { get; set; }
    public string CarId { get; set; }
    public int LeasingDurationInMonths { get; set; }

    public double DownPayment { get; set; }

    public int YearlyMileageLimit { get; set; }

    public double MonthlyLeaseInstallment { get; set; }

    public DateTime From { get; set; }
    public DateTime To { get; set; }

    public string Conditions { get; set; } //todo requirements

    public bool IsActive() {
        return DateTime.Now > From && DateTime.Now < To;
    }

    public override string ToString() {
        return $"{Id} - {From} - {To}";
    }
}