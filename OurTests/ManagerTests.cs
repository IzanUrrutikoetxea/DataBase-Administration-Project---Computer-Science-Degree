using DbManager.Security;

namespace OurTests
{
  public class ManagerTests
  {
    #region IsUserAdmin Tests
    [Fact]
    public void ManagerTests_IsUserAdmin_ShouldReturnTrue_WhenUserIsAdmin()
    {
      //Arrange
      var adminName = Profile.AdminProfileName;
      var manager = new Manager(adminName);

      //Act
      var result = manager.IsUserAdmin();

      //Assert
      Assert.True(result);
    }
    [Fact]
    public void ManagerTests_IsUserAdmin_ShouldReturnFalse_WhenUserIsNotAdmin()
    {
      //Arrange
      var adminName = Profile.AdminProfileName + "a";
      var manager = new Manager(adminName);

      //Act
      var result = manager.IsUserAdmin();

      //Assert
      Assert.False(result);
    }
    #endregion

    #region IsPasswordCorrect Tests
    [Fact]
    public void ManagerTests_IsPasswordCorrect_ShouldReturnTrue_WhenPasswordIsCorrect()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      var result = manager.IsPasswordCorrect(username, password);

      //Arrange
      Assert.True(result);
    }
    [Fact]
    public void ManagerTests_IsPasswordCorrect_ShouldReturnFalse_WhenPasswordIsIncorrect()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);

      var manager = new Manager(username);

      //Act
      var result = manager.IsPasswordCorrect(username, password + "a");

      //Arrange
      Assert.False(result);
    }
    #endregion
  }
}