using System.IO;
using System.Text.Json;
using System.Xml;
using LeasingCompanyMS.Model;

namespace LeasingCompanyMS.Utils;

public static class JsonUtils {
    public static string ReadFromJson(string jsonFilePath) {
        if (string.IsNullOrEmpty(jsonFilePath)) throw new ArgumentNullException(nameof(jsonFilePath));
        return File.ReadAllText(jsonFilePath);
    }

    public static void WriteToJson(string jsonString, string jsonFilePath) {
        if (string.IsNullOrEmpty(jsonFilePath)) throw new ArgumentNullException(nameof(jsonFilePath));

        File.WriteAllText(jsonFilePath, jsonString);
    }

    public static List<User> MapJsonStringToUserList(string jsonString) {
        var users = JsonSerializer.Deserialize<List<User>>(jsonString);
        return users ?? new List<User>();
    }

    public static List<Car> MapJsonStringToCarList(string jsonString) {
        var cars = JsonSerializer.Deserialize<List<Car>>(jsonString);
        return cars ?? new List<Car>();
    }

    public static List<Leasing> MapJsonStringToLeasingList(string jsonString) {
        var leasings = JsonSerializer.Deserialize<List<Leasing>>(jsonString)!;
        return leasings ?? new List<Leasing>();
    }

    public static List<Payment> MapJsonStringToPaymentsList(string jsonString) {
        var payments = JsonSerializer.Deserialize<List<Payment>>(jsonString)!;
        return payments ?? new List<Payment>();
    }
    
    public static string MapListToJsonString<T>(List<T> carList) {
        return JsonSerializer.Serialize(carList, new JsonSerializerOptions() {
            WriteIndented = true,
        });
    }
    
    public static string MapCarListToJsonString(List<Car> carList) {
        return JsonSerializer.Serialize(carList, new JsonSerializerOptions() {
            WriteIndented = true,
        });
    }

    public static string MapLeasingListToJsonString(List<Leasing> leasingList) {
        return JsonSerializer.Serialize(leasingList, new JsonSerializerOptions() {
            WriteIndented = true,
        });
    }
    
    public static string MapUserListToJsonString(List<User> userList) {
        return JsonSerializer.Serialize(userList, new JsonSerializerOptions() {
            WriteIndented = true,
        });
    }
}