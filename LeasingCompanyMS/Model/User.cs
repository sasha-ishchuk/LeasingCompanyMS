using System.Text.Json.Serialization;

namespace LeasingCompanyMS.Model;

public record User {
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("email")] public string Email { get; set; }
    [JsonPropertyName("first_name")] public string FirstName { get; set; }
    [JsonPropertyName("last_name")] public string LastName { get; set; }
    [JsonPropertyName("phone_number")] public string PhoneNumber { get; set; }
    [JsonPropertyName("address_line1")] public string AddressLine1 { get; set; }
    [JsonPropertyName("address_line2")] public string AddressLine2 { get; set; }
    [JsonPropertyName("city")] public string City { get; set; }
    [JsonPropertyName("state")] public string State { get; set; }
    [JsonPropertyName("zipcode")] public string ZipCode { get; set; }
    [JsonPropertyName("country")] public string Country { get; set; }
    [JsonPropertyName("company_name")] public string CompanyName { get; set; }
    [JsonPropertyName("tax_id")] public string TaxId { get; set; }
    [JsonPropertyName("username")] public string Username { get; set; }
    [JsonPropertyName("password")] public string Password { get; set; }
    [JsonPropertyName("role")] public string Role { get; set; }
}