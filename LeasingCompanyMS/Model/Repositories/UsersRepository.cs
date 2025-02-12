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

    public String Add(User user) {
        user.Id = NewGuid().ToString();
        _users.Add(user);

        UpdateUsersFileContents();
        return user.Id;
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
        
        UpdateUser(user, updatedUser);
        UpdateUsersFileContents();

        return true;
    }
    
    private void UpdateUser(User user, User updatedUser) {
        user.Email = updatedUser.Email;
        user.FirstName = updatedUser.FirstName;
        user.LastName = updatedUser.LastName;
        user.PhoneNumber = updatedUser.PhoneNumber;
        user.AddressLine1 = updatedUser.AddressLine1;
        user.AddressLine2 = updatedUser.AddressLine2;
        user.City = updatedUser.City;
        user.State = updatedUser.State;
        user.ZipCode = updatedUser.ZipCode;
        user.Country = updatedUser.Country;
        user.CompanyName = updatedUser.CompanyName;
        user.TaxId = updatedUser.TaxId;
        user.Username = updatedUser.Username;
        user.Password = updatedUser.Password;
        user.Role = updatedUser.Role;
    }
}