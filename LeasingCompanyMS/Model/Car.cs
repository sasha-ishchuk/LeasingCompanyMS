namespace LeasingCompanyMS.Model;

public record Car {
    public string Id { get; set; }
    public string Registration { get; set; }
    public string Mark { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string VIN { get; set; }

    public override string ToString() {
        return string.Join(" ", Year, Mark, Model);
    }
}