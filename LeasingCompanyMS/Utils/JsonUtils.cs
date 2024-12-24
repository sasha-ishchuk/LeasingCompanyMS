using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LeasingCompanyMS.Model;

namespace LeasingCompanyMS.Utils
{
    public class JsonUtils
    {
        public virtual string ReadUsersFromJson(string jsonFilePath)
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
    }
}
