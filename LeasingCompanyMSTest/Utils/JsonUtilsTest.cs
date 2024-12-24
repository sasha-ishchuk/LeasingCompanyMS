using System.Text.Json;
using LeasingCompanyMS.Model;
using LeasingCompanyMS.Utils;

namespace LeasingCompanyMSTest.Utils
{
    [TestClass]
    public class JsonUtilsTest
    {
        private JsonUtils _jsonUtils;
        private string _jsonFilePath;

        [TestInitialize]
        public void Setup()
        {
            _jsonUtils = new JsonUtils();
            _jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "users_test.json");
        }

        [TestMethod]
        public void ReadUsersFromJson_WhenFileExists_ThenReturnsJsonString()
        {
            // given
            string result = _jsonUtils.ReadUsersFromJson(_jsonFilePath);
            // when/then
            Assert.IsNotNull(result);
            string expectedJson = File.ReadAllText(_jsonFilePath);
            Assert.AreEqual(expectedJson, result);
        }

        [TestMethod]
        public void ReadUsersFromJson_WhenFileDoesNotExist_ThenThrowsFileNotFoundException()
        {
            // given
            string nonExistentFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "nonexistent.json");
            // when/then
            Assert.ThrowsException<FileNotFoundException>(() => _jsonUtils.ReadUsersFromJson(nonExistentFilePath));
        }

        [TestMethod]
        public void ReadUsersFromJson_WhenFilePathIsNull_ThenThrowsArgumentNullException()
        {
            // given/when/then
            Assert.ThrowsException<ArgumentNullException>(() => _jsonUtils.ReadUsersFromJson(null));
        }

        [TestMethod]
        public void MapJsonStringToUserList_WhenJsonStringIsValid_ThenReturnsUserList()
        {
            // given
            string jsonString = File.ReadAllText(_jsonFilePath);
            // when
            List<User> result = _jsonUtils.MapJsonStringToUserList(jsonString);
            // then
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("test", result[0].Username);
            Assert.AreEqual("test", result[0].Password);
            Assert.AreEqual("test", result[0].Role);
        }

        [TestMethod]
        public void MapJsonStringToUserList_WhenJsonStringIsEmpty_ThenReturnsEmptyList()
        {
            // given
            string jsonString = "[]";
            // when
            List<User> result = _jsonUtils.MapJsonStringToUserList(jsonString);
            // then
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void MapJsonStringToUserList_WhenJsonStringIsInvalid_ThenThrowsJsonException()
        {
            // given
            string invalidJsonString = "invalid json";
            // when/then
            Assert.ThrowsException<JsonException>(() => _jsonUtils.MapJsonStringToUserList(invalidJsonString));
        }
    }
}