using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.Utils;
using Moq;

namespace LeasingCompanyMSTest.Model
{
    [TestClass]
    public class UsersRepositoryTest
    {
        private readonly Mock<JsonUtils> _jsonUtilsMock = new();
        private readonly UsersRepository _usersRepository = new();

        [TestMethod]
        public void AuthenticateUser_WhenValidCredentials_ThenReturnsTrue()
        {
            // given
            var username = "user";
            var password = "user";
            // when
            var result = _usersRepository.AuthenticateUser(username, password);
            // then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AuthenticateUser_WhenInvalidCredentials_ThenReturnsFalse()
        {
            // given
            var username = "user1";
            var password = "user1";
            // when
            var result = _usersRepository.AuthenticateUser(username, password);
            // then
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetByUsername_WhenUserExists_ThenReturnsUser()
        {
            // given/when
            User user = _usersRepository.GetByUsername("user");
            // then
            Assert.IsNotNull(user);
            Assert.AreEqual("user", user.Username);
            Assert.AreEqual("user", user.Password);
            Assert.AreEqual("user", user.Role);
        }

        [TestMethod]
        public void GetByUsername_WhenUserDoesNotExist_ThenThrowsException()
        {
            // given/when/then
            Assert.ThrowsException<Exception>(() => _usersRepository.GetByUsername("nonexistinguser"));
        }

        [TestMethod]
        public void GetRoleByUsername_WhenUserExists_ThenReturnsRole()
        {
            // given/when
            string role = _usersRepository.GetRoleByUsername("user");
            // then
            Assert.IsNotNull(role);
            Assert.AreEqual("user", role);
        }

        [TestMethod]
        public void GetRoleByUsername_WhenUserDoesNotExist_ThenThrowsException()
        {
            // given/when/then
            Assert.ThrowsException<Exception>(() => _usersRepository.GetRoleByUsername("nonexistinguser"));
        }

        [TestMethod]
        public void GetAll_WhenReadUsersFromJson_ReturnsAllUsers()
        {
            // given/when
            List<User> users = _usersRepository.GetAll();
            // then
            Assert.IsNotNull(users);
            Assert.AreEqual(2, users.Count);
            Assert.AreEqual("admin", users[0].Username);
            Assert.AreEqual("admin", users[0].Password);
            Assert.AreEqual("admin", users[0].Role);
            Assert.AreEqual("user", users[1].Username);
            Assert.AreEqual("user", users[1].Password);
            Assert.AreEqual("user", users[1].Role);
        }
    }
}
