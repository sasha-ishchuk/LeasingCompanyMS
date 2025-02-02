namespace LeasingCompanyMS.Model.Repositories;

public class CarsFilter {
    public string? Id { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int? ProductionYear { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? BodyColor { get; set; }
    public EngineFilter? Engine { get; set; }
    public string? Vin { get; set; }
    public List<string>? Packages { get; set; }
    public int? EstimatedNetValue { get; set; }
}