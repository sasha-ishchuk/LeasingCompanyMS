using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LeasingCompanyMS.Utils;

namespace LeasingCompanyMS.Model
{
    public class UserRepository : IUserRepository
    {
        private readonly JsonUtils _jsonUtils;

        public UserRepository()
        {
            _jsonUtils = new JsonUtils();
        }

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
            string jsonString = _jsonUtils.ReadFromJson(GetPathToUsersJson());
            List<User> users = _jsonUtils.MapJsonStringToUserList(jsonString);
            if (users.Count == 0)
            {
                throw new Exception("No users found");
            }
            return users;
        }

        private string GetPathToUsersJson()
        {
            string? projectPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            if (projectPath == null)
            {
                throw new Exception("Project path not found");
            }
            return Path.Combine(projectPath, "Json", "users.json");
        }
    }
}
