using DbManager;

namespace OurTests
{
  public class CreateSecurityProfileTests
  {
    #region Constructor Tests
    [Fact]
    public void CreateSecurityProfile_Constructor_ShouldInitializeAttributesCorrectly()
    {
      //Arrange
      var profileName = "pepito";

      //Act
      var createSecurityProfile = new CreateSecurityProfile(profileName);

      //Assert
      Assert.Equal(profileName, createSecurityProfile.ProfileName);

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