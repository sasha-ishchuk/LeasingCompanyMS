using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LeasingCompanyMS.Model;

namespace LeasingCompanyMS.Utils
{
    public class JsonUtils
    {
        public virtual string ReadFromJson(string jsonFilePath)
        {
            if (string.IsNullOrEmpty(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath));
            }
            return File.ReadAllText(jsonFilePath);
        }

        public List<User> MapJsonStringToUserList(string jsonString)
        {
            List<User>? users = JsonSerializer.Deserialize<List<User>>(jsonString);
            return users ?? new List<User>();
        }

        public List<Car> MapJsonStringToCarList(string jsonString)
        {
            List<Car>? cars = JsonSerializer.Deserialize<List<Car>>(jsonString);
            return cars ?? new List<Car>();
        }
    }
}
