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
  }
}