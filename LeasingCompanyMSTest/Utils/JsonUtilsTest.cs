using System.Text.Json;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Utils;

namespace LeasingCompanyMSTest.Utils
{
    [TestClass]
    public class JsonUtilsTest {

        private string _jsonFilePath;

        [TestInitialize]
        public void Setup() {
            _jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "users_test.json");
        }

        [TestMethod]
        public void ReadUsersFromJson_WhenFileExists_ThenReturnsJsonString() {
            // given
            string result = JsonUtils.ReadFromJson(_jsonFilePath);
            // when/then
            Assert.IsNotNull(result);
            string expectedJson = File.ReadAllText(_jsonFilePath);
            Assert.AreEqual(expectedJson, result);
        }

        [TestMethod]
        public void ReadUsersFromJson_WhenFileDoesNotExist_ThenThrowsFileNotFoundException() {
            // given
            string nonExistentFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "nonexistent.json");
            // when/then
            Assert.ThrowsException<FileNotFoundException>(() => JsonUtils.ReadFromJson(nonExistentFilePath));
        }

        [TestMethod]
        public void ReadUsersFromJson_WhenFilePathIsNull_ThenThrowsArgumentNullException() {
            // given/when/then
            Assert.ThrowsException<ArgumentNullException>(() => JsonUtils.ReadFromJson(null));
        }

        [TestMethod]
        public void MapJsonStringToUserList_WhenJsonStringIsValid_ThenReturnsUserList() {
            // given
            string jsonString = File.ReadAllText(_jsonFilePath);
            // when
            List<User> result = JsonUtils.MapJsonStringToUserList(jsonString);
            // then
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("test", result[0].Username);
            Assert.AreEqual("test", result[0].Password);
            Assert.AreEqual("test", result[0].Role);
        }

        [TestMethod]
        public void MapJsonStringToUserList_WhenJsonStringIsEmpty_ThenReturnsEmptyList() {
            // given
            string jsonString = "[]";
            // when
            List<User> result = JsonUtils.MapJsonStringToUserList(jsonString);
            // then
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void MapJsonStringToUserList_WhenJsonStringIsInvalid_ThenThrowsJsonException() {
            // given
            string invalidJsonString = "invalid json";
            // when/then
            Assert.ThrowsException<JsonException>(() => JsonUtils.MapJsonStringToUserList(invalidJsonString));
        }
    }
}