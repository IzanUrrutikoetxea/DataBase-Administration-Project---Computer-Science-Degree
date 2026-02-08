using DbManager;

namespace OurTests
{
  public class RevokeTests
  {
    #region Constructor Tests
    [Fact]
    public void Revoke_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var privilegeName = "pepito";
      var tableName = "TestTable";
      var profileName = "juan";

      //Act
      var revoke = new Revoke(privilegeName, tableName, profileName);

      //Assert
      Assert.Equal(privilegeName, revoke.PrivilegeName);
      Assert.Equal(tableName, revoke.TableName);
      Assert.Equal(profileName, revoke.ProfileName);

    }
    #endregion

    #region Execute tests
    //[Fact]
    //public void Delete_Execute_ShouldWorkCorrectly()
    //{
    //  //Arrange
    //  var table = "TestTable";
    //  var condition = new DbManager.Condition("Name", "=", "Pepe");
    //  var delete = new DbManager.Parser.Delete(table, condition);
    //  var database = Database.CreateTestDatabase();

    //  //Act
    //  var result = delete.Execute(database);

    //  //Assert
    //  Assert.Equal(Constants.DeleteSuccess, result);
    //  Assert.Equal(2, database.TableByName("TestTable").NumRows());
    //}
    #endregion
  }
}