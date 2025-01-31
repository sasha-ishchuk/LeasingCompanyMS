using LeasingCompanyMS.Model;
using LeasingCompanyMS.Model.Repositories;
using LeasingCompanyMS.Utils;
using Moq;

namespace LeasingCompanyMSTest.Model.Repositories {
    [TestClass]
    public class UsersRepositoryTest {
        private readonly UsersRepository _usersRepository = new();

        [TestMethod]
        public void AuthenticateUser_WhenValidCredentials_ThenReturnsTrue() {
            // given
            var username = "user";
            var password = "user";
            // when
            var result = _usersRepository.AuthenticateUser(username, password);
            // then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AuthenticateUser_WhenInvalidCredentials_ThenReturnsFalse() {
            // given
            var username = "user1";
            var password = "user1";
            // when
            var result = _usersRepository.AuthenticateUser(username, password);
            // then
            Assert.IsFalse(result);
        }
       
        [TestMethod]
        public void GetAll_ReturnsAllUsers() {
            // given/when
            List<User> users = _usersRepository.GetAll();
            // then
            Assert.IsNotNull(users);
            Assert.AreEqual(3, users.Count);
            Assert.AreEqual("admin", users[0].Username);
            Assert.AreEqual("admin", users[0].Password);
            Assert.AreEqual("admin", users[0].Role);
            Assert.AreEqual("user", users[1].Username);
            Assert.AreEqual("user", users[1].Password);
            Assert.AreEqual("user", users[1].Role);
        }

        [TestMethod]
        public void GetById_WhenUserExists_ThenReturnsUser() {
            // given/when
            User user = _usersRepository.GetById("1");
            // then
            Assert.IsNotNull(user);
            Assert.AreEqual("user", user.Username);
            Assert.AreEqual("user", user.Password);
            Assert.AreEqual("user", user.Role);
        }

        [TestMethod]
        public void GetById_WhenUserDoesNotExist_ThenReturnsNull() {
            // given/when
            User user = _usersRepository.GetById("nonexistingid");
            // then
            Assert.IsNull(user);
        }

        [TestMethod]
        public void Get_WithRoleFilter_ReturnsFilteredUsers() {
            // given
            var filter = new UsersFilter { Role = "admin" };
            // when
            List<User> users = _usersRepository.Get(filter);
            // then
            Assert.IsNotNull(users);
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual("admin", users[0].Username);
            Assert.AreEqual("admin", users[0].Password);
            Assert.AreEqual("admin", users[0].Role);
        }

        [TestMethod]
        public void Get_WithUsernamrFilter_ReturnsFilteredUsers() {
            // given
            var filter = new UsersFilter { Username = "admin" };
            // when
            List<User> users = _usersRepository.Get(filter);
            // then
            Assert.IsNotNull(users);
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual("admin", users[0].Username);
            Assert.AreEqual("admin", users[0].Password);
            Assert.AreEqual("admin", users[0].Role);
        }

        [TestMethod]
        public void Get_WithPasswordFilter_ReturnsFilteredUsers() {
            // given
            var filter = new UsersFilter { Password = "admin" };
            // when
            List<User> users = _usersRepository.Get(filter);
            // then
            Assert.IsNotNull(users);
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual("admin", users[0].Username);
            Assert.AreEqual("admin", users[0].Password);
            Assert.AreEqual("admin", users[0].Role);
        }
    }
}
