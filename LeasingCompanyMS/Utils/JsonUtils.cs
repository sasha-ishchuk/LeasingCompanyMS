using System.IO;
using System.Text.Json;
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

    public static string MapCarListToJsonString(List<Car> carList) {
        return JsonSerializer.Serialize(carList);
    }

    public static string MapLeasingListToJsonString(List<Leasing> leasingList) {
        return JsonSerializer.Serialize(leasingList);
    }
    
    public static string MapUserListToJsonString(List<User> userList) {
        return JsonSerializer.Serialize(userList);
    }
}