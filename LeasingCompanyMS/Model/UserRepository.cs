using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LeasingCompanyMS.Model
{
    public class UserRepository : IUserRepository
    {
        public bool AuthenticateUser(string username, string password)
        {
            List<User> users = GetAll();
            foreach (var user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public User GetByUsername(string username)
        {
            List<User> users = GetAll();
            foreach (var user in users)
            {
                if (user.Username == username)
                {
                    return user;
                }
            }
            throw new Exception("User not found");
        }

        public String GetRoleByUsername(string username)
        {
            List<User> users = GetAll();
            foreach (var user in users)
            {
                if (user.Username == username)
                {
                    return user.Role;
                }
            }
            throw new Exception("User not found");
        }

        public List<User> GetAll()
        {
            List<User> users = ReadUsersFromJson();
            if (users.Count == 0)
            {
                throw new Exception("No users found");
            }
            return users;
        }

        private List<User> ReadUsersFromJson()
        {
            string? projectPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            if (projectPath == null)
            {
                throw new Exception("Project path not found");
            }
            string jsonFilePath = Path.Combine(projectPath, "Json", "users.json");
            string jsonData = File.ReadAllText(jsonFilePath);
            List<User>? users = JsonSerializer.Deserialize<List<User>>(jsonData);
            return users ?? [];
        }
    }
}
