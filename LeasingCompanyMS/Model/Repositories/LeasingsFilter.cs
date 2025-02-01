namespace LeasingCompanyMS.Model.Repositories;

public class LeasingsFilter {
    public string? Id { get; set; }
    public string? UserId { get; set; }
    public string? CarId { get; set; }
    public bool? IsActive { get; set; }
}