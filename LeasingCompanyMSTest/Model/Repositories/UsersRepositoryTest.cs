using LeasingCompanyMS.Model.Repositories;

namespace LeasingCompanyMSTest.Model.Repositories {
    [TestClass]
    public class UsersRepositoryTest {
        private readonly UsersRepository _usersRepository = new();
       
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
            var user = _usersRepository.GetById("1");
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
    }
}
