using DbManager.Security;

namespace OurTests
{
  public class ProfileTests
  {
    #region GrantPrivilege Tests
    [Fact]
    public void Profile_GrantPrivilege_ShouldGrantPrivilege_WhenAllGoesOk()
    {
      //Assert
      var table = "TestTable";
      var privilege = Privilege.Delete;
      var profile = new Profile();
      profile.PrivilegesOn[table] = new List<Privilege>();

      //Act
      profile.GrantPrivilege(table, privilege);

      //Assert
      var result = profile.PrivilegesOn[table].Contains(privilege);
      Assert.True(result);
    }
    [Fact]
    public void Profile_GrantPrivilege_ShouldReturnFalse_WhenTableNameIsEmpty()
    {
      //Assert
      var table = "";
      var privilege = Privilege.Delete;
      var profile = new Profile();

      //Act & Assert
      Assert.False(profile.GrantPrivilege(table, privilege));
    }
    [Fact]
    public void Profile_GrantPrivilege_ShouldReturnFalse_WhenTableNameIsNull()
    {
      //Assert
      string table = null;
      var privilege = Privilege.Delete;
      var profile = new Profile();

      //Act & Assert
      Assert.False(profile.GrantPrivilege(table, privilege));
    }
    [Fact]
    public void Profile_GrantPrivilege_ShouldReturnFalse_WhenProfileDoesNotContainTable()
    {
      //Assert
      string table = "TestTable";
      var privilege = Privilege.Delete;
      var profile = new Profile();

      //Act & Assert
      Assert.False(profile.GrantPrivilege(table, privilege));
    }
    [Fact]
    public void Profile_GrantPrivilege_ShouldReturnFalse_WhenTableAlreadyContainsPrivilege()
    {
      //Assert
      string table = "TestTable";
      var privilege = Privilege.Delete;
      var profile = new Profile();
      profile.PrivilegesOn[table] = new List<Privilege>() { privilege};

      //Act & Assert
      Assert.False(profile.GrantPrivilege(table, privilege));
    }
    #endregion

    #region RevokePrivilege Tests
    [Fact]
    public void Profile_RevokePrivilege_ShouldRevokePrivilege_WhenAllGoesOk()
    {
      //Assert
      var table = "TestTable";
      var privilege = Privilege.Delete;
      var profile = new Profile();
      profile.PrivilegesOn[table] = new List<Privilege>() { privilege };

      //Act
      profile.RevokePrivilege(table, privilege);

      //Assert
      var result = profile.PrivilegesOn[table].Contains(privilege);
      Assert.False(result);
    }
    [Fact]
    public void Profile_RevokePrivilege_ShouldReturnFalse_WhenTableNameIsEmpty()
    {
      //Assert
      var table = "";
      var privilege = Privilege.Delete;
      var profile = new Profile();

      //Act & Assert
      Assert.False(profile.RevokePrivilege(table, privilege));
    }
    [Fact]
    public void Profile_RevokePrivilege_ShouldReturnFalse_WhenTableNameIsNull()
    {
      //Assert
      string table = null;
      var privilege = Privilege.Delete;
      var profile = new Profile();

      //Act & Assert
      Assert.False(profile.RevokePrivilege(table, privilege));
    }
    [Fact]
    public void Profile_RevokePrivilege_ShouldReturnFalse_WhenProfileDoesNotContainTable()
    {
      //Assert
      string table = "TestTable";
      var privilege = Privilege.Delete;
      var profile = new Profile();

      //Act & Assert
      Assert.False(profile.RevokePrivilege(table, privilege));
    }
    [Fact]
    public void Profile_RevokePrivilege_ShouldReturnFalse_WhenTableDoesNotContainPrivilege()
    {
      //Assert
      string table = "TestTable";
      var privilege = Privilege.Delete;
      var profile = new Profile();
      profile.PrivilegesOn[table] = new List<Privilege>() { };

      //Act & Assert
      Assert.False(profile.RevokePrivilege(table, privilege));
    }
    #endregion

    #region IsGrantedPrivilege Tests
    [Fact]
    public void Profile_IsGrantedPrivilege_ShouldReturnTrue_WhenTableDoesContainPrivilege()
    {
      //Assert
      string table = "TestTable";
      var privilege = Privilege.Delete;
      var profile = new Profile();
      profile.PrivilegesOn[table] = new List<Privilege>() { privilege };

      //Act & Assert
      Assert.True(profile.IsGrantedPrivilege(table, privilege));
    }
    [Fact]
    public void Profile_IsGrantedPrivilege_ShouldReturnTrue_WhenTableDoesNotContainPrivilege()
    {
      //Assert
      string table = "TestTable";
      var privilege = Privilege.Delete;
      var profile = new Profile();
      profile.PrivilegesOn[table] = new List<Privilege>() {  };

      //Act & Assert
      Assert.False(profile.IsGrantedPrivilege(table, privilege));
    }
    #endregion
  }
}