using DbManager;
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

    #region GrantPrivilege Tests
    [Fact]
    public void Manager_GrantPrivilege_ShouldGrantPrivilege_WhenAllIsOk()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";
      profile.PrivilegesOn["TestTable"] = new List<Privilege>() { };

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      manager.GrantPrivilege("Engineers", "TestTable", Privilege.Select);

      //Assert
      Assert.True(profile.IsGrantedPrivilege("TestTable", Privilege.Select));
    }
    [Fact]
    public void Manager_GrantPrivilege_ShouldDoNothing_WhenProfileDoesNotExist()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";
      profile.PrivilegesOn["TestTable"] = new List<Privilege>() {  };

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      manager.GrantPrivilege("Diseigners", "TestTable", Privilege.Select);

      //Assert
      Assert.False(profile.IsGrantedPrivilege("TestTable", Privilege.Select));
    }

    [Fact]
    public void Manager_GrantPrivilege_ShouldDoNothing_WhenTableDoesNotExist()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      manager.GrantPrivilege("Engineers", "TestTable", Privilege.Select);

      //Assert
      Assert.False(profile.PrivilegesOn.ContainsKey("TestTable"));
    }
    #endregion

    #region RevokePrivilege Tests
    [Fact]
    public void Manager_RevokePrivilege_ShouldRevokePrivilege_WhenAllIsOk()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";
      profile.PrivilegesOn["TestTable"] = new List<Privilege>() { Privilege.Select };

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      manager.RevokePrivilege("Engineers", "TestTable", Privilege.Select);

      //Assert
      Assert.False(profile.IsGrantedPrivilege("TestTable", Privilege.Select));
    }
    [Fact]
    public void Manager_RevokePrivilege_ShouldDoNothing_WhenProfileDoesNotExist()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";
      profile.PrivilegesOn["TestTable"] = new List<Privilege>() { Privilege.Select };

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      manager.RevokePrivilege("Diseigners", "TestTable", Privilege.Select);

      //Assert
      Assert.True(profile.IsGrantedPrivilege("TestTable", Privilege.Select));
    }

    [Fact]
    public void Manager_RevokePrivilege_ShouldDoNothing_WhenTableDoesNotExist()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      manager.RevokePrivilege("Engineers", "TestTable", Privilege.Select);

      //Assert
      Assert.False(profile.PrivilegesOn.ContainsKey("TestTable"));
    }
    #endregion

    #region IsGrantedfPrivilege Tests
    [Fact]
    public void Manager_IsGrantedfPrivilege_ShouldReturnTrue_WhenUserHasPrivilege()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";
      profile.PrivilegesOn["TestTable"] = new List<Privilege>() { Privilege.Select };

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act & Assert
      Assert.True(manager.IsGrantedPrivilege("User", "TestTable", Privilege.Select));
    }
    [Fact]
    public void Manager_IsGrantedfPrivilege_ShouldReturnFalse_WhenUserHasNotPrivilege()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";
      profile.PrivilegesOn["TestTable"] = new List<Privilege>() { Privilege.Select };

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act & Assert
      Assert.False(manager.IsGrantedPrivilege("fakeUser", "TestTable", Privilege.Select));
      Assert.False(manager.IsGrantedPrivilege(username, "TestTablea", Privilege.Select));
      Assert.False(manager.IsGrantedPrivilege(username, "TestTable", Privilege.Delete));
    }
    [Fact]
    public void Manager_IsGrantedfPrivilege_ShouldReturnFalse_WhenAnErrorOccurs()
    {

    }
    #endregion

    #region AddProfile Tests
    [Fact]
    public void Manager_AddProfile_ShouldAddProfile()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";

      var manager = new Manager(username);

      //Act
      manager.AddProfile(profile);

      //Assert
      Assert.True(manager.Profiles.Contains(profile)); 
    }
    #endregion

    #region UserByName Tests
    [Fact]
    public void Manager_UserByName_ShouldReturnTheCorrectUser_WhenUserExists()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      var resultUser = manager.UserByName(username);

      //Assert
      Assert.Equal(user.Username,resultUser.Username);
      Assert.Equal(user.EncryptedPassword,resultUser.EncryptedPassword);
    }
    [Fact]
    public void Manager_UserByName_ShouldReturnNull_WhenUserDoesNotExist()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      var resultUser = manager.UserByName("fakeUser");

      //Assert
      Assert.Null(resultUser);
    }
    #endregion

    #region ProfileByName Tests
    [Fact]
    public void Manager_ProfileByName_ShouldReturnTheCorrectProfile_WhenProfileExists()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      var resultProfile = manager.ProfileByName("Engineers");

      //Assert
      Assert.Equal(profile.Users.Count, resultProfile.Users.Count);
      Assert.Equal(profile.Name, resultProfile.Name);
    }
    [Fact]
    public void Manager_ProfileByName_ShouldReturnNull_WhenProfileDoesNotExist()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      var resultProfile = manager.ProfileByName("Designers");

      //Assert
      Assert.Null(resultProfile);
    }
    #endregion

    #region ProfileByUser Tests
    [Fact]
    public void Manager_ProfileByUser_ShouldReturnTheCorrectProfile_WhenUserExists()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      var resultProfile = manager.ProfileByUser("User");

      //Assert
      Assert.Equal(profile.Users.Count, resultProfile.Users.Count);
      Assert.Equal(profile.Name, resultProfile.Name);
    }
    [Fact]
    public void Manager_ProfileByUser_ShouldReturnNull_WhenUserDoesNotExist()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      var resultProfile = manager.ProfileByUser("fakeUser");

      //Assert
      Assert.Null(resultProfile);
    }
    #endregion

    #region RemoveProfile Tests
    [Fact]
    public void Manager_RemoveProfile_ShouldReturnTrue_WhenProfileExists()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      var resultProfile = manager.RemoveProfile("Engineers");

      //Assert
      Assert.True(resultProfile);
      Assert.Equal(0, manager.Profiles.Count);
    }
    [Fact]
    public void Manager_RemoveProfile_ShouldReturnFalse_WhenProfileDoesNotExist()
    {
      //Arrange
      var username = "User";
      var password = "user";
      var user = new User(username, password);

      var profile = new Profile();
      profile.Users.Add(user);
      profile.Name = "Engineers";

      var manager = new Manager(username);
      manager.Profiles.Add(profile);

      //Act
      var resultProfile = manager.RemoveProfile("Diseigners");

      //Assert
      Assert.False(resultProfile);
      Assert.Equal(1, manager.Profiles.Count);
    }
    #endregion
  }
}