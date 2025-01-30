using System.IO;
using LeasingCompanyMS.Utils;

namespace LeasingCompanyMS.Model.Repositories;

using IUsersRepository = IRepository<User, string, UsersFilter>;

public class UsersRepository : IUsersRepository {
    private static readonly string UsersJson = JsonUtils.ReadFromJson(GetPathToUsersJson());
    private static readonly List<User> Users = JsonUtils.MapJsonStringToUserList(UsersJson);

    private static string GetPathToUsersJson() {
        var projectPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
        if (projectPath == null) throw new Exception("Project path not found");
        return Path.Combine(projectPath, "Json", "users.json");
    }
    
    public bool AuthenticateUser(string username, string password) {
        List<User> users = GetAll();
        foreach (var user in users)
            if (user.Username == username && user.Password == password)
                return true;

        return false;
    }

    public List<User> GetAll() {
        return Users;
    }
    
    public List<User> Get(UsersFilter usersFilter) {
        return Users.FindAll(user => {
            return usersFilter.Id != null ? user.Id == usersFilter.Id : true
                && usersFilter.Username != null ? user.Username == usersFilter.Username : true
                && usersFilter.Password != null ? user.Password == usersFilter.Password : true
                && usersFilter.Role != null ? user.Role == usersFilter.Role : true;
        });
    }

    public User? GetById(string id) {
        return Get(new UsersFilter() {Id = id}).FirstOrDefault();
    }
}