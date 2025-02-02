using System.Text.Json.Serialization;

namespace LeasingCompanyMS.Model;

public record Engine {
    [JsonPropertyName("type")] public string Type { get; set; }
    [JsonPropertyName("displacement")] public string Displacement { get; set; }
    [JsonPropertyName("power")] public string Power { get; set; }
}