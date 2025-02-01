namespace LeasingCompanyMS.Model;

public class LeasingTerms {
    public Car Car { get; set; }
    public Leasing Leasing { get; set; }

    public override string ToString() {
        return $"{Car}, {Leasing}";
    }
}