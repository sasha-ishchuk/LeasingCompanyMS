namespace LeasingCompanyMS.Model;

public class LeasingTerms {
    public Car Car { get; set; }
    public double DownPayment { get; set; }
    public int LeasingDurationInMonths { get; set; }
    public int YearlyMileageLimit { get; set; }
    public double MonthlyLeaseInstallment { get; set; }

    public override string ToString() {
        return $"{Car}";
    }
}