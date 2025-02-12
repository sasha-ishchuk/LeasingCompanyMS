using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;

namespace LeasingCompanyMSTest.Model.Repositories;

[TestClass]
public class UsersRepositoryTest {
    private UsersRepository _usersRepository;
    private string _testFilePath;

    [TestInitialize]
    public void Setup() {
        _testFilePath = $"Json/users_test_{Guid.NewGuid()}.json";
        File.Copy("Json/users_test.json", _testFilePath);
        _usersRepository = new UsersRepository(_testFilePath);
    }

    [TestCleanup]
    public void Cleanup() {
        if (File.Exists(_testFilePath)) {
            File.Delete(_testFilePath);
        }
    }
        
    [TestMethod]
    public void GetAll_ReturnsAllUsers() {
        // given/when
        var users = _usersRepository.GetAll();
        // then
        Assert.IsNotNull(users);
        Assert.AreEqual(3, users.Count);
    }

    [TestMethod]
    public void GetById_WhenUserExists_ThenReturnsUser() {
        // given/when
        var user = _usersRepository.GetById("4f5f8a88-cb1f-43fb-8c4f-5b0197c2f028");
        // then
        Assert.IsNotNull(user);
        Assert.AreEqual("user", user.Username);
        Assert.AreEqual("user", user.Password);
        Assert.AreEqual("user", user.Role);
    }

    [TestMethod]
    public void GetById_WhenUserDoesNotExist_ThenReturnsNull() {
        // given/when
        var user = _usersRepository.GetById("nonexistingid");
        // then
        Assert.IsNull(user);
    }

    [TestMethod]
    public void Get_WithRoleFilter_ReturnsFilteredUsers() {
        // given
        var filter = new UsersFilter { Role = "admin" };
        // when
        var users = _usersRepository.Get(filter);
        // then
        Assert.IsNotNull(users);
        Assert.AreEqual(1, users.Count);
        Assert.AreEqual("admin", users[0].Username);
        Assert.AreEqual("admin", users[0].Password);
        Assert.AreEqual("admin", users[0].Role);
    }
        
    [TestMethod]
    public void Get_WithNonExistingRoleFilter_ReturnsNoUsers() {
        // given
        var filter = new UsersFilter { Role = "role" };
        // when
        var users = _usersRepository.Get(filter);
        // then
        Assert.IsNotNull(users);
        Assert.AreEqual(0, users.Count);
    }

    [TestMethod]
    public void Get_WithUsernameFilter_ReturnsFilteredUsers() {
        // given
        var filter = new UsersFilter { Username = "admin" };
        // when
        var users = _usersRepository.Get(filter);
        // then
        Assert.IsNotNull(users);
        Assert.AreEqual(1, users.Count);
        Assert.AreEqual("admin", users[0].Username);
        Assert.AreEqual("admin", users[0].Password);
        Assert.AreEqual("admin", users[0].Role);
    }
        
    [TestMethod]
    public void Get_WithNonExistingUsernameFilter_ReturnsNoUsers() {
        // given
        var filter = new UsersFilter { Username = "john_doe" };
        // when
        var users = _usersRepository.Get(filter);
        // then
        Assert.IsNotNull(users);
        Assert.AreEqual(0, users.Count);
    }

    [TestMethod]
    public void Get_WithPasswordFilter_ReturnsFilteredUsers() {
        // given
        var filter = new UsersFilter { Password = "admin" };
        // when
        var users = _usersRepository.Get(filter);
        // then
        Assert.IsNotNull(users);
        Assert.AreEqual(1, users.Count);
        Assert.AreEqual("admin", users[0].Username);
        Assert.AreEqual("admin", users[0].Password);
        Assert.AreEqual("admin", users[0].Role);
    }
        
    [TestMethod]
    public void Get_WithNonExistingPasswordFilter_ReturnsNoUsers() {
        // given
        var filter = new UsersFilter { Password = "hello_world" };
        // when
        var users = _usersRepository.Get(filter);
        // then
        Assert.IsNotNull(users);
        Assert.AreEqual(0, users.Count);
    }
        
    [TestMethod]
    public void UpdateById_WithExistingUserId_ReturnsTrue() {
        // given
        var userName = "test";
        var password = "test123";
        var role = "user";
        var realId = "ae6bfb62-9999-555-bf27-7a730541e00b";
        // when
        var updated = _usersRepository.UpdateById(realId, BuildUser(userName, password, role));
        // then
        Assert.IsTrue(updated);
        var updatedUser = _usersRepository.GetById(realId);
        Assert.IsNotNull(updatedUser);
        Assert.AreEqual(userName, updatedUser.Username);
        Assert.AreEqual(password, updatedUser.Password);
        Assert.AreEqual(role, updatedUser.Role);
    }
        
    [TestMethod]
    public void UpdateById_WithNonExistingUserId_ReturnsFalse() {
        // given / when
        var updated = _usersRepository.UpdateById("someRandomId", BuildUser("userName", "password", "role"));
        // then
        Assert.IsFalse(updated);
    }
        
    [TestMethod]
    public void AddUser_WithValidUser() {
        // given
        var userName = "test";
        var password = "test123";
        var role = "user";
        // when
        var id = _usersRepository.Add(BuildUser(userName, password, role));
        // then
        var user = _usersRepository.GetById(id);
        Assert.IsNotNull(user);
        Assert.AreEqual(userName, user.Username);
        Assert.AreEqual(password, user.Password);
        Assert.AreEqual(role, user.Role);
    }
        
    private User BuildUser(string username, string password, string role) {
        return new User {
            Email = "email@dot.com",
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "123456789",
            AddressLine1 = "Address 1",
            AddressLine2 = "Address 2",
            City = "City",
            State = "State",
            ZipCode = "12345",
            Country = "Country",
            CompanyName = "Company",
            TaxId = "TaxId",
            Username = username,
            Password = password,
            Role = role
        };
    }
}