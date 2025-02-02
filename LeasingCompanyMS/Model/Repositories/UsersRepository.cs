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

    public List<User> GetAll() {
        return Users;
    }

    public List<User> Get(UsersFilter usersFilter) {
        return Users.FindAll(user => {
            return (usersFilter.Id == null || user.Id == usersFilter.Id)
                && (usersFilter.Password == null || user.Password == usersFilter.Password)
                   && (usersFilter.Username == null || user.Username == usersFilter.Username)
                && (usersFilter.Role == null || user.Role == usersFilter.Role);
        });
    }

    public User? GetById(string id) {
        return Get(new UsersFilter() {Id = id}).FirstOrDefault();
    }
}