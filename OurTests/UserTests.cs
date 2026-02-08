using DbManager.Security;

namespace OurTests
{
  public class UserTests
  {
    #region Constructor Tests
    [Fact]
    public void UserTests_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var username = "admin";
      var password = "admin";

      //Act
      var user = new User(username, password);

      //Assert
      Assert.Equal(username, user.Username);
      Assert.Equal(Encryption.Encrypt(password), user.EncryptedPassword);

    }
    #endregion
  }
}