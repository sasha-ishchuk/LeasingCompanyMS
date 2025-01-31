using System.IO;
using System.Text.Json;
using LeasingCompanyMS.Model;

namespace LeasingCompanyMS.Utils;

public static class JsonUtils {
    public static string ReadFromJson(string jsonFilePath) {
        if (string.IsNullOrEmpty(jsonFilePath)) throw new ArgumentNullException(nameof(jsonFilePath));
        return File.ReadAllText(jsonFilePath);
    }

    public static List<User> MapJsonStringToUserList(string jsonString) {
        List<User>? users = JsonSerializer.Deserialize<List<User>>(jsonString);
        return users ?? new List<User>();
    }

    public static List<Car> MapJsonStringToCarList(string jsonString) {
        List<Car>? cars = JsonSerializer.Deserialize<List<Car>>(jsonString);
        return cars ?? new List<Car>();
    }

    public static List<Leasing> MapJsonStringToLeasingList(string jsonString) {
        List<Leasing>? leasings = JsonSerializer.Deserialize<List<Leasing>>(jsonString)!;
        return leasings ?? new List<Leasing>();
    }
}