﻿namespace LeasingCompanyMS.Pages;

public record YearlyMileageLimitOption {
    public int Limit { get; init; }
    public double Coefficient { get; init; }
}