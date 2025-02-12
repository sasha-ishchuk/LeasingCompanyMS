using System.Text.Json;
using LeasingCompanyMS.Utils;

namespace LeasingCompanyMSTest.Utils;

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
        var result = JsonUtils.ReadFromJson(_jsonFilePath);
        // when/then
        Assert.IsNotNull(result);
        var expectedJson = File.ReadAllText(_jsonFilePath);
        Assert.AreEqual(expectedJson, result);
    }

    [TestMethod]
    public void ReadUsersFromJson_WhenFileDoesNotExist_ThenThrowsFileNotFoundException() {
        // given
        var nonExistentFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "nonexistent.json");
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
        var jsonString = File.ReadAllText(_jsonFilePath);
        // when
        var result = JsonUtils.MapJsonStringToUserList(jsonString);
        // then
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Count);
        Assert.AreEqual("admin", result[0].Username);
        Assert.AreEqual("admin", result[0].Password);
        Assert.AreEqual("admin", result[0].Role);
    }

    [TestMethod]
    public void MapJsonStringToUserList_WhenJsonStringIsEmpty_ThenReturnsEmptyList() {
        // given
        var jsonString = "[]";
        // when
        var result = JsonUtils.MapJsonStringToUserList(jsonString);
        // then
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void MapJsonStringToUserList_WhenJsonStringIsInvalid_ThenThrowsJsonException() {
        // given
        var invalidJsonString = "invalid json";
        // when/then
        Assert.ThrowsException<JsonException>(() => JsonUtils.MapJsonStringToUserList(invalidJsonString));
    }
}