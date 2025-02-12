namespace LeasingCompanyMS.Model;

public record Payment {
    public string Id { get; set; }
    public int OrdinalNumber { get; set; }
    public string UserId { get; set; }
    public string CarId { get; set; }
    public string LeasingId { get; set; }
    public double NetAmount { get; set; }
    public double GrossAmount { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public PaymentStatus Status { get; set; }
}