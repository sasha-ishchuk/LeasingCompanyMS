﻿namespace LeasingCompanyMS.Model;

public record Leasing {
    public string Id { get; set; }
    public string User { get; set; }
    public string UserId { get; set; }
    public int CarId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    
    public string Conditions { get; set; } //todo requirements

    public bool IsActive() {
        return DateTime.Now > From && DateTime.Now < To;
    }
}