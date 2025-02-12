using System.Text.Json.Serialization;

namespace LeasingCompanyMS.Model;

public record Car {
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("brand")] public string Brand { get; set; }
    [JsonPropertyName("model")] public string Model { get; set; }
    [JsonPropertyName("production_year")] public int ProductionYear { get; set; }

    [JsonPropertyName("registration_number")]
    public string? RegistrationNumber { get; set; }

    [JsonPropertyName("body_color")] public string BodyColor { get; set; }
    [JsonPropertyName("engine")] public Engine Engine { get; set; }
    [JsonPropertyName("vin")] public string Vin { get; set; }
    [JsonPropertyName("packages")] public List<string> Packages { get; set; }

    [JsonPropertyName("estimated_net_value")]
    public int EstimatedNetValue { get; set; }

    [JsonPropertyName("status")] public CarStatus Status { get; set; }
}