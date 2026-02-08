using DbManager;

namespace OurTests
{
  public class DropSecurityProfileTests
  {
    #region Constructor Tests
    [Fact]
    public void DropSecurityProfile_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var profileName = "pepito";

      //Act
      var dropSecurityProfile = new DropSecurityProfile(profileName);

      //Assert
      Assert.Equal(profileName, dropSecurityProfile.ProfileName);

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