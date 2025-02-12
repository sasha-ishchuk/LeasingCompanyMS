using System.IO;
using LeasingCompanyMS.Utils;
using static System.Guid;

namespace LeasingCompanyMS.Model.Repositories;

using IUsersRepository = IRepository<User, string, UsersFilter>;

public class UsersRepository : IUsersRepository {
    private readonly List<User> _users;
    private readonly string _usersFilePath;

    public UsersRepository(string usersFilePath = "Json/users.json") {
        _usersFilePath = usersFilePath;

        var usersJson = File.ReadAllText(_usersFilePath);
        _users = JsonUtils.MapJsonStringToUserList(usersJson);
    }

    private void UpdateUsersFileContents() {
        var stringifiedJsonUsers = JsonUtils.MapUserListToJsonString(_users);
        File.WriteAllText(_usersFilePath, stringifiedJsonUsers);
    }

    public void Add(User user) {
        user.Id = NewGuid().ToString();
        _users.Add(user);

        UpdateUsersFileContents();
    }

    public List<User> GetAll() {
        return _users;
    }

    public List<User> Get(UsersFilter usersFilter) {
        return _users.FindAll(user => {
            return (usersFilter.Id == null || usersFilter.Id == user.Id)
                   && (usersFilter.Username == null || usersFilter.Username == user.Username)
                   && (usersFilter.Password == null || usersFilter.Password == user.Password)
                   && (usersFilter.Role == null || usersFilter.Role == user.Role);
        });
    }

    public User? GetById(string id) {
        return Get(new UsersFilter { Id = id }).FirstOrDefault();
    }
    
    public bool UpdateById(string id, User updatedUser) {
        var user = _users.Find(c => c.Id == id);
        if (user == null) {
            return false;
        }
        
        user = updatedUser;
        UpdateUsersFileContents();

        return true;
    }
}